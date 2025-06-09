using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9ArcoVenenoso : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private float _precisaoOriginal; //precisão original da arma
    private bool _envenenamento; //variável que verifica se há efeito de envenenamento
    public override void Inicializar()
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
                //personagem.arma.velocidadeDeAtaque += 0.2f; //aumenta a velocidade de ataque em 0.2
                //personagem.arma.dano += (_danoOriginal / 20); //aumenta o dano em 5%
                //personagem.precisao += _precisaoOriginal; //aumenta em 100% a precisão
                _envenenamento = true;
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque += 0.3f; //aumenta a velocidade de ataque em 0.3
                //personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                //personagem.precisao += (_precisaoOriginal * 2); //aumenta em 200% a precisão
                _envenenamento = true;
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque += 0.4f; //aumenta a velocidade de ataque em 0.4
                //personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                //personagem.precisao += (_precisaoOriginal * 3); //aumenta em 300% a precisão
                _envenenamento = true;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.dano = _danoOriginal;
        //personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
        //personagem.precisao = _precisaoOriginal;
        _envenenamento = false;
    }
}
