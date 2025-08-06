using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Sacerdote/Grande Benção/Nv3")]
public class HabilidadeGrandeBencaoNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorCura = 2f;
    [SerializeField]
    private int numeroDeCuras = 3;
    public GameObject vfx;
    [SerializeField]
    private GameObject vfxAliado;
    [SerializeField]
    private float tempoDeVfxAliado = 1;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade1(personagem, () =>
                {
                    float cura = personagem._dano * multiplicadorCura;

                    if (personagem.vfxHabilidadeAtivaClasse == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(1, true);
                    }

                    foreach (IAPersonagemBase aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
                    {
                        if (aliado != null && aliado.controlador == personagem.controlador && aliado._comportamento != EstadoDoPersonagem.MORTO && aliado != personagem)
                        {
                            for (int i = 0; i < numeroDeCuras; i++)
                            {
                                personagem.CurarAliado(aliado, cura);

                                GameObject vfxAliadoInstanciado = GameObject.Instantiate(vfxAliado, aliado.transform.position + Vector3.zero, aliado.transform.rotation, aliado.transform);
                                personagem.StartCoroutine(EsperarVFXAliado(personagem, vfxAliadoInstanciado));
                            }
                        }
                    }

                    base.ChecarEfeitosAoAtivarHabilidade(personagem);
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }

    IEnumerator EsperarVFXAliado(IAPersonagemBase personagem, GameObject vfx)
    {
        yield return new WaitForSeconds(tempoDeVfxAliado);
        Destroy(vfx);
        RemoverEfeito(personagem);
    }
}
