using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade7Machado : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _velocidadeDeAtaqueOriginal = personagem.personagem.arma.velocidadeDeAtaque;
        _danoOriginal = personagem.personagem.arma.dano;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.075f; //reduz a velocidade de ataque em 0.075
                personagem.personagem.arma.dano -= (_danoOriginal / 100) * 7.5f; //diminui o dano em 7.5%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.15f; //reduz a velocidade de ataque em 0.15
                personagem.personagem.arma.dano -= (_danoOriginal / 100) * 15; //diminui o dano em 15%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.25f; //reduz a velocidade de ataque em 0.25
                personagem.personagem.arma.dano -= (_danoOriginal / 100) * 22.5f; //diminui o dano em 22.5%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.arma.dano = _danoOriginal;
        personagem.personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
    }
}
