using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2ArcoVenenoso : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    public override void Inicializar()
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
                //personagem.arma.dano += (_danoOriginal / 100) * 3; //aumenta o dano da arma em 3%
                break;
            case 2:
                //personagem.arma.dano += (_danoOriginal / 100) * 7; //aumenta o dano da arma em 7%
                break;
            case 3:
                //personagem.arma.dano += (_danoOriginal / 100) * 15; //aumenta o dano da arma em 15%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
