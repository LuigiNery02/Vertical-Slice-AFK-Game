using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Guerreiro/Postura de Briga/Nv3")]
public class HabilidadePosturaDeBrigaNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private int willPowerNecessario = 4;
    [SerializeField]
    private float bonusDefesas = 8;
    [SerializeField]
    private float multiplicadorBonusAtaque = 0.08f;

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

            dados.valorOriginalAtaque = personagem.personagem.arma.dano;
            dados.valorMultiplicadoAtaque = (dados.valorOriginalAtaque * multiplicadorBonusAtaque);
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


        dados.valorOriginalAtaque = 0;
        dados.valorMultiplicadoAtaque = 0;

        personagem.dadosDasHabilidadesPassivas.Remove(this);
    }

    private IEnumerator MonitorarCondicao(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (personagem.willPower >= willPowerNecessario)
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

    private void AplicarBonus(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        personagem.defesa += bonusDefesas;
        personagem.defesaMagica += bonusDefesas;

        personagem._dano += dados.valorMultiplicadoAtaque;
    }

    private void RemoverBonus(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        personagem.defesa -= bonusDefesas;
        personagem.defesaMagica -= bonusDefesas;

        personagem._dano -= dados.valorMultiplicadoAtaque;
    }
}
