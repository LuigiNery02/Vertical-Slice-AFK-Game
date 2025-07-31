using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Elementalista/Conjuração Focada/Nv1")]
public class HabilidadeConjuracaoFocadaNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float buffCastFixo = 0.1f;
    public override void AtivarEfeito(IAPersonagemBase personagem)
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
                if (personagem.habilidadeAtivaClasse != null)
                {
                    float bonusAtivaClasse = personagem.habilidadeAtivaClasse.castFixo * buffCastFixo;
                    personagem.habilidadeAtivaClasse.reducaoCastFixo += bonusAtivaClasse;

                    dados.buffCastFixoAtivaClasse = bonusAtivaClasse;

                    dados.bonusAplicado = true;
                }

                if (personagem.habilidadeAtivaArma != null)
                {
                    float bonusAtivaArma = personagem.habilidadeAtivaArma.castFixo * buffCastFixo;
                    personagem.habilidadeAtivaArma.reducaoCastFixo += bonusAtivaArma;

                    dados.buffCastFixoAtivaArma = bonusAtivaArma;

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
                if (personagem.habilidadeAtivaClasse != null)
                {
                    personagem.habilidadeAtivaClasse.reducaoCastFixo -= dados.buffCastFixoAtivaClasse;
                }

                if (personagem.habilidadeAtivaArma != null)
                {
                    personagem.habilidadeAtivaArma.reducaoCastFixo -= dados.buffCastFixoAtivaArma;
                }
            }

            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }
    }
}
