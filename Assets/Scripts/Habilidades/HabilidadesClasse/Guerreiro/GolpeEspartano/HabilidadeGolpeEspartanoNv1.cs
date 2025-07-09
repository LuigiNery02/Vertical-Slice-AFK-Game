using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Golpe Espartano/Nv1")]
public class HabilidadeGolpeEspartanoNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    public GameObject vfx;

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
                        if (personagem._personagemAlvo.conjurandoHabilidade)
                        {
                            personagem._personagemAlvo.CancelarHabilidade();
                            personagem.StartCoroutine(EsperarFrame(personagem));
                        }
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

    IEnumerator EsperarFrame(IAPersonagemBase personagem)
    {
        yield return null; //agurada um frame
        RemoverEfeito(personagem);
    }
}
