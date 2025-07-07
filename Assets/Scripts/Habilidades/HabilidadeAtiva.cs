using UnityEngine;

public enum HabilidadeElemento { Neutro, Agua, Fogo, Vento, Sagrado }
public class HabilidadeAtiva : HabilidadeBase
{
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

            personagem.EsperarRecargaHabilidade(indice, tempoDeEfeito);
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
}
