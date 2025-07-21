using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Espada/Ataque Pesado/Nv3")]

public class HabilidadeAtaquePesadoNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 2.5f;
    [SerializeField]
    private float tempoDeStun = 3;
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaArma)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade2(personagem, () =>
                {
                    personagem.efeitoPorAtaqueAtivado = true;

                    float danoOriginal = personagem._dano;

                    personagem.AtivarEfeitoPorAtaque("AtaquePesadoNv3", (bool acerto) =>
                    {
                        if (acerto)
                        {
                            personagem._dano *= multiplicadorDeDano;

                            IAPersonagemBase inimigo = personagem._personagemAlvo;

                            if (!inimigo.stunado)
                            {
                                inimigo.tempoDeStun = tempoDeStun;
                                inimigo.VerificarComportamento("stun");
                            }
                            personagem.StartCoroutine(EsperarFrame(personagem, danoOriginal));
                        }
                        else
                        {
                            RemoverEfeito(personagem);
                        }

                    });

                    if (personagem.vfxHabilidadeAtivaArma == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(2, true);
                    }
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("AtaquePesadoNv3");
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(2, false);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem, float dano)
    {
        yield return null; //agurada um frame
        personagem._dano = dano;
        RemoverEfeito(personagem);
    }
}
