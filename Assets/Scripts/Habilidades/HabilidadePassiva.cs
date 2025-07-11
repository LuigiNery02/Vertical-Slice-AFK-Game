using UnityEngine;
public class HabilidadePassiva : HabilidadeBase
{
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {

    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {

    }

    public bool ChecarRuna(IAPersonagemBase personagem, int nivel)
    {
        switch (nivel)
        {
            case 1:
                if (personagem.personagem.runaNivel1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            case 2:
                if (personagem.personagem.runaNivel2)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            case 3:
                if (personagem.personagem.runaNivel3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            default: return false;
        }
    }
}
