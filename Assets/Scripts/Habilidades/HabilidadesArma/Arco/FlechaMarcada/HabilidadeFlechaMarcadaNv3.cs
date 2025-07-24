using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Arco/Flecha Marcada/Nv3")]
public class HabilidadeFlechaMarcadaNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 1.75f;
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaArma)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade2(personagem, () =>
                {
                    IAPersonagemBase alvo = EncontrarInimigoComMaiorHP(personagem);

                    if (alvo == null)
                    {
                        personagem.VerificarComportamento("selecionarAlvo");
                    }
                    else
                    {
                        personagem._personagemAlvo = alvo;
                        personagem._alvoAtual = alvo.transform;
                        personagem.VerificarComportamento("perseguir");
                    }

                    personagem.efeitoPorAtaqueAtivado = true;

                    float danoOriginal = personagem._dano;
                    float distanciaOriginal = personagem.distanciaMinimaParaAtacar;

                    personagem.distanciaMinimaParaAtacar = 100;

                    personagem.AtivarEfeitoPorAtaque("FlechaMarcadaNv3", (bool acerto) =>
                    {
                        if (acerto)
                        {
                            personagem._dano *= multiplicadorDeDano;
                            personagem.StartCoroutine(EsperarFrame(personagem, danoOriginal, distanciaOriginal));
                        }
                        else
                        {
                            RemoverEfeito(personagem);
                        }
                    });

                    if (personagem.vfxHabilidadeAtivaArma == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(2, true);
                    }
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("FlechaMarcadaNv3");
        personagem.GerenciarVFXHabilidade(2, false);
        base.RemoverEfeito(personagem);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem, float dano, float distancia)
    {
        yield return null; //agurada um frame
        personagem._dano = dano;
        RemoverEfeito(personagem);
    }

    private IAPersonagemBase EncontrarInimigoComMaiorHP(IAPersonagemBase personagem)
    {
        IAPersonagemBase inimigoComMaiorHP = null;
        float maiorHP = float.MinValue;

        foreach (IAPersonagemBase inimigo in GameObject.FindObjectsOfType<IAPersonagemBase>())
        {
            if (inimigo != null && inimigo.controlador != personagem.controlador && inimigo._comportamento != EstadoDoPersonagem.MORTO)
            {
                float hpAtual = inimigo.hpAtual;

                if (hpAtual > maiorHP)
                {
                    maiorHP = hpAtual;
                    inimigoComMaiorHP = inimigo;
                }
            }
        }

        return inimigoComMaiorHP;
    }
}
