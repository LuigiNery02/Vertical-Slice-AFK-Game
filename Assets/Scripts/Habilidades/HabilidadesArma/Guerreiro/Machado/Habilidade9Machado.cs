using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9Machado : HabilidadeBase
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
                //personagem.arma.velocidadeDeAtaque += 0.15f; //aumenta a velocidade de ataque em 0.15
                //personagem.arma.dano += (_danoOriginal / 100) * 4; //aumenta o dano em 4%
                //personagem.precisao += _precisaoOriginal; //aumenta em 100% a precisão
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque += 0.3f; //aumenta a velocidade de ataque em 0.25
                //personagem.arma.dano += (_danoOriginal / 100) * 8; //aumenta o dano em 8%
                //personagem.precisao += (_precisaoOriginal * 2); //aumenta em 200% a precisão
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque += 0.4f; //aumenta a velocidade de ataque em 0.35
                //personagem.arma.dano += (_danoOriginal / 100) * 16; //aumenta o dano em 16%
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
