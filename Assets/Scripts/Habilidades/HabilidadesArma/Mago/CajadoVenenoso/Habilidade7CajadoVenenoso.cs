using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade7CajadoVenenoso : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private float _velocidadeDeMovimentoOriginal; //velocidade de movimento do inimigo
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _velocidadeDeAtaqueOriginal = personagem.personagem.arma.velocidadeDeAtaque;
        _danoOriginal = personagem.personagem.arma.dano;
        personagem.efeitoPorAtaque = CausarEnvenenamento;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
        _velocidadeDeMovimentoOriginal = personagem._personagemAlvo._velocidade;

        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.2f; //reduz a velocidade de ataque em 0.2
                personagem.personagem.arma.dano -= (_danoOriginal / 10); //diminui o dano em 10%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.4f; //reduz a velocidade de ataque em 0.4
                personagem.personagem.arma.dano -= (_danoOriginal / 5); //diminui o dano em 20%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.6f; //reduz a velocidade de ataque em 0.6
                personagem.personagem.arma.dano -= (_danoOriginal / 4); //diminui o dano em 25%
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
