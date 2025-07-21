using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Golpe Espartano/Nv3")]

public class HabilidadeGolpeEspartanoNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float tempoDeTaunt = 3;
    [SerializeField]
    private float raioDeDistanciaDoTaunt;
    [SerializeField]
    private float tempoDeStun = 3;
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
                    personagem.efeitoPorAtaqueAtivado = true;

                    personagem.AtivarEfeitoPorAtaque("GolpeEspartanoNv3", (bool acerto) =>
                    {
                        if (acerto)
                        {
                            personagem.StartCoroutine(ExecutarEfeito(personagem, personagem._personagemAlvo));
                            if (!personagem._personagemAlvo)
                            {
                                personagem._personagemAlvo.tempoDeStun = tempoDeStun;
                                personagem._personagemAlvo.VerificarComportamento("stun");
                            }
                        }
                        else
                        {
                            RemoverEfeito(personagem);
                        }
                    });

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
        personagem.RemoverEfeitoPorAtaque("GolpeEspartanoNv3");
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(1, false);
    }

    private IEnumerator ExecutarEfeito(IAPersonagemBase personagem, IAPersonagemBase inimigoStunado)
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

                if (!inimigo.stunado && inimigo != inimigoStunado)
                {
                    inimigo.VerificarComportamento("perseguir");
                }
            }
        }

        if (inimigoStunado != null && !inimigoStunado.stunado)
        {
            inimigoStunado.tempoDeStun = tempoDeStun;
            inimigoStunado.VerificarComportamento("stun");
        }

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

        RemoverEfeito(personagem);
    }
}
