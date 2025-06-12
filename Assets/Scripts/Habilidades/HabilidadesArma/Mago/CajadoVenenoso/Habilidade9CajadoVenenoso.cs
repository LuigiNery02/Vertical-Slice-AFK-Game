using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9CajadoVenenoso : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private int _precisaoOriginal; //precisão original da arma
    private float _velocidadeDeMovimentoOriginal; //velocidade de movimento do inimigo
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _velocidadeDeAtaqueOriginal = personagem.personagem.arma.velocidadeDeAtaque;
        _danoOriginal = personagem.personagem.arma.dano;
        _precisaoOriginal = personagem.personagem.precisao;
        personagem.efeitoPorAtaque = CausarEnvenenamento;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
        _velocidadeDeMovimentoOriginal = personagem._personagemAlvo._velocidade;

        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.velocidadeDeAtaque += 0.1f; //aumenta a velocidade de ataque em 0.1
                personagem.personagem.arma.dano += (_danoOriginal / 20); //aumenta o dano em 5%
                personagem.personagem.precisao += _precisaoOriginal; //aumenta em 100% a precisão
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.personagem.arma.velocidadeDeAtaque += 0.2f; //aumenta a velocidade de ataque em 0.2
                personagem.personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                personagem.personagem.precisao += (_precisaoOriginal * 2); //aumenta em 200% a precisão
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.personagem.arma.velocidadeDeAtaque += 0.3f; //aumenta a velocidade de ataque em 0.3
                personagem.personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                personagem.personagem.precisao += (_precisaoOriginal * 3); //aumenta em 300% a precisão
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
        }
    }

    private void CausarEnvenenamento() //função que ativa o efeito de queimadura
    {
        if (!personagem._personagemAlvo.envenenamento)
        {
            personagem._personagemAlvo._velocidade = (_velocidadeDeMovimentoOriginal / 2);
            personagem._personagemAlvo.envenenamento = true;
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.arma.dano = _danoOriginal;
        personagem.personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
        personagem.personagem.precisao = _precisaoOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
        personagem.efeitoPorAtaqueAtivado = false;
        if (personagem._personagemAlvo != null && personagem._personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem._personagemAlvo._velocidade = _velocidadeDeMovimentoOriginal;
            personagem._personagemAlvo.envenenamento = false;
        }
    }
}
