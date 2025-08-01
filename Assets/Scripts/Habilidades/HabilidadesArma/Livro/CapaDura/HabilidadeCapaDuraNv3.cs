using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Livro/Capa Dura/Nv3")]
public class HabilidadeCapaDuraNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorCura = 0.75f;
    public GameObject vfx;
    [SerializeField]
    private GameObject vfxAliado;
    [SerializeField]
    private float tempoDeVfxAliado = 1;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaArma)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade2(personagem, () =>
                {
                    //movimento especial
                    personagem.movimentoEspecial = "Curar";
                    personagem.VerificarComportamento("movimentoEspecial");

                    if (personagem.vfxHabilidadeAtivaArma == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(2, true);
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
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(2, false);
        base.RemoverEfeito(personagem);
    }

    IEnumerator EsperarVFXAliado(IAPersonagemBase personagem, GameObject vfx)
    {
        yield return new WaitForSeconds(tempoDeVfxAliado);
        Destroy(vfx);
        RemoverEfeito(personagem);
    }
}
