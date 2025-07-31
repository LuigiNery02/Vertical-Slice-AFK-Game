using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Elementalista/Arco Elemental/Nv2")]
public class HabilidadeArcoElementalNv2 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField] private float buffCastFixo = 0.2f;
    [SerializeField] private float buffMaximo = 0.3f;
    public GameObject vfx;
    [SerializeField] private float tempoDeVfx = 2;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }

            var dados = personagem.dadosDasHabilidadesPassivas[this];

            if (dados.habilidadesBuffadas == null)
            {
                dados.habilidadesBuffadas = new Dictionary<HabilidadeAtiva, float>();
            }

            if (dados.eventoArcoElemental == null)
            {
                void CalcularElementos(HabilidadeAtiva habilidadeAtiva)
                {
                    if (dados.elemento == habilidadeAtiva.elemento)
                    {
                        dados.elemento = habilidadeAtiva.elemento;
                        dados.sequencia = 1;
                        return;
                    }

                    if (habilidadeAtiva.elemento != dados.elemento)
                    {
                        dados.sequencia++;
                        dados.elemento = habilidadeAtiva.elemento;
                    }
                    int elementosConsiderados = Mathf.Min(dados.sequencia, Mathf.FloorToInt(buffMaximo / buffCastFixo));

                    if (elementosConsiderados < 2)
                    {
                        return;
                    }

                    float bonusPercentual = Mathf.Min(elementosConsiderados * buffCastFixo, buffMaximo);

                    if (!dados.habilidadesBuffadas.ContainsKey(habilidadeAtiva))
                    {

                        float bonusValor = habilidadeAtiva.castFixo * bonusPercentual;
                        habilidadeAtiva.reducaoCastFixo += bonusValor;
                        dados.habilidadesBuffadas[habilidadeAtiva] = bonusValor;

                        if (personagem.vfxHabilidadePassivaClasse == null)
                        {
                            var vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position, personagem.transform.rotation, personagem.transform);
                            personagem.vfxHabilidadePassivaClasse = vfxInstanciado;
                            personagem.StartCoroutine(EsperarTempoDeVfx(personagem));
                        }
                        else
                        {
                            personagem.GerenciarVFXHabilidade(3, true);
                            personagem.StartCoroutine(EsperarTempoDeVfx(personagem));
                        }
                    }
                }
                dados.eventoArcoElemental = CalcularElementos;
                personagem.aoConjurarHabilidade += CalcularElementos;
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            if (dados.eventoArcoElemental != null)
            {
                personagem.aoConjurarHabilidade -= dados.eventoArcoElemental;
            }

            if (dados.eventoRemocaoBuff != null)
            {
                personagem.aoConjurarHabilidade -= dados.eventoRemocaoBuff;
            }

            if (dados.habilidadesBuffadas != null)
            {
                foreach (var kvp in dados.habilidadesBuffadas)
                {
                    kvp.Key.reducaoCastFixo -= kvp.Value;
                }
                dados.habilidadesBuffadas.Clear();
            }

            dados.bonusAplicado = false;
            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }
    }

    IEnumerator EsperarTempoDeVfx(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(tempoDeVfx);
        personagem.GerenciarVFXHabilidade(3, false);
    }
}
