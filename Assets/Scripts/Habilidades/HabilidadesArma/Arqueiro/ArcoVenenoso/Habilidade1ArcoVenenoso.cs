using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade1ArcoVenenoso : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private bool _envenenamento; //variável que verifica se há efeito de envenenamento
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
                //personagem.arma.dano += (_danoOriginal / 100) * 15; //aumenta o dano em 15%
                _envenenamento = true;
                break;
            case 2:
                //personagem.arma.dano += (_danoOriginal / 4); //aumenta o dano em 25%
                _envenenamento = true;
                break;
            case 3:
                //personagem.arma.dano += (_danoOriginal / 100) * 35; //aumenta o dano em 35%
                _envenenamento = true;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.dano = _danoOriginal;
        _envenenamento = false;
    }
}
