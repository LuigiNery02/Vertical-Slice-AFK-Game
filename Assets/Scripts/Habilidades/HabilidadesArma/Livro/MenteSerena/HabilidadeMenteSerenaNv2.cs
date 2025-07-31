using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Livro/Mente Serena/Nv2")]
public class HabilidadeMenteSerenaNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorSP = 0.04f;
    [SerializeField]
    private float tempoParaRestaurarSP = 1;
    public GameObject vfx;
    [SerializeField]
    private GameObject vfxAliado;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
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

                    sp = aliadoEncontrado._spMaximoEInicial * multiplicadorSP;
                    GameObject vfxAliadoInstanciado = GameObject.Instantiate(vfxAliado, aliadoEncontrado.transform.position + Vector3.zero, aliadoEncontrado.transform.rotation, aliadoEncontrado.transform);

                    personagem.StartCoroutine(ChecarCondicaoDeEfeito(personagem, aliadoEncontrado, sp, vfxAliadoInstanciado));
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(2, false);
        base.RemoverEfeito(personagem);
    }

    IEnumerator ChecarCondicaoDeEfeito(IAPersonagemBase personagem, IAPersonagemBase aliado, float sp, GameObject vfx)
    {
        while (base.ChecarAtivacao(personagem) && aliado != null && aliado._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem.GastarSP(custoDeMana);

            personagem.GerenciarVFXHabilidade(2, true);
            vfx.SetActive(true);

            aliado.ReceberSP(sp);

            yield return new WaitForSeconds(1);
        }

        Destroy(vfx);
        RemoverEfeito(personagem);
    }
}
