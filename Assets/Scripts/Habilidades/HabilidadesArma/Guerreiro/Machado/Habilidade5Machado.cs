using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5Machado : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _danoOriginal = personagem.personagem.arma.dano;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.dano += (_danoOriginal / 4) * 3; //aumenta o ataque em 75%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 2; //dura 2 segundos
                break;
            case 2:
                personagem.personagem.arma.dano += (_danoOriginal / 4) * 3; //aumenta o ataque em 75%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 3; //dura 3 segundos
                break;
            case 3:
                personagem.personagem.arma.dano += (_danoOriginal / 4) * 3; //aumenta o ataque em 75%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 5; //dura 5 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.arma.dano = _danoOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
    }
}
