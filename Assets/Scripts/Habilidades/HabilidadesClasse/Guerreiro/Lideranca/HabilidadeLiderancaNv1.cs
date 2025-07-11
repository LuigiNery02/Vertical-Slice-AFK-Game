using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Guerreiro/Lideranca/Nv1")]
public class HabilidadeLiderancaNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField] 
    private float bonusDefesas = 1f;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }

            var dados = personagem.dadosDasHabilidadesPassivas[this];

            void AtualizarBonusDefesa(int _ = 0)
            {
                float bonus = personagem.willPower * bonusDefesas;
                RemoverBonusDoGrupo(personagem);
                AplicarBonusAoGrupo(personagem, bonus);
            }

            dados.eventoWillPowerLideranca = AtualizarBonusDefesa;

            personagem.aoReceberWillPower += AtualizarBonusDefesa;
            personagem.aoGastarWillPower += AtualizarBonusDefesa;

            AtualizarBonusDefesa();
        }    
    }


    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (!personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            return;
        }  

        RemoverBonusDoGrupo(personagem);

        if (dados.eventoWillPowerLideranca != null)
        {
            personagem.aoReceberWillPower -= dados.eventoWillPowerLideranca;
            personagem.aoGastarWillPower -= dados.eventoWillPowerLideranca;
        }

        personagem.dadosDasHabilidadesPassivas.Remove(this);
    }

    private void AplicarBonusAoGrupo(IAPersonagemBase personagem, float bonus)
    {
        foreach (var aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
        {
            if (aliado.controlador != personagem.controlador)
            {
                continue;
            }

            if (!aliado.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                aliado.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }
                

            var dados = aliado.dadosDasHabilidadesPassivas[this];

            aliado.defesa += bonus;
            aliado.defesaMagica += bonus;

            dados.valorOriginalDefesa = bonus;
            dados.valorOriginalDefesaMagica = bonus;
            dados.bonusAplicado = true;
        }
    }

    private void RemoverBonusDoGrupo(IAPersonagemBase personagem)
    {
        foreach (var aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
        {
            if (aliado.controlador != personagem.controlador)
            {
                continue;
            }

            if (!aliado.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
            {
                continue;
            }

            aliado.defesa -= dados.valorOriginalDefesa;
            aliado.defesaMagica -= dados.valorOriginalDefesaMagica;

            dados.valorOriginalDefesa = 0;
            dados.valorOriginalDefesaMagica = 0;
            dados.bonusAplicado = false;
        }
    }
}
