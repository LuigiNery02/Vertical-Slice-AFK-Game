using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10Lanca : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _hpOriginal; //hp original do personagem
    private int _precisaoOriginal; //precisão original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _hpOriginal = personagem._hpMaximoEInicial;
        _danoOriginal = personagem.personagem.arma.dano;
        _precisaoOriginal = personagem.personagem.precisao;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.SofrerDano((_hpOriginal / 20)); //reduz o hp em 5%
                personagem.personagem.arma.dano += (_danoOriginal / 100) * 3; //aumenta o dano em 3%
                personagem.personagem.precisao += _precisaoOriginal; //aumenta em 100% a precisão
                break;
            case 2:
                personagem.SofrerDano((_hpOriginal / 10));//reduz o hp em 10%
                personagem.personagem.arma.dano += (_danoOriginal / 100) * 9; //aumenta o dano em 9%
                personagem.personagem.precisao += (_precisaoOriginal * 2); //aumenta em 200% a precisão
                break;
            case 3:
                personagem.SofrerDano((_hpOriginal / 5)); //reduz o hp em 20%
                personagem.personagem.arma.dano += (_danoOriginal / 100) * 18; //aumenta o dano em 18%
                personagem.personagem.precisao += (_precisaoOriginal * 3); //aumenta em 300% a precisão
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.arma.dano = _danoOriginal;
        personagem.personagem.precisao = _precisaoOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
    }
}
