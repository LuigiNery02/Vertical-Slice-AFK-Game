using System;
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

    [HideInInspector]
    public float reducaoCastFixo;
    [HideInInspector]
    public float reducaoCastVariavel;

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

    public void ChecarCastingHabilidade1(IAPersonagemBase personagem, Action efeitoFinal)
    {
        float tempoTotalCast = Mathf.Max(0f, (castFixo - reducaoCastFixo) + (castVariavel - reducaoCastVariavel));

        if (tempoTotalCast > 0f)
        {
            personagem.VerificarComportamento("conjurarHabilidade1");

            personagem.ConjurarHabilidadeComCallback(tempoTotalCast, 1, () =>
            {
                personagem.podeAtivarEfeitoHabilidadeAtivaClasse = false;
                efeitoFinal?.Invoke();
            });
        }
        else
        {
            personagem.podeAtivarEfeitoHabilidadeAtivaClasse = false;
            efeitoFinal?.Invoke();
        }
    }

    public void ChecarCastingHabilidade2(IAPersonagemBase personagem, Action efeitoFinal)
    {
        float tempoTotalCast = Mathf.Max(0f, (castFixo - reducaoCastFixo) + (castVariavel - reducaoCastVariavel));

        if (tempoTotalCast > 0f)
        {
            personagem.VerificarComportamento("conjurarHabilidade2");

            personagem.ConjurarHabilidadeComCallback(tempoTotalCast, 2, () =>
            {
                personagem.podeAtivarEfeitoHabilidadeAtivaArma = false;
                efeitoFinal?.Invoke();
            });
        }
        else
        {
            personagem.podeAtivarEfeitoHabilidadeAtivaArma = false;
            efeitoFinal?.Invoke();
        }
    }

}
