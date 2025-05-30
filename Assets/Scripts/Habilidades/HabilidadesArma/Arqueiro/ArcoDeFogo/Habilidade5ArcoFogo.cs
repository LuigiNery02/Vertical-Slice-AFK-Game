using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5ArcoFogo : HabilidadeBase
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
                //personagem.arma.dano += (_danoOriginal); //dobra o ataque
                _queimadura = true;
                //dura 2 segundos
                break;
            case 2:
                //personagem.arma.dano += (_danoOriginal); //dobra o ataque
                _queimadura = true;
                //dura 3 segundos
                break;
            case 3:
                //personagem.arma.dano += (_danoOriginal); //dobra o ataque
                _queimadura = true;
                //dura 5 segundos
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
