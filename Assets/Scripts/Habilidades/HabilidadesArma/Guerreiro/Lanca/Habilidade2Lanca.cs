using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2Lanca : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        //_danoOriginal = personagem.arma.dano;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.arma.dano += (_danoOriginal / 100) * 3; //aumenta o dano em 3%
                break;
            case 2:
                //personagem.arma.dano += (_danoOriginal / 100) * 9; //aumenta o dano em 9%
                break;
            case 3:
                //personagem.arma.dano += (_danoOriginal / 100) * 18; //aumenta o dano em 18%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
