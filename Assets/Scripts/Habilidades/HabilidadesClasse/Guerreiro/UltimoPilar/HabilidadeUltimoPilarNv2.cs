using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Guerreiro/Ultimo Pilar/Nv2")]
public class HabilidadeUltimoPilarNv2 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorBonusHP = 0.01f;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            void CurarAoGastarWillPower(int quantidade)
            {
                float cura = personagem.personagem.hp * multiplicadorBonusHP * quantidade;
                personagem.ReceberHP(cura);
            }

            if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }
            var dados = personagem.dadosDasHabilidadesPassivas[this];

            dados.eventosExternos = new List<System.Action<int>>();

            foreach (var aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
            {
                if (aliado.controlador != personagem.controlador)
                {
                    continue;
                }

                System.Action<int> handler = CurarAoGastarWillPower;
                aliado.aoGastarWillPower += handler;
                dados.eventosExternos.Add(handler);
            }
        }     
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            int index = 0;
            foreach (var aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
            {
                if (aliado.controlador != personagem.controlador)
                {
                    continue;
                }
                if (dados.eventosExternos == null || index >= dados.eventosExternos.Count)
                {
                    break;
                }

                aliado.aoGastarWillPower -= dados.eventosExternos[index];
                index++;
            }

            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }
    }
}
