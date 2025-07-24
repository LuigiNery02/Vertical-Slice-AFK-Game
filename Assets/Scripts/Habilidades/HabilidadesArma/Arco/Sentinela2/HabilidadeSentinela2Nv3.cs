using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Arco/Sentinela2/Nv3")]
public class HabilidadeSentinela2Nv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float bonusRange = 0.3f;

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
                float bonus = personagem.distanciaMinimaParaAtacar * bonusRange;
                personagem.distanciaMinimaParaAtacar += bonus;

                dados.valorMultiplicadoRange = bonus;
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
                personagem.distanciaMinimaParaAtacar -= dados.valorMultiplicadoRange;
            }

            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }
    }
}
