using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Sacerdote/Fonte de Luz/Nv3")]
public class HabilidadeFonteDeLuzNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDanoAliados = 0.1f;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }

            var dados = personagem.dadosDasHabilidadesPassivas[this];

            if (dados.alvosComBonus == null)
            {
                dados.alvosComBonus = new HashSet<IAPersonagemBase>();
            }

            IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

            foreach (var aliado in personagens)
            {
                if (aliado.controlador == personagem.controlador && aliado != personagem)
                {
                    if (!dados.alvosComBonus.Contains(aliado))
                    {
                        float dano = aliado._dano * multiplicadorDanoAliados;

                        if (!dados.danosAliados.ContainsKey(aliado))
                        {
                            dados.danosAliados[aliado] = dano;
                        }

                        aliado._dano += dano;

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
            if (dados.alvosComBonus != null)
            {
                foreach (var aliado in dados.alvosComBonus)
                {
                    if (aliado != null)
                    {
                        if (dados.danosAliados.TryGetValue(aliado, out float dano))
                        {
                            aliado._dano -= dano;
                        }
                    }
                }
            }

            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }
    }
}
