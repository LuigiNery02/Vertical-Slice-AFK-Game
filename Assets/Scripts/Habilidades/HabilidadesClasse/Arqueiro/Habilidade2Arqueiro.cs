using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2Arqueiro : HabilidadeBase
{
    private float _danoOriginalProjetil; //valor do dano original do proj�til
    private int _precisaoOriginal; //precis�o original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //_danoOriginalProjetil = personagem.danoAtaqueDistancia;
        _precisaoOriginal = personagem.personagem.precisao;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.precisao += _precisaoOriginal; //aumenta a precis�o em 100%
                //personagem.danoAtaqueDistancia += (_danoOriginalProjetil / 20); //aumenta o dano em 5%
                break;
            case 2:
                personagem.personagem.precisao += (_precisaoOriginal * 2); //aumenta a precis�o em 200%
                //personagem.danoAtaqueDistancia += (_danoOriginalProjetil / 10); //aumenta o dano dos proj�teis em 10%
                break;
            case 3:
                personagem.personagem.precisao += (_precisaoOriginal * 3); //aumenta a precis�o em 300%
                //personagem.danoAtaqueDistancia += (_danoOriginalProjetil / 5); //aumenta o dano dos proj�teis em 20%
                break;
        }
    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.danoAtaqueDistancia = _danoOriginalProjetil;
        personagem.personagem.precisao = _precisaoOriginal;
    }
}
