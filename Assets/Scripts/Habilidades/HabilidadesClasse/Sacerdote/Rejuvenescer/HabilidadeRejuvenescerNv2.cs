using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Sacerdote/Rejuvenescer/Nv2")]
public class HabilidadeRejuvenescerNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorCura = 1f;
    [SerializeField]
    private int numeroDeTicks = 4;
    [SerializeField]
    private float tempoPorTick = 1;
    public GameObject vfx;
    [SerializeField]
    private GameObject vfxAliado;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade1(personagem, () =>
                {
                    if (personagem.vfxHabilidadeAtivaClasse == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(1, true);
                    }

                    IAPersonagemBase aliadoEncontrado = null;

                    foreach (IAPersonagemBase aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
                    {
                        if (aliado != null && aliado.controlador == personagem.controlador && aliado._comportamento != EstadoDoPersonagem.MORTO && aliado != personagem)
                        {
                            aliadoEncontrado = aliado;
                        }
                    }

                    personagem.StartCoroutine(EsperarEfeitoPorTick(personagem, aliadoEncontrado));

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

    IEnumerator EsperarEfeitoPorTick(IAPersonagemBase personagem, IAPersonagemBase aliado)
    {
        float cura = personagem._dano * multiplicadorCura;

        for (int i = 0; i < numeroDeTicks; i++)
        {
            personagem.CurarAliado(personagem, cura);
            GameObject vfxAliadoInstanciado = GameObject.Instantiate(vfxAliado, aliado.transform.position + Vector3.zero, aliado.transform.rotation, aliado.transform);
            yield return new WaitForSeconds(tempoPorTick / 2);
            Destroy(vfxAliadoInstanciado);
            yield return new WaitForSeconds(tempoPorTick / 2);
        }

        RemoverEfeito(personagem);
    }
}
