using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Sacerdote/Mãoes Abençoadas/Nv1")]
public class HabilidadeMaosAbencoadasNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorBonusCura = 0.1f;
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
                personagem.multiplicadorBonusCura += multiplicadorBonusCura;

                dados.bonusMultiplicadorCura = multiplicadorBonusCura;

                dados.bonusAplicado = true;
            }
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
    }
}
