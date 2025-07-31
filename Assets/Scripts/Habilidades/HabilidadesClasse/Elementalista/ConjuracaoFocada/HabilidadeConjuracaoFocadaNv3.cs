using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Elementalista/Conjuração Focada/Nv3")]
public class HabilidadeConjuracaoFocadaNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float chanceDeReducaoDeCastFixo = 0.05f;
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
                void CalcularChanceDeReducaoDeCastFixo(HabilidadeAtiva habilidadeAtiva)
                {
                    if(!dados.bonusAplicado)
                    {
                        if (Random.value <= chanceDeReducaoDeCastFixo)
                        {
                            if (habilidadeAtiva == personagem.habilidadeAtivaClasse)
                            {
                                habilidadeAtiva.reducaoCastFixo += habilidadeAtiva.castFixo;
                                dados.buffCastFixoAtivaClasse = habilidadeAtiva.castFixo;
                            }
                            else if (habilidadeAtiva == personagem.habilidadeAtivaArma)
                            {
                                habilidadeAtiva.reducaoCastFixo += habilidadeAtiva.castFixo;
                                dados.buffCastFixoAtivaArma = habilidadeAtiva.castFixo;
                            }

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
                        }
                    }
                }
                dados.eventoConjuracaoFocada = CalcularChanceDeReducaoDeCastFixo;
                personagem.aoConjurarHabilidade += CalcularChanceDeReducaoDeCastFixo;
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            if (dados.eventoConjuracaoFocada != null)
            {
                if (personagem.habilidadeAtivaClasse != null)
                {
                    personagem.habilidadeAtivaClasse.reducaoCastFixo -= dados.buffCastFixoAtivaClasse;
                }

                if (personagem.habilidadeAtivaArma != null)
                {
                    personagem.habilidadeAtivaArma.reducaoCastFixo -= dados.buffCastFixoAtivaArma;
                }

                personagem.aoConjurarHabilidade -= dados.eventoConjuracaoFocada;
            }

            dados.bonusAplicado = false;
            personagem.dadosDasHabilidadesPassivas.Remove(this);
            personagem.GerenciarVFXHabilidade(3, false);
        }
    }

    IEnumerator EsperarTempoDeVfx(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(tempoDeVfx);
        personagem.GerenciarVFXHabilidade(3, false);
    }
}
