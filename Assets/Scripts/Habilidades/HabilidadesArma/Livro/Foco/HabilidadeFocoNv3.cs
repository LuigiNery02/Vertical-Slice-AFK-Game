using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Livro/Foco/Nv3")]
public class HabilidadeFocoNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorRecuperacaoSP = 0.03f;
    [SerializeField]
    private float probabilidadeDeRecuperacaoSP = 20;
    public GameObject vfx;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadePassivaArma)
        {
            if (base.ChecarRuna(personagem, nivel))
            {
                personagem.efeitoPorAliadoCuradoAtivado = true;

                personagem.AtivarEfeitoPorAliadoCurado("FocoNv3", () =>
                {
                    if (CalcularProbabilidadeDeRecuperacao())
                    {
                        float sp = personagem._spMaximoEInicial * multiplicadorRecuperacaoSP;
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
                    }
                });
            }
        }
    }
    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorDanoCausado("FocoNv3");
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(4, false);
    }

    private bool CalcularProbabilidadeDeRecuperacao()
    {
        int rng = Random.Range(0, 100);

        return rng < probabilidadeDeRecuperacaoSP;
    }

    IEnumerator EsperarVFX(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(1.5f);
        personagem.GerenciarVFXHabilidade(4, false);
    }
}
