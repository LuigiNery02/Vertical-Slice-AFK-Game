using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Ladino/Transfusão/Nv3")]
public class HabilidadeTransfusaoNv3 : HabilidadePassiva
{
    [Header("Configurações da Habilidade")]
    [SerializeField]
    private float multiplicadorEfeitosNegativos = 0.2f;

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
            }

            IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

            foreach (var aliado in personagens)
            {
                if (aliado.controlador == personagem.controlador)
                {
                    if (!dados.alvosComBonus.Contains(aliado))
                    {
                        aliado.multiplicadorEfeitosPositivosParaEfeitosNegativos += multiplicadorEfeitosNegativos;
                        dados.alvosComBonus.Add(aliado);
                    }
                }
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            foreach (var aliado in dados.alvosComBonus)
            {
                aliado.multiplicadorEfeitosPositivosParaEfeitosNegativos -= multiplicadorEfeitosNegativos;
            }

            dados.alvosComBonus.Clear();
        }
    }
}
