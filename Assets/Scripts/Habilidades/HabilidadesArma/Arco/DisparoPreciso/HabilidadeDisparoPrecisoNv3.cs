using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Arco/Disparo Preciso/Nv3")]
public class HabilidadeDisparoPrecisoNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 2.5f;
    [SerializeField]
    private float porcentagemDanoSangramento = 0.01f;
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

                    personagem.AtivarEfeitoPorAtaque("DisparoPrecisoNv3", (bool acerto) =>
                    {
                        if (acerto)
                        {
                            personagem._dano *= multiplicadorDeDano;
                            personagem.StartCoroutine(EsperarFrame(personagem, danoOriginal));

                            if(personagem._hitAtaquePersonagem != null)
                            {
                                HitAtaquePersonagem hit = personagem._hitAtaquePersonagem.GetComponent<HitAtaquePersonagem>();

                                if(hit != null)
                                {
                                    float danoSangramento = danoOriginal * porcentagemDanoSangramento;

                                    hit.efeitoExtraAoAtingirAlvo = (IAPersonagemBase alvo) =>
                                    {
                                        if (!alvo.sangramento && alvo._comportamento != EstadoDoPersonagem.MORTO)
                                        {
                                            alvo.Sangramento(danoSangramento, 1, tempoDeRecarga);
                                        }
                                    };
                                }
                            }
                        }
                        else
                        {
                            RemoverEfeito(personagem);
                        }
                    });

                    personagem.atravessarHit = true;

                    if (personagem.vfxHabilidadeAtivaArma == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(2, true);
                    }

                    if (personagem._personagemAlvo != null)
                    {
                        personagem.VerificarComportamento("perseguir");
                    }
                    else
                    {
                        personagem.VerificarComportamento("selecionarAlvo");
                    }
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("DisparoPrecisoNv3");
        personagem.atravessarHit = false;
        personagem.GerenciarVFXHabilidade(2, false);
        base.RemoverEfeito(personagem);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem, float dano)
    {
        yield return null; //agurada um frame
        personagem._dano = dano;
        RemoverEfeito(personagem);
    }
}
