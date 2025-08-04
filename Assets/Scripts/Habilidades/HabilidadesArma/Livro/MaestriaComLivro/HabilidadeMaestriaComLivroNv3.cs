using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Livro/Maestria com Livro/Nv3")]

public class HabilidadeMaestriaComLivroNv3 : HabilidadePassiva
{
    [SerializeField]
    private float bonusRecuperacao = 0.3f;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadePassivaArma)
        {
            if (base.ChecarRuna(personagem, nivel))
            {
                foreach (IAPersonagemBase aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
                {
                    if (aliado != null && aliado.controlador == personagem.controlador && aliado._comportamento != EstadoDoPersonagem.MORTO)
                    {
                        if (!aliado.dadosDasHabilidadesPassivas.ContainsKey(this))
                        {
                            aliado.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
                        }

                        var dados = aliado.dadosDasHabilidadesPassivas[this];

                        if (!dados.bonusAplicado)
                        {
                            float bonusSP = aliado.multiplicadorBonusRecuperacaoSP * bonusRecuperacao;

                            aliado.multiplicadorBonusRecuperacaoSP += bonusSP;

                            dados.bonusMultiplicadorRecuperacaoSP = bonusSP;
                            dados.bonusAplicado = true;
                        }
                    }
                }
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        foreach (IAPersonagemBase aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
        {
            if (aliado != null && aliado.controlador == personagem.controlador)
            {
                if (aliado.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
                {
                    if (dados.bonusAplicado)
                    {
                        aliado.multiplicadorBonusRecuperacaoSP -= dados.bonusMultiplicadorRecuperacaoSP;
                    }

                    aliado.dadosDasHabilidadesPassivas.Remove(this);
                }
            }
        }
    }
}
