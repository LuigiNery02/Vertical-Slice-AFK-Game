using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Ladino/Ataque Furtivo/Nv2")]
public class HabilidadeAtaqueFurtivoNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 3;
    [SerializeField]
    private int valorMarcadores = 2;
    public LayerMask layerChao;
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
                    Vector3 novaPosicao;
                    if (EncontrarPosicao(personagem, personagem.transform.position, out novaPosicao))
                    {
                        personagem.transform.position = novaPosicao;

                        float dano = personagem._dano * multiplicadorDeDano;
                        personagem._personagemAlvo.SofrerDano(dano, false, personagem);
                        personagem._personagemAlvo.AtualizarMarcadoresDeAlvo(valorMarcadores, true);
                    }

                    if (personagem.vfxHabilidadeAtivaClasse == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(1, true);
                    }

                    personagem.VerificarComportamento("selecionarAlvo");

                    personagem.StartCoroutine(EsperarVFX(personagem));
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }

    private bool EncontrarPosicao(IAPersonagemBase personagem, Vector3 origem, out Vector3 posicaoValida)
    {
        IAPersonagemBase inimigo = personagem._personagemAlvo;

        Vector3 posicaoTentativa = origem;

        Vector3 direcaoOposta = -inimigo.transform.forward;
        posicaoTentativa = inimigo.transform.position + direcaoOposta * 2f;

        RaycastHit hit;
        if (Physics.Raycast(posicaoTentativa + Vector3.up * 5f, Vector3.down, out hit, 10f, layerChao))
        {
            posicaoValida = hit.point;
            return true;
        }

        posicaoValida = origem;
        return false;
    }

    IEnumerator EsperarVFX(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(1);
        RemoverEfeito(personagem);
    }
}
