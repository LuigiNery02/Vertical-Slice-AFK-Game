using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Guerreiro/Postura de Briga/Nv1")]

public class HabilidadePosturaDeBrigaNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float bonusDefesas = 5;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
        {
            personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
        }

        var dados = personagem.dadosDasHabilidadesPassivas[this];
        dados.monitoramento = personagem.StartCoroutine(MonitorarCondicao(personagem, dados));
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
            RemoverBonus(personagem);
        }

        personagem.dadosDasHabilidadesPassivas.Remove(this);
    }

    private IEnumerator MonitorarCondicao(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            if (personagem.willPower >= 8)
            {
                if (!dados.bonusAplicado)
                {
                    AplicarBonus(personagem);
                    dados.bonusAplicado = true;
                }
            }
            else
            {
                if (dados.bonusAplicado)
                {
                    RemoverBonus(personagem);
                    dados.bonusAplicado = false;
                }
            }
        }
    }

    private void AplicarBonus(IAPersonagemBase personagem)
    {
        personagem.defesa += bonusDefesas;
        personagem.defesaMagica += bonusDefesas;
    }

    private void RemoverBonus(IAPersonagemBase personagem)
    {
        personagem.defesa -= bonusDefesas;
        personagem.defesaMagica -= bonusDefesas;
    }
}
