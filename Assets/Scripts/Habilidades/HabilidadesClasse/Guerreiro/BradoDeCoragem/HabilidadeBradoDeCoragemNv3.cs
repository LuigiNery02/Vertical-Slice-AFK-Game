using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Brado de Coragem/Nv3")]

public class HabilidadeBradoDeCoragemNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float raioDeDistanciaDoTaunt = 10;
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade1(personagem, () =>
                {
                    personagem.imuneAStun = true;
                    personagem.imuneAKnockback = true;
                    personagem.StartCoroutine(EsperarTempoDeEfeitoTaunt(personagem));

                    if (personagem.vfxHabilidadeAtivaClasse == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(1, true);
                    }
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.imuneAStun = false;
        personagem.imuneAKnockback = false;
        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }

    IEnumerator EsperarTempoDeEfeitoTaunt(IAPersonagemBase personagem)
    {
        Dictionary<IAPersonagemBase, IAPersonagemBase> alvosOriginais = new();

        Collider[] colliders = Physics.OverlapSphere(personagem.transform.position, raioDeDistanciaDoTaunt);

        foreach (var collider in colliders)
        {
            IAPersonagemBase inimigo = collider.GetComponent<IAPersonagemBase>();

            if (inimigo != null && inimigo.controlador != personagem.controlador && inimigo._comportamento != EstadoDoPersonagem.MORTO)
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

        yield return new WaitForSeconds(tempoDeEfeito);

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

        RemoverEfeito(personagem);
    }
}
