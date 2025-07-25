using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Punho da Disciplina/Nv3")]
public class HabilidadePunhoDaDisciplinaNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 1.5f;
    [SerializeField]
    private float multiplicadorDiminuicaoDeDanoInimigo = 0.8f;
    [SerializeField]
    private float tempoDiminuicaoDeDanoInimigo = 4f;
    public GameObject vfx;
    public GameObject vfxInimigo;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade1(personagem, () =>
                {
                    personagem.efeitoPorAtaqueAtivado = true;

                    Dictionary<IAPersonagemBase, Coroutine> debuffsAtivos = new();
                    Dictionary<IAPersonagemBase, float> valoresSubtraidos = new();

                    personagem.AtivarEfeitoPorAtaque("PunhoDaDisciplinaNv3", (bool acerto) =>
                    {
                        if (acerto)
                        {
                            IAPersonagemBase inimigo = personagem._personagemAlvo;

                            float danoFinal = multiplicadorDeDano;
                            float debuffFinal = multiplicadorDiminuicaoDeDanoInimigo;
                            float tempoFinal = tempoDiminuicaoDeDanoInimigo;

                            if (inimigo != null && inimigo.VerificarEfeitoNegativo())
                            {
                                danoFinal *= 2;
                                debuffFinal *= debuffFinal;
                                tempoFinal *= 2;
                            }

                            personagem._dano *= danoFinal;

                            if (inimigo != null)
                            {
                                if (debuffsAtivos.TryGetValue(inimigo, out var coroutine))
                                {
                                    personagem.StopCoroutine(coroutine);

                                    if (valoresSubtraidos.TryGetValue(inimigo, out float valorAnterior))
                                    {
                                        inimigo._dano += valorAnterior;
                                    }
                                }

                                float valorReduzido = inimigo._dano * debuffFinal;
                                inimigo._dano -= valorReduzido;
                                inimigo.recebeuDebuffPunhoDisciplina = true;
                                inimigo.ataqueDiminuido = true;

                                valoresSubtraidos[inimigo] = valorReduzido;

                                Coroutine novaCorrotina = personagem.StartCoroutine(TempoEfeitoDiminuirDano(inimigo, tempoFinal, valorReduzido, () =>
                                {
                                    valoresSubtraidos.Remove(inimigo);
                                    debuffsAtivos.Remove(inimigo);
                                }));

                                debuffsAtivos[inimigo] = novaCorrotina;
                            }

                            float danoOriginal = personagem._dano;
                            personagem.StartCoroutine(EsperarFrame(personagem, danoOriginal));
                        }
                        else
                        {
                            RemoverEfeito(personagem);
                        }
                    });


                    if (personagem.vfxHabilidadeAtivaClasse == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(1, true);
                    }
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("PunhoDaDisciplinaNv3");
        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }

    private IEnumerator EsperarFrame(IAPersonagemBase personagem, float dano)
    {
        yield return null;
        personagem._dano = dano;
        RemoverEfeito(personagem);
    }

    private IEnumerator TempoEfeitoDiminuirDano(IAPersonagemBase inimigo, float tempo, float valorReduzido, System.Action aoFinalizar)
    {
        GameObject vfxInstanciado = null;
        if (vfxInimigo != null)
        {
            vfxInstanciado = GameObject.Instantiate(vfxInimigo, inimigo.transform.position, inimigo.transform.rotation, inimigo.transform);
        }

        yield return new WaitForSeconds(tempo);

        if (inimigo != null)
        {
            inimigo._dano += valorReduzido;
            inimigo.recebeuDebuffPunhoDisciplina = false;
            inimigo.ataqueDiminuido = false;
        }

        if (vfxInstanciado != null)
        {
            GameObject.Destroy(vfxInstanciado);
        }

        aoFinalizar?.Invoke();
    }
}
