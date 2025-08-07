using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Sacerdote/Marca Sagrada/Nv1")]
public class HabilidadeMarcaSagradaNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDanoMarcado = 0.1f;
    public GameObject vfx;
    [SerializeField]
    private GameObject vfxAlvo;

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

                    if(personagem._personagemAlvo != null)
                    {
                        IAPersonagemBase alvo = personagem._personagemAlvo;
                        alvo.marcado = true;
                        alvo.multiplicadorDanoMarcado += multiplicadorDanoMarcado;

                        GameObject vfxAlvoInstanciado = GameObject.Instantiate(vfxAlvo, alvo.transform.position + Vector3.zero, alvo.transform.rotation, alvo.transform);
                        personagem.StartCoroutine(EsperarTempoDeEfeito(personagem, alvo ,vfxAlvoInstanciado));
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

    IEnumerator EsperarTempoDeEfeito(IAPersonagemBase personagem, IAPersonagemBase alvo, GameObject vfx)
    {
        yield return new WaitForSeconds(tempoDeEfeito);
        alvo.marcado = false;
        alvo.multiplicadorDanoMarcado -= multiplicadorDanoMarcado;
        Destroy(vfx);
        RemoverEfeito(personagem);
    }
}
