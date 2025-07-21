using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Ladino/Mãos Ligeiras/Nv3")]
public class HabilidadeMaosLigeirasNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float buffVelocidadeDeAtaque = 0.3f;

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
                float bonus = personagem._velocidadeDeAtaque * buffVelocidadeDeAtaque;
                personagem._velocidadeDeAtaque -= bonus;

                dados.valorMultiplicadoAtaque = bonus;
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
                personagem._velocidadeDeAtaque += dados.valorMultiplicadoAtaque;
            }

            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }
    }
}
