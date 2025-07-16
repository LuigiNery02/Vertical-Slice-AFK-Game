using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Espada/Maestria com Espadas/Nv2")]

public class HabilidadeMaestriaComEspadasNv2 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float buffAtaque = 30;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (personagem.personagem.arma.nome == "Espada")
            {
                personagem._dano += buffAtaque;
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.personagem.arma.nome == "Espada")
        {
            personagem._dano -= buffAtaque;
        }
    }
}
