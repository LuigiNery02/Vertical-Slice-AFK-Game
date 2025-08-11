using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Sacerdote/Fonte de Luz/Nv1")]
public class HabilidadeFonteDeLuzNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorReducaoCooldownAliados = 0.1f;
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
                        if (aliado.habilidadeAtivaClasse != null && aliado.habilidadeAtivaClasse.tempoDeRecarga > 0)
                        {
                            float reducaoCooldown = aliado.habilidadeAtivaClasse.tempoDeRecarga * multiplicadorReducaoCooldownAliados;

                            if (!dados.cooldownsAliados.ContainsKey(aliado.habilidadeAtivaClasse))
                            {
                                dados.cooldownsAliados[aliado.habilidadeAtivaClasse] = reducaoCooldown;
                            }
                            
                            aliado.habilidadeAtivaClasse.tempoDeRecarga -= reducaoCooldown;
                        }

                        if (aliado.habilidadeAtivaArma != null && aliado.habilidadeAtivaArma.tempoDeRecarga > 0)
                        {
                            float reducaoCooldown = aliado.habilidadeAtivaArma.tempoDeRecarga * multiplicadorReducaoCooldownAliados;

                            if (!dados.cooldownsAliados.ContainsKey(aliado.habilidadeAtivaArma))
                            {
                                dados.cooldownsAliados[aliado.habilidadeAtivaArma] = reducaoCooldown;
                            }

                            aliado.habilidadeAtivaArma.tempoDeRecarga -= reducaoCooldown;
                        }

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
                        if (aliado.habilidadeAtivaClasse != null && aliado.habilidadeAtivaClasse.tempoDeRecarga > 0 && dados.cooldownsAliados.TryGetValue(aliado.habilidadeAtivaClasse, out float reducaoCooldownClasse))
                        {
                            aliado.habilidadeAtivaClasse.tempoDeRecarga += reducaoCooldownClasse;
                        }

                        if (aliado.habilidadeAtivaArma != null && aliado.habilidadeAtivaArma.tempoDeRecarga > 0 && dados.cooldownsAliados.TryGetValue(aliado.habilidadeAtivaArma, out float reducaoCooldownArma))
                        {
                            aliado.habilidadeAtivaArma.tempoDeRecarga += reducaoCooldownArma;
                        }
                    }
                }

                personagem.dadosDasHabilidadesPassivas.Remove(this);
            }
        }
    }
}
