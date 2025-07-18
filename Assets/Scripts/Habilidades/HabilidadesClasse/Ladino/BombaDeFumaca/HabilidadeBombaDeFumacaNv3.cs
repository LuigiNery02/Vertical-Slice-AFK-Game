using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Ladino/Bomba de Fumaça/Nv3")]
public class HabilidadeBombaDeFumacaNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    public LayerMask layerChao;
    public float raioAtordoamento = 3;
    public float tempoDeStun = 1;
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
                    CancelarFocoDosInimigos(personagem);

                    if (personagem.personagem.arma.armaDano == TipoDeDano.DANO_MELEE)
                    {
                        AplicarAtordoamento(personagem);
                    }

                    Vector3 posOrigem = personagem.transform.position;
                    Vector3 novaPosicao;

                    if (EncontrarPosicao(personagem, posOrigem, out novaPosicao))
                    {
                        personagem.transform.position = novaPosicao;

                        if (personagem.personagem.arma.armaDano == TipoDeDano.DANO_RANGED || personagem.personagem.arma.armaDano == TipoDeDano.DANO_MAGICO)
                        {
                            AplicarAtordoamento(personagem);
                        }
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

    private void CancelarFocoDosInimigos(IAPersonagemBase personagem)
    {
        IAPersonagemBase[] todosAliados = GameObject.FindObjectsOfType<IAPersonagemBase>();
        IAPersonagemBase[] todosInimigos = GameObject.FindObjectsOfType<IAPersonagemBase>();
        foreach (var inimigo in todosInimigos)
        {
            if (inimigo.controlador != personagem.controlador)
            {
                if (inimigo._personagemAlvo == personagem)
                {
                    IAPersonagemBase novoAlvo = null;

                    foreach (var alvo in todosAliados)
                    {
                        if (alvo != personagem && alvo.controlador == personagem.controlador)
                        {
                            novoAlvo = alvo;
                            break;
                        }
                    }

                    if (novoAlvo == null)
                    {
                        inimigo._personagemAlvo.VerificarComportamento("selecionarAlvo");
                    }
                    else
                    {
                        inimigo._personagemAlvo = novoAlvo;
                        inimigo._alvoAtual = novoAlvo.transform;
                    }
                }
            }
        }
    }

    private bool EncontrarPosicao(IAPersonagemBase personagem, Vector3 origem, out Vector3 posicaoValida)
    {
        IAPersonagemBase inimigoMaisProximo = null;
        float menorDistancia = float.MaxValue;

        foreach (var inimigo in GameObject.FindObjectsOfType<IAPersonagemBase>())
        {
            if (inimigo.controlador != personagem.controlador)
            {
                float distancia = Vector3.Distance(origem, inimigo.transform.position);
                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    inimigoMaisProximo = inimigo;
                }
            }
        }

        if (inimigoMaisProximo == null)
        {
            posicaoValida = origem;
            return false;
        }

        Vector3 posicaoTentativa = origem;

        if (personagem.personagem.arma.armaDano == TipoDeDano.DANO_MELEE)
        {
            Vector3 direcaoOposta = -inimigoMaisProximo.transform.forward;
            posicaoTentativa = inimigoMaisProximo.transform.position + direcaoOposta * 2f;
        }
        else if (personagem.personagem.arma.armaDano == TipoDeDano.DANO_RANGED || personagem.personagem.arma.armaDano == TipoDeDano.DANO_MAGICO)
        {
            Vector3 direcaoParaLonge = (origem - inimigoMaisProximo.transform.position).normalized;
            posicaoTentativa = origem + direcaoParaLonge * 5f;
        }

        RaycastHit hit;
        if (Physics.Raycast(posicaoTentativa + Vector3.up * 5f, Vector3.down, out hit, 10f, layerChao))
        {
            posicaoValida = hit.point;
            return true;
        }

        posicaoValida = origem;
        return false;
    }

    private void AplicarAtordoamento(IAPersonagemBase personagem)
    {
        Vector3 pontoDeReferencia = personagem.transform.position;

        foreach (var inimigo in GameObject.FindObjectsOfType<IAPersonagemBase>())
        {
            if (inimigo != personagem && inimigo.controlador != personagem.controlador)
            {
                float distancia = Vector3.Distance(inimigo.transform.position, pontoDeReferencia);
                if (distancia <= raioAtordoamento)
                {
                    inimigo.tempoDeStun = tempoDeStun;
                    inimigo.VerificarComportamento("stun");
                }
            }
        }
    }



    IEnumerator EsperarVFX(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(1);
        RemoverEfeito(personagem);
    }
}
