using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Sacerdote/Benção Divina/Nv1")]
public class HabilidadeBencaoDivinaNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorCura = 1.5f;
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
                    if (personagem.vfxHabilidadeAtivaClasse == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(1, true);
                    }

                    IAPersonagemBase aliadoComMenorHP = null;
                    float menorHP = 9999999999999;

                    foreach (IAPersonagemBase aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
                    {
                        if (aliado != null && aliado.controlador == personagem.controlador && aliado._comportamento != EstadoDoPersonagem.MORTO && aliado != personagem)
                        {
                            float hpAtual = aliado.hpAtual;

                            if (hpAtual < menorHP)
                            {
                                menorHP = hpAtual;
                                aliadoComMenorHP = aliado;
                            }
                        }
                    }

                    float cura = aliadoComMenorHP._dano * multiplicadorCura;
                    personagem.CurarAliado(aliadoComMenorHP, cura);

                    GameObject vfxAliadoInstanciado = GameObject.Instantiate(vfxAliado, aliadoComMenorHP.transform.position + Vector3.zero, aliadoComMenorHP.transform.rotation, aliadoComMenorHP.transform);
                    personagem.StartCoroutine(EsperarVFXAliado(personagem, vfxAliadoInstanciado));

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
