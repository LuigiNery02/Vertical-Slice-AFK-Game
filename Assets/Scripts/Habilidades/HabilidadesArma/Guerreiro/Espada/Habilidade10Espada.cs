using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10Espada : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _hpOriginal; //hp original do personagem
    private float _precisaoOriginal; //precisão original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _hpOriginal = personagem._hpMaximoEInicial;
        //_danoOriginal = personagem.arma.dano;
        //_precisaoOriginal = personagem.precisao;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.hpAtual -= (_hpOriginal / 20); //reduz o hp em 5%
                //personagem.arma.dano += (_danoOriginal / 20); //aumenta o dano em 5%
                //personagem.precisao += _precisaoOriginal; //aumenta em 100% a precisão
                break;
            case 2:
                personagem.hpAtual -= (_hpOriginal / 10); //reduz o hp em 10%
                //personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                //personagem.precisao += (_precisaoOriginal * 2); //aumenta em 200% a precisão
                break;
            case 3:
                personagem.hpAtual -= (_hpOriginal / 5); //reduz o hp em 20%
                //personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                //personagem.precisao += (_precisaoOriginal * 3); //aumenta em 300% a precisão
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.dano = _danoOriginal;
        //personagem.precisao = _precisaoOriginal;
    }
}
