using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2CajadoVenenoso : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        //_danoOriginal = personagem.arma.dano;
        //_danoMagicoOriginal = personagem.danoMagico;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.arma.dano += (_danoOriginal / 100) * 2; //aumenta o dano da arma em 2%
                break;
            case 2:
                //personagem.arma.dano += (_danoOriginal / 100) * 6; //aumenta o dano da arma em 6%
                break;
            case 3:
                //personagem.arma.dano += (_danoOriginal / 100) * 12; //aumenta o dano da arma em 12%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
