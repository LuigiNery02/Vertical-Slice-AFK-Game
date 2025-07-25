using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Ladino/Instinto Predatório/Nv3")]
public class HabilidadeInstintoPredatorioNv3 : HabilidadePassiva
{
    [Header("Configurações da Habilidade")]
    [SerializeField]
    private float multiplicadorDano = 0.15f;
    [SerializeField]
    private float valorHpInimigo = 0.6f;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }

            var dados = personagem.dadosDasHabilidadesPassivas[this];
            dados.monitoramento = personagem.StartCoroutine(MonitorarCondicao(personagem, dados));
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
        {
            return;
        }

        var dados = personagem.dadosDasHabilidadesPassivas[this];

        if (dados.monitoramento != null)
        {
            personagem.StopCoroutine(dados.monitoramento);
        }

        if (dados.bonusAplicado)
        {
            RemoverBonus(personagem, dados);
        }

        personagem.dadosDasHabilidadesPassivas.Remove(this);
    }

    private IEnumerator MonitorarCondicao(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            var alvo = personagem._personagemAlvo;

            if (alvo != null)
            {
                float hpEfeito = alvo._hpMaximoEInicial * valorHpInimigo;

                if (alvo != null && alvo.hpAtual <= hpEfeito)
                {
                    if (!dados.bonusAplicado)
                    {
                        AplicarBonus(personagem, dados);
                        dados.bonusAplicado = true;
                    }
                }
                else
                {
                    if (dados.bonusAplicado)
                    {
                        RemoverBonus(personagem, dados);
                        dados.bonusAplicado = false;
                    }
                }
            }  
        }
    }

    private void AplicarBonus(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        dados.valorOriginalAtaque = personagem._dano;
        float bonus = personagem._dano * multiplicadorDano;
        personagem._dano += bonus;
        dados.valorMultiplicadoAtaque = bonus;
    }

    private void RemoverBonus(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        personagem._dano -= dados.valorMultiplicadoAtaque;
    }
}
