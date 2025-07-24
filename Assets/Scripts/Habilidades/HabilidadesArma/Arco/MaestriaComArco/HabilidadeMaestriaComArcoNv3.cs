using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Arco/Maestria com Arco/Nv3")]
public class HabilidadeMaestriaComArcoNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float buffAtaque = 60;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (personagem.personagem.arma.nome == "Arco")
            {
                personagem._dano += buffAtaque;
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.personagem.arma.nome == "Arco")
        {
            personagem._dano -= buffAtaque;
        }
    }
}
