using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Ladino/Bomba de Fumaça/Nv1")]
public class HabilidadeBombaDeFumacaNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    public float distanciaMaximaTeleporte = 5f;
    public int tentativasMaximas = 10;
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
                    CancelarFocoDosInimigos(personagem);

                    Vector3 novaPosicao;
                    if (EncontrarPosicaoAleatoriaProxima(personagem.transform.position, out novaPosicao))
                    {
                        personagem.transform.position = novaPosicao;
                    }

                    if (personagem.vfxHabilidadeAtivaClasse == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
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
            if(inimigo.controlador != personagem.controlador)
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

    private bool EncontrarPosicaoAleatoriaProxima(Vector3 origem, out Vector3 posicaoValida)
    {
        for (int i = 0; i < tentativasMaximas; i++)
        {
            Vector2 offset2D = Random.insideUnitCircle * distanciaMaximaTeleporte;
            Vector3 tentativa = origem + new Vector3(offset2D.x, 5f, offset2D.y); // 5f de altura para raycast para baixo

            if (Physics.Raycast(tentativa, Vector3.down, out RaycastHit hit, 10f, layerChao))
            {
                posicaoValida = hit.point;
                return true;
            }
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
