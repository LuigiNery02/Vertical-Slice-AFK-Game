using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Livro/Barreira/Nv2")]
public class HabilidadeBarreiraNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorSP = 0.15f;
    public GameObject vfx;
    [SerializeField]
    private GameObject vfxEscudoAliado;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaArma)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                base.ChecarCastingHabilidade2(personagem, () =>
                {
                    if (personagem.vfxHabilidadeAtivaArma == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(2, true);
                    }

                    IAPersonagemBase aliadoEncontrado = null;
                    float sp = 0;

                    foreach (IAPersonagemBase aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
                    {
                        if (aliado != null && aliado.controlador == personagem.controlador && aliado._comportamento != EstadoDoPersonagem.MORTO && aliado != personagem)
                        {
                            aliadoEncontrado = aliado;
                        }
                    }

                    sp = personagem._spMaximoEInicial * multiplicadorSP;

                    aliadoEncontrado.valorEscudo += sp;
                    aliadoEncontrado.escudoAtivado = true;

                    if (aliadoEncontrado.escudoVfx == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfxEscudoAliado, aliadoEncontrado.transform.position + Vector3.zero, aliadoEncontrado.transform.rotation, aliadoEncontrado.transform);
                        aliadoEncontrado.escudoVfx = vfxInstanciado;
                    }
                    else
                    {
                        aliadoEncontrado.escudoVfx.SetActive(true);
                    }

                    personagem.StartCoroutine(EsperarTempoVFX(personagem));
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(2, false);
        base.RemoverEfeito(personagem);
    }

    IEnumerator EsperarTempoVFX(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(1);
        RemoverEfeito(personagem);
    }
}
