using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade8Espada : HabilidadeBase
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
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.velocidadeDeAtaque += 0.1f; //aumenta a velocidade de ataque em 0.1
                personagem.personagem.arma.dano += (_danoOriginal / 20); //aumenta o dano em 5%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.personagem.arma.velocidadeDeAtaque += 0.2f; //aumenta a velocidade de ataque em 0.2
                personagem.personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.personagem.arma.velocidadeDeAtaque += 0.3f; //aumenta a velocidade de ataque em 0.3
                personagem.personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
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
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
    }
}
