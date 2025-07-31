using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Elementalista/Nevasca/Nv1")]
public class HabilidadeNevascaNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeAtaque = 3;
    [SerializeField]
    private float raioVfx = 10;
    [SerializeField]
    private float tempoDeVfx = 3;
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade1(personagem, () =>
                {
                    //movimento especial
                    personagem.movimentoEspecial = "LancaElemento";
                    personagem.VerificarComportamento("movimentoEspecial");

                    personagem.StartCoroutine(Nevasca(personagem));
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }

    IEnumerator Nevasca(IAPersonagemBase personagem)
    {
        float dano = personagem._dano * multiplicadorDeAtaque;

        Vector3 posicaoAlvo = personagem._personagemAlvo.transform.position;

        GameObject vfxInstanciado = GameObject.Instantiate(vfx, posicaoAlvo, Quaternion.identity);
        personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
        vfxInstanciado.transform.localScale = (Vector3.one * raioVfx) / 9;

        Collider[] colliders = Physics.OverlapSphere(posicaoAlvo, raioVfx / 2f);

        foreach (var collider in colliders)
        {
            IAPersonagemBase inimigo = collider.GetComponent<IAPersonagemBase>();

            if (inimigo != null && inimigo.controlador != personagem.controlador && inimigo._comportamento != EstadoDoPersonagem.MORTO)
            {
                inimigo.SofrerDano(dano, false, personagem);
                if (personagem.cargasDeGelo != 0 || personagem.cargasDeFogo != 0 || personagem.cargasDeRaio != 0)
                {
                    inimigo.CausarEfeitoCargasElementais(personagem.cargasDeGelo, personagem.cargasDeFogo, personagem.cargasDeRaio, personagem._dano);
                }
                personagem.AtualizarCargasElementais("gelo");
            }
        }

        yield return new WaitForSeconds(tempoDeVfx);

        RemoverEfeito(personagem);
    }
}
