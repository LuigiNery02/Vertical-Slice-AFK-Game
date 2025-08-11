using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Sacerdote/Barreira Reforçada/Nv3")]
public class HabilidadeBarreiraReforcadaNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorBonusCura = 2f;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }

            var dados = personagem.dadosDasHabilidadesPassivas[this];

            personagem.efeitoPorAliadoCuradoAtivado = true;

            personagem.AtivarEfeitoPorAliadoCurado("BarreiraReforcadaNv3", (IAPersonagemBase aliado, float cura) =>
            {
                if (dados.bonusAplicado)
                {
                    personagem.multiplicadorBonusCura -= dados.bonusMultiplicadorCura;
                }

                personagem.multiplicadorBonusCura += multiplicadorBonusCura;

                dados.bonusMultiplicadorCura = multiplicadorBonusCura;

                dados.bonusAplicado = true;

            });
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            if (dados.bonusAplicado)
            {
                personagem.multiplicadorBonusCura -= dados.bonusMultiplicadorCura;
            }

            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }

        personagem.efeitoPorAliadoCuradoAtivado = false;
        personagem.RemoverEfeitoPorHabilidadeAliado("BarreiraReforcadaNv3");
    }
}
