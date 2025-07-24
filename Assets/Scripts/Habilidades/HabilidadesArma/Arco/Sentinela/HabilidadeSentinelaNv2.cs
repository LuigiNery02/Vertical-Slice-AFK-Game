using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Arco/Sentinela/Nv2")]
public class HabilidadeSentinelaNv2 : HabilidadePassiva
{
    [Header("Configura��es Habilidade")]
    [SerializeField]
    private float buffVelocidadeDeAtaque = 0.2f;
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
