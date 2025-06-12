using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9Machado : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private int _precisaoOriginal; //precis�o original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _velocidadeDeAtaqueOriginal = personagem.personagem.arma.velocidadeDeAtaque;
        _danoOriginal = personagem.personagem.arma.dano;
        _precisaoOriginal = personagem.personagem.precisao;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.velocidadeDeAtaque += 0.15f; //aumenta a velocidade de ataque em 0.15
                personagem.personagem.arma.dano += (_danoOriginal / 100) * 4; //aumenta o dano em 4%
                personagem.personagem.precisao += _precisaoOriginal; //aumenta em 100% a precis�o
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.personagem.arma.velocidadeDeAtaque += 0.3f; //aumenta a velocidade de ataque em 0.25
                personagem.personagem.arma.dano += (_danoOriginal / 100) * 8; //aumenta o dano em 8%
                personagem.personagem.precisao += (_precisaoOriginal * 2); //aumenta em 200% a precis�o
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.personagem.arma.velocidadeDeAtaque += 0.4f; //aumenta a velocidade de ataque em 0.35
                personagem.personagem.arma.dano += (_danoOriginal / 100) * 16; //aumenta o dano em 16%
                personagem.personagem.precisao += (_precisaoOriginal * 3); //aumenta em 300% a precis�o
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.arma.dano = _danoOriginal;
        personagem.personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
        personagem.personagem.precisao = _precisaoOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
    }
}
