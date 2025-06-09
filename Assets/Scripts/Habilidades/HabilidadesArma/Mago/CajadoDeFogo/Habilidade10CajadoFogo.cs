using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10CajadoFogo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _hpOriginal; //hp original do personagem
    private float _defesaMagicaOriginal; //precisão original da arma
    private bool _queimadura; //variável que verifica se há efeito de queimadura
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
                //personagem.defesaMagica += _precisaoOriginal; //aumenta em 100% a defesa mágica
                _queimadura = true;
                break;
            case 2:
                personagem.hpAtual -= (_hpOriginal / 10); //reduz o hp em 10%
                //personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                //personagem.defesaMagica += (_precisaoOriginal * 2); //aumenta em 200% a defesa mágica
                _queimadura = true;
                break;
            case 3:
                personagem.hpAtual -= (_hpOriginal / 5); //reduz o hp em 20%
                //personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                //personagem.defesaMagica += (_precisaoOriginal * 3); //aumenta em 300% a defesa mágica
                _queimadura = true;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.dano = _danoOriginal;
        //personagem.defesaMagica = _defesaMagicaOriginal;
        _queimadura = false;
    }
}
