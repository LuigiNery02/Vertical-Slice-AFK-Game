using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade1CajadoFogo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private bool _queimadura; //variável que verifica se há efeito de queimadura
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
                //personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 15%
                _queimadura = true;
                break;
            case 2:
                //personagem.arma.dano += (_danoOriginal / 10) * 3; //aumenta o dano em 25%
                _queimadura = true;
                break;
            case 3:
                //personagem.arma.dano += (_danoOriginal / 10) * 4; //aumenta o dano em 35%
                _queimadura = true;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.dano = _danoOriginal;
        _queimadura = false;
    }
}
