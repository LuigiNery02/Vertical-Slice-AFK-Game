using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Golpe Espartano/Nv1")]
public class HabilidadeGolpeEspartanoNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem))
            {
                personagem.efeitoPorAtaqueAtivado = true;
                personagem.podeAtivarEfeitoHabilidadeAtivaClasse = false;

                personagem.GastarSP(custoDeMana);

                personagem.AtivarEfeitoPorAtaque("GolpeEspartanoNv1", (bool acerto) =>
                {
                    if (acerto)
                    {
                        if (personagem._personagemAlvo.conjurandoHabilidade)
                        {
                            personagem._personagemAlvo.CancelarHabilidade();
                            personagem.StartCoroutine(EsperarFrame(personagem));
                        }
                        else
                        {
                            RemoverEfeito(personagem);
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
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("GolpeEspartanoNv1");
        personagem.efeitoPorAtaqueAtivado = false;
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(1, false);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem)
    {
        yield return null; //agurada um frame
        RemoverEfeito(personagem);
    }
}
