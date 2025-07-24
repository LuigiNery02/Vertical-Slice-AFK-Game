using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Arco/Flecha por Flecha/Nv2")]
public class HabilidadeFlechaPorFlechaNv2 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 2.5f;
    [SerializeField]
    private float reducaoVelocidadeDeAtaque = 0.3f;

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
                float bonusVelocidadeAtaque = personagem._velocidadeDeAtaque * reducaoVelocidadeDeAtaque;
                personagem._velocidadeDeAtaque += bonusVelocidadeAtaque;

                float bonusAtaque = personagem._dano * multiplicadorDeDano;
                personagem._dano += bonusAtaque;

                dados.valorMultiplicadoAtaque = bonusVelocidadeAtaque;
                dados.valorOriginalAtaque = bonusAtaque;

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
                personagem._velocidadeDeAtaque -= dados.valorMultiplicadoAtaque;
                personagem._dano -= dados.valorOriginalAtaque;
            }

            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }
    }
}
