using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Ladino/Transfusão/Nv1")]
public class HabilidadeTransfusaoNv1 : HabilidadePassiva
{
    [Header("Configurações da Habilidade")]
    [SerializeField]
    private float tempoExtraEfeitosNegativos = 2f;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
            {
                dados = new DadosHabilidadePassiva();
                personagem.dadosDasHabilidadesPassivas[this] = dados;
            }

            if (dados.monitoramento == null)
            {
                if (dados.alvosComBonus == null)
                {
                    dados.alvosComBonus = new HashSet<IAPersonagemBase>();
                }

                dados.monitoramento = personagem.StartCoroutine(MonitorarAlvos(personagem, dados));
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (!personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            return;
        }
            

        if (dados.monitoramento != null)
        {
            personagem.StopCoroutine(dados.monitoramento);
            dados.monitoramento = null;
        }

        if (dados.alvosComBonus != null)
        {
            foreach (var alvo in dados.alvosComBonus)
            {
                if (alvo != null && alvo.tempoDeStun >= tempoExtraEfeitosNegativos)
                {
                    alvo.tempoDeStun -= tempoExtraEfeitosNegativos;
                }
            }
            dados.alvosComBonus.Clear();
        }

        personagem.dadosDasHabilidadesPassivas.Remove(this);
    }

    private IEnumerator MonitorarAlvos(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        while (true)
        {
            var alvo = personagem._personagemAlvo;

            if (alvo != null && alvo.stunado)
            {
                // Verifica se o alvo já tem bônus aplicado
                if (!personagem.bonusStunAplicado.Contains(alvo))
                {
                    alvo.tempoDeStun += tempoExtraEfeitosNegativos;
                    personagem.bonusStunAplicado.Add(alvo);
                    personagem.StartCoroutine(RemoverBonusDepois(alvo, personagem));
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator RemoverBonusDepois(IAPersonagemBase alvo, IAPersonagemBase personagem)
    {
        float tempo = tempoExtraEfeitosNegativos;

        yield return new WaitForSeconds(tempo);

        if (personagem.bonusStunAplicado.Contains(alvo))
        {
            if (alvo.tempoDeStun >= tempo)
                alvo.tempoDeStun -= tempo;

            personagem.bonusStunAplicado.Remove(alvo);
        }
    }

}
