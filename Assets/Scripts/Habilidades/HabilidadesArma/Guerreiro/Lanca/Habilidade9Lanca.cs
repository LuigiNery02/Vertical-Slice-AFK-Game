using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9Lanca : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private float _precisaoOriginal; //precisão original da arma
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        //_velocidadeDeAtaqueOriginal = personagem.arma.velocidadeDeAtaque;
        //_danoOriginal = personagem.arma.dano;
        //_precisaoOriginal = personagem.precisao;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.arma.velocidadeDeAtaque += 0.1f; //aumenta a velocidade de ataque em 0.1
                //personagem.arma.dano += (_danoOriginal / 100) * 3; //aumenta o dano em 3%
                //personagem.precisao += _precisaoOriginal; //aumenta em 100% a precisão
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque += 0.2f; //aumenta a velocidade de ataque em 0.2
                //personagem.arma.dano += (_danoOriginal / 100) * 9; //aumenta o dano em 9%
                //personagem.precisao += (_precisaoOriginal * 2); //aumenta em 200% a precisão
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque += 0.3f; //aumenta a velocidade de ataque em 0.3
                //personagem.arma.dano += (_danoOriginal / 100) * 18; //aumenta o dano em 18%
                //personagem.precisao += (_precisaoOriginal * 3); //aumenta em 300% a precisão
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.dano = _danoOriginal;
        //personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
        //personagem.precisao = _precisaoOriginal;
    }
}
