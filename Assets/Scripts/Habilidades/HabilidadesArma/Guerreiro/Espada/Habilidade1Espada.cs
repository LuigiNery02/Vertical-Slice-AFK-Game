using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade1Espada : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        //_danoOriginal = personagem.arma.dano;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                break;
            case 2:
                //personagem.arma.dano += (_danoOriginal / 10) * 3; //aumenta o dano em 30%
                break;
            case 3:
                //personagem.arma.dano += (_danoOriginal / 10) * 4; //aumenta o dano em 40%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.dano = _danoOriginal;
    }
}
