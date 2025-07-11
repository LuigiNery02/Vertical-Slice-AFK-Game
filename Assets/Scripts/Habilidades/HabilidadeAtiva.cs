using UnityEngine;

public enum HabilidadeElemento { Neutro, Agua, Fogo, Vento, Sagrado }
public class HabilidadeAtiva : HabilidadeBase
{
    public HabilidadeElemento elemento;
    public float custoDeMana;
    public float castFixo;
    public float castVariavel;
    public float tempoDeEfeito;
    public float tempoDeRecarga;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if(tempoDeEfeito != 0)
        {
            int indice = 0;

            if(tipoDeHabilidade == TipoDeHabilidade.Classe)
            {
                indice = 1;
            }
            else if(tipoDeHabilidade == TipoDeHabilidade.Arma)
            {
                indice = 2;
            }

            personagem.EsperarEfeitoHabilidade(indice, tempoDeEfeito);
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (tempoDeRecarga != 0)
        {
            int indice = 0;

            if (tipoDeHabilidade == TipoDeHabilidade.Classe)
            {
                indice = 1;
            }
            else if (tipoDeHabilidade == TipoDeHabilidade.Arma)
            {
                indice = 2;
            }

            personagem.EsperarRecargaHabilidade(indice, tempoDeRecarga);
        }
    }

    public bool ChecarAtivacao(IAPersonagemBase personagem)
    {
        if(personagem.spAtual >= custoDeMana)
        {
            return true;
        }
        else
        {
            return false;
        }
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
