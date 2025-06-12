using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade7Lanca : HabilidadeBase
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
                personagem.personagem.arma.velocidadeDeAtaque -= 0.05f; //reduz a velocidade de ataque em 0.05
                personagem.personagem.arma.dano -= (_danoOriginal / 10); //diminui o dano em 10%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.1f; //reduz a velocidade de ataque em 0.1
                personagem.personagem.arma.dano -= (_danoOriginal / 5); //diminui o dano em 20%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.15f; //reduz a velocidade de ataque em 0.15
                personagem.personagem.arma.dano -= (_danoOriginal / 4); //diminui o dano em 25%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.personagem.arma.dano = _danoOriginal;
        personagem.personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
    }
}
