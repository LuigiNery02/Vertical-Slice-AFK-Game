using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade6ArcoVenenoso : HabilidadeBase
{
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private float _velocidadeDeMovimentoOriginal; //velocidade de movimento do inimigo
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _velocidadeDeAtaqueOriginal = personagem.personagem.arma.velocidadeDeAtaque;
        personagem.efeitoPorAtaque = CausarEnvenenamento;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
        _velocidadeDeMovimentoOriginal = personagem._personagemAlvo._velocidade;

        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.15f; //reduz a velocidade de ataque em 0.15
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 4; //dura 4 segundos
                break;
            case 2:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.15f; //reduz a velocidade de ataque em 0.15
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 8; //dura 8 segundos
                break;
            case 3:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.15f; //reduz a velocidade de ataque em 0.15
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 12; //dura 12 segundos
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
