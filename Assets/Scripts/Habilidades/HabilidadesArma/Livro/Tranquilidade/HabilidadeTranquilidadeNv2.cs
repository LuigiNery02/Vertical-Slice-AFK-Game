using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Livro/Tranquilidade/Nv2")]
public class HabilidadeTranquilidadeNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float valorSP = 10;
    [SerializeField]
    private float tempoParaRestaurarSP = 1f;
    [SerializeField]
    private float raioCura = 10f;
    public GameObject vfx;
    [SerializeField]
    private GameObject vfxAliado;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
        {
            base.ChecarCastingHabilidade2(personagem, () =>
            {
                var vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position, personagem.transform.rotation, personagem.transform);
                personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
                vfxInstanciado.transform.localScale = (Vector3.one * (raioCura - (raioCura * 0.25f)));

                personagem.StartCoroutine(ChecarCondicaoDeEfeito(personagem, vfxInstanciado));
            });
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(2, false);
        base.RemoverEfeito(personagem);
    }

    private IEnumerator ChecarCondicaoDeEfeito(IAPersonagemBase personagem, GameObject vfx)
    {
        SistemaDeBatalha sistemaDeBatalha = FindObjectOfType<SistemaDeBatalha>();

        while (base.ChecarAtivacao(personagem) && personagem != null && personagem._comportamento != EstadoDoPersonagem.MORTO && !sistemaDeBatalha.fimDeBatalha)
        {
            personagem.GastarSP(custoDeMana);

            Collider[] colliders = Physics.OverlapSphere(personagem.transform.position, raioCura);
            foreach (var col in colliders)
            {
                IAPersonagemBase aliado = col.GetComponent<IAPersonagemBase>();
                if (aliado != null && aliado.controlador == personagem.controlador && aliado._comportamento != EstadoDoPersonagem.MORTO && aliado != personagem)
                {
                    aliado.ReceberSP(valorSP);

                    GameObject vfxAliadoInstanciado = GameObject.Instantiate(vfxAliado, aliado.transform.position, aliado.transform.rotation, aliado.transform);
                    Destroy(vfxAliadoInstanciado, 1f);
                }
            }

            yield return new WaitForSeconds(tempoParaRestaurarSP);
        }

        Destroy(vfx);
        RemoverEfeito(personagem);
    }
}
