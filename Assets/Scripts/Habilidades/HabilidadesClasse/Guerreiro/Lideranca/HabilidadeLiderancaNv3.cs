using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Guerreiro/Lideranca/Nv3")]
public class HabilidadeLiderancaNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorBonusAtaque = 0.02f;
    [SerializeField]
    private float multiplicadorBonusHP = 0.1f;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }

            var dados = personagem.dadosDasHabilidadesPassivas[this];

            void AtualizarBonusAtaque(int _ = 0)
            {
                float multiplicador = multiplicadorBonusAtaque;
                float bonusTotal = personagem.willPower * multiplicador;

                RemoverBonusDoGrupo(personagem);
                AplicarBonusAoGrupo(personagem, bonusTotal);
            }

            dados.eventoWillPowerLideranca = AtualizarBonusAtaque;

            personagem.aoReceberWillPower += AtualizarBonusAtaque;
            personagem.aoGastarWillPower += AtualizarBonusAtaque;

            AtualizarBonusAtaque();
            AplicarBonusHPAoGrupo(personagem);
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (!personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            return;
        }

        RemoverBonusDoGrupo(personagem);
        RemoverBonusHPDoGrupo(personagem);

        if (dados.eventoWillPowerLideranca != null)
        {
            personagem.aoReceberWillPower -= dados.eventoWillPowerLideranca;
            personagem.aoGastarWillPower -= dados.eventoWillPowerLideranca;
        }

        personagem.dadosDasHabilidadesPassivas.Remove(this);
    }

    private void AplicarBonusAoGrupo(IAPersonagemBase personagem, float multiplicador)
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

            float bonus = aliado.personagem.arma.dano * multiplicador;

            aliado._dano += bonus;

            dados.valorOriginalAtaque = bonus;
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

            aliado._dano -= dados.valorOriginalAtaque;

            dados.valorOriginalAtaque = 0;
            dados.bonusAplicado = false;
        }
    }

    private void AplicarBonusHPAoGrupo(IAPersonagemBase personagem)
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

            float bonusHP = aliado.personagem.hp * multiplicadorBonusHP;
            aliado._hpMaximoEInicial += bonusHP;
            dados.valorOriginalHP = bonusHP;
        }
    }

    private void RemoverBonusHPDoGrupo(IAPersonagemBase personagem)
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

            aliado._hpMaximoEInicial -= dados.valorOriginalHP;
            dados.valorOriginalHP = 0;
        }
    }
}
