using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Livro/Mente Partilhada/Nv3")]
public class HabilidadeMentePartilhadaNv3 : HabilidadePassiva
{
    [SerializeField]
    private float multiplicadorRegeneracaoSP = 0.02f;
    public GameObject vfx;
    [SerializeField]
    private float tempoVFX = 1.5f;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadePassivaArma)
        {
            if (base.ChecarRuna(personagem, nivel))
            {
                personagem.efeitoPorHabilidadeAliadoAtivado = true;

                personagem.AtivarEfeitoPorHabilidadeAliado("MentePartilhadaNv3", () =>
                {
                    float sp = personagem._spMaximoEInicial * multiplicadorRegeneracaoSP;
                    personagem.ReceberSP(sp);

                    if (personagem.vfxHabilidadePassivaArma == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadePassivaArma = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(4, true);
                    }

                    personagem.StartCoroutine(EsperarVFX(personagem));
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.efeitoPorHabilidadeAliadoAtivado = false;
        personagem.RemoverEfeitoPorHabilidadeAliado("MentePartilhadaNv3");
    }

    IEnumerator EsperarVFX(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(tempoVFX);
        personagem.GerenciarVFXHabilidade(4, false);
    }
}
