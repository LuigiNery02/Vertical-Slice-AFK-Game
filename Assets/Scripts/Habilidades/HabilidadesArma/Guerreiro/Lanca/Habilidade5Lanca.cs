using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5Lanca : HabilidadeBase
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
                //personagem.arma.dano += (_danoOriginal / 2); //aumenta o dano da arma em 50%
                //dura 5 segundos
                break;
            case 2:
                //personagem.arma.dano += (_danoOriginal / 2); //aumenta o dano da arma em 50%
                //dura 10 segundos
                break;
            case 3:
                //personagem.arma.dano += (_danoOriginal / 2); //aumenta o dano da arma em 50%
                //dura 15 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.dano = _danoOriginal;
    }
}
