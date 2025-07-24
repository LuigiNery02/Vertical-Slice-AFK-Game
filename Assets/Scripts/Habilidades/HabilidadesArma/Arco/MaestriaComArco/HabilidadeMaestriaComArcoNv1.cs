using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Arco/Maestria com Arco/Nv1")]
public class HabilidadeMaestriaComArcoNv1 : HabilidadePassiva
{
    [Header("Configura��es Habilidade")]
    [SerializeField]
    private float buffAtaque = 10;

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
