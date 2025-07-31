using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Elementalista/Corrente Arcana/Nv3")]
public class HabilidadeCorrenteArcanaNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private int conjuracoesNecessarias = 3;
    [SerializeField]
    private float multiplicacaoDeDano = 0.1f;
    public GameObject vfx;
    [SerializeField]
    private float tempoDeVfx = 2;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }

            var dados = personagem.dadosDasHabilidadesPassivas[this];

            if (!dados.bonusAplicado)
            {
                void CalcularElementos(HabilidadeAtiva habilidadeAtiva)
                {
                    if (dados.elemento == habilidadeAtiva.elemento)
                    {
                        dados.sequencia++;
                    }
                    else
                    {
                        dados.sequencia = 1;
                        dados.elemento = habilidadeAtiva.elemento;
                    }

                    if (dados.sequencia >= conjuracoesNecessarias)
                    {
                        dados.buffProximaHabilidade = true;
                        dados.sequencia = 0;

                        dados.bonusAplicado = true;

                        if (personagem.vfxHabilidadePassivaClasse == null)
                        {
                            GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                            personagem.vfxHabilidadePassivaClasse = vfxInstanciado;
                            personagem.StartCoroutine(EsperarTempoDeVfx(personagem));
                        }
                        else
                        {
                            personagem.GerenciarVFXHabilidade(3, true);
                            personagem.StartCoroutine(EsperarTempoDeVfx(personagem));
                        }

                        personagem.spSemCusto = true;
                        personagem.aoConjurarHabilidade += AplicarAumentoDeDano;
                        personagem.aoConjurarHabilidade -= CalcularElementos;
                    }
                }

                void AplicarAumentoDeDano(HabilidadeAtiva habilidadeAtiva)
                {
                    if (dados.buffProximaHabilidade)
                    {
                        dados.buffProximaHabilidade = false;

                        dados.valorOriginalAtaque = personagem._dano;
                        float bonus = personagem._dano * multiplicacaoDeDano;
                        personagem._dano += bonus;
                        dados.valorMultiplicadoAtaque = bonus;

                        personagem.StartCoroutine(RemoverBonusDepoisDaExecucao(personagem));

                        personagem.aoConjurarHabilidade -= AplicarAumentoDeDano;
                        personagem.spSemCusto = false;
                        personagem.aoConjurarHabilidade += CalcularElementos;
                    }

                }

                dados.eventoCorrenteArcana = CalcularElementos;
                dados.evento2CorrenteArcana = AplicarAumentoDeDano;

                personagem.aoConjurarHabilidade += CalcularElementos;

                dados.bonusAplicado = true;
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            if (dados.eventoCorrenteArcana != null)
            {
                personagem.aoConjurarHabilidade -= dados.eventoCorrenteArcana;
            }

            if (dados.evento2CorrenteArcana != null)
            {
                personagem.aoConjurarHabilidade -= dados.evento2CorrenteArcana;
            }

            if (dados.valorMultiplicadoAtaque != 0)
            {
                personagem._dano -= dados.valorMultiplicadoAtaque;
            }

            dados.bonusAplicado = false;
            personagem.dadosDasHabilidadesPassivas.Remove(this);
            personagem.GerenciarVFXHabilidade(3, false);
            personagem.spSemCusto = false;
        }
    }
    IEnumerator EsperarTempoDeVfx(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(tempoDeVfx);
        personagem.GerenciarVFXHabilidade(3, false);
    }

    IEnumerator RemoverBonusDepoisDaExecucao(IAPersonagemBase personagem)
    {
        yield return null;

        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            personagem._dano -= dados.valorMultiplicadoAtaque;
            dados.valorMultiplicadoAtaque = 0;
        }
    }
}
