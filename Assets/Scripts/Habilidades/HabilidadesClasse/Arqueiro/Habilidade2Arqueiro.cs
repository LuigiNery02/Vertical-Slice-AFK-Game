using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2Arqueiro : HabilidadeBase
{
    private float _danoOriginalProjetil; //valor do dano original do projétil
    private int _precisaoOriginal; //precisão original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //_danoOriginalProjetil = personagem.danoAtaqueDistancia;
        _precisaoOriginal = personagem.personagem.precisao;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.precisao += _precisaoOriginal; //aumenta a precisão em 100%
                //personagem.danoAtaqueDistancia += (_danoOriginalProjetil / 20); //aumenta o dano em 5%
                break;
            case 2:
                personagem.personagem.precisao += (_precisaoOriginal * 2); //aumenta a precisão em 200%
                //personagem.danoAtaqueDistancia += (_danoOriginalProjetil / 10); //aumenta o dano dos projéteis em 10%
                break;
            case 3:
                personagem.personagem.precisao += (_precisaoOriginal * 3); //aumenta a precisão em 300%
                //personagem.danoAtaqueDistancia += (_danoOriginalProjetil / 5); //aumenta o dano dos projéteis em 20%
                break;
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.danoAtaqueDistancia = _danoOriginalProjetil;
        personagem.personagem.precisao = _precisaoOriginal;
    }
}
