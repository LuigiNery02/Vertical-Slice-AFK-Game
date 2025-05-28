using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade1Mago : HabilidadeBase
{
    private float _danoMagicoOriginal; //dano m�gico original do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _danoMagicoOriginal = personagem._danoAtaqueBasico;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem._danoAtaqueBasico += (_danoMagicoOriginal / 2); //aumenta o dano m�gico em 50%
                break;
            case 2:
                personagem._danoAtaqueBasico += (_danoMagicoOriginal); //aumenta o dano m�gico em 100%
                break;
            case 3:
                personagem._danoAtaqueBasico += (_danoMagicoOriginal * 2); //aumenta o dano m�gico em 200%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem._danoAtaqueBasico = _danoMagicoOriginal;
    }
}
