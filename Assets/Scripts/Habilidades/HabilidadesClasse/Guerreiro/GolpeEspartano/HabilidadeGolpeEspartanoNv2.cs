using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Golpe Espartano/Nv2")]

public class HabilidadeGolpeEspartanoNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float tempoDeTaunt;
    [SerializeField]
    private float raioDeDistanciaDoTaunt;
    public GameObject vfx;

    private Dictionary<IAPersonagemBase, IAPersonagemBase> alvosOriginais = new();
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidade1)
        {
            if (base.ChecarAtivacao(personagem))
            {
                personagem.efeitoPorAtaque = null;
                personagem.efeitoPorAtaqueAtivado = true;
                personagem.podeAtivarEfeitoHabilidade1 = false;

                personagem.GastarSP(custoDeMana);

                personagem.efeitoPorAtaque = (bool acerto) =>
                {
                    if (acerto)
                    {
                        AplicarTaunt(personagem);
                    }
                    else
                    {
                        RemoverEfeito(personagem);
                    }
                };

                if (personagem.vfxHabilidade1 == null)
                {
                    GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                    personagem.vfxHabilidade1 = vfxInstanciado;
                }
                else
                {
                    personagem.GerenciarVFXHabilidade(1, true);
                }
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.efeitoPorAtaque = null;
        personagem.efeitoPorAtaqueAtivado = false;
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(1, false);
    }

    private void AplicarTaunt(IAPersonagemBase personagem)
    {
        Collider[] colliders = Physics.OverlapSphere(personagem.transform.position, raioDeDistanciaDoTaunt);

        foreach (var collider in colliders)
        {
            IAPersonagemBase inimigo = collider.GetComponent<IAPersonagemBase>();

            if (inimigo != null && inimigo.controlador != personagem.controlador && inimigo._comportamento != EstadoDoPersonagem.MORTO)
            {
                if (!alvosOriginais.ContainsKey(inimigo))
                {
                    alvosOriginais[inimigo] = inimigo._personagemAlvo;
                    inimigo._personagemAlvo = personagem;
                    inimigo._alvoAtual = personagem.transform;
                    if (!inimigo.stunado)
                    {
                        inimigo.VerificarComportamento("perseguir");
                    }
                }
            }
        }

        personagem.StartCoroutine(EsperarTempoDeTaunt(personagem));
    }

    IEnumerator EsperarTempoDeTaunt(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(tempoDeTaunt);

        foreach (var par in alvosOriginais)
        {
            if (par.Key != null && par.Value != null && par.Key._comportamento != EstadoDoPersonagem.MORTO)
            {
                par.Key._personagemAlvo = par.Value;
                par.Key._alvoAtual = par.Value.transform;
                if (!par.Key.stunado)
                {
                    par.Key.VerificarComportamento("perseguir");
                }
            }
        }

        alvosOriginais.Clear();
        RemoverEfeito(personagem);
    }
}
