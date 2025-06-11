using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade1Mago : HabilidadeBase
{
    private float _danoMagicoOriginal; //dano m�gico original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _danoMagicoOriginal = personagem.danoAtaqueMagico;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.danoAtaqueMagico += (_danoMagicoOriginal / 2); //aumenta o dano m�gico em 50%
                break;
            case 2:
                personagem.danoAtaqueMagico += _danoMagicoOriginal; //aumenta o dano m�gico em 100%
                break;
            case 3:
                personagem.danoAtaqueMagico += (_danoMagicoOriginal * 2); //aumenta o dano m�gico em 200%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.danoAtaqueMagico = _danoMagicoOriginal;
    }
}
