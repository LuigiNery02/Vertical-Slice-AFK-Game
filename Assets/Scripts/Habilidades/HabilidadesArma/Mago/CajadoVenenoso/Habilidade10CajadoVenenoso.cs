using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10CajadoVenenoso : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _hpOriginal; //hp original do personagem
    private float _ataqueMagicoOriginal; //ataque mágico original do personagem
    private bool _envenenamento; //variável que verifica se há efeito de envenenamento
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _hpOriginal = personagem._hpMaximoEInicial;
        //_danoOriginal = personagem.arma.dano;
        //_ataqueMagicoOriginal = personagem.ataqueMagico;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.hpAtual -= (_hpOriginal / 20); //reduz o hp em 5%
                //personagem.arma.dano += (_danoOriginal / 20); //aumenta o dano em 5%
                //personagem.precisao += (_precisaoOriginal / 2); //aumenta em 50% o ataque mágico
                _envenenamento = true;
                break;
            case 2:
                personagem.hpAtual -= (_hpOriginal / 10); //reduz o hp em 10%
                //personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                //personagem.precisao += _precisaoOriginal; //aumenta em 100% o ataque mágico
                _envenenamento = true;
                break;
            case 3:
                personagem.hpAtual -= (_hpOriginal / 5); //reduz o hp em 20%
                //personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                //personagem.precisao += (_precisaoOriginal * 2); //aumenta em 200% o ataque mágico
                _envenenamento = true;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.dano = _danoOriginal;
        //personagem.ataqueMagico = _ataqueMagicoOriginal;
        _envenenamento = false;
    }
}
