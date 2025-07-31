using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Livro/Segredos do Sangue/Nv3")]
public class HabilidadeSegredosDoSangueNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float reducaoHP = 0.1f;
    [SerializeField]
    private float multiplicadorSP = 0.1f;
    [SerializeField]
    private float tempoParaRestaurarSP = 1f;
    [SerializeField]
    private float raioCura = 10f;
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
                    float hpPerdido = personagem._hpMaximoEInicial * reducaoHP;
                    personagem.SofrerDano(hpPerdido, false, personagem);

                    var vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position, personagem.transform.rotation, personagem.transform);
                    personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
                    vfxInstanciado.transform.localScale = (Vector3.one * (raioCura - (raioCura * 0.25f)));

                    personagem.StartCoroutine(ChecarCondicaoDeEfeito(personagem, vfxInstanciado));
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(2, false);
        base.RemoverEfeito(personagem);
    }

    private IEnumerator ChecarCondicaoDeEfeito(IAPersonagemBase personagem, GameObject vfx)
    {
        while (base.ChecarAtivacao(personagem) && personagem != null && personagem._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem.GastarSP(custoDeMana);

            Collider[] colliders = Physics.OverlapSphere(personagem.transform.position, raioCura);
            foreach (var col in colliders)
            {
                IAPersonagemBase aliado = col.GetComponent<IAPersonagemBase>();
                if (aliado != null && aliado.controlador == personagem.controlador && aliado._comportamento != EstadoDoPersonagem.MORTO)
                {
                    float spRestaurado = aliado._spMaximoEInicial * multiplicadorSP;
                    aliado.ReceberSP(spRestaurado);

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
