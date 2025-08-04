using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Livro/Afeto/Nv2")]
public class HabilidadeAfetoNv2 : HabilidadePassiva
{
    [SerializeField]
    private float bonusRecuperacao = 0.15f;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadePassivaArma)
        {
            if (base.ChecarRuna(personagem, nivel))
            {
                if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
                {
                    personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
                }

                var dados = personagem.dadosDasHabilidadesPassivas[this];

                if (!dados.bonusAplicado)
                {
                    float bonusHP = personagem.multiplicadorBonusRecuperacaoHP * bonusRecuperacao;
                    float bonusSP = personagem.multiplicadorBonusRecuperacaoSP * bonusRecuperacao;

                    personagem.multiplicadorBonusRecuperacaoHP += bonusHP;
                    personagem.multiplicadorBonusRecuperacaoSP += bonusSP;

                    dados.bonusMultiplicadorRecuperacaoHP = bonusHP;
                    dados.bonusMultiplicadorRecuperacaoSP = bonusSP;
                    dados.bonusAplicado = true;
                }
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            if (dados.bonusAplicado)
            {
                personagem.multiplicadorBonusRecuperacaoHP -= dados.bonusMultiplicadorRecuperacaoHP;
                personagem.multiplicadorBonusRecuperacaoSP -= dados.bonusMultiplicadorRecuperacaoSP;
            }

            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }
    }
}
