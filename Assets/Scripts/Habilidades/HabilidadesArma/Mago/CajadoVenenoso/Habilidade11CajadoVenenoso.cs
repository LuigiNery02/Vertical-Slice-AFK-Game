using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11CajadoVenenoso : HabilidadeBase
{
    private float _velocidadeDeMovimentoOriginal; //velocidade de movimento do inimigo
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        personagem.efeitoPorAtaque = CausarEnvenenamento;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
        _velocidadeDeMovimentoOriginal = personagem._personagemAlvo._velocidade;
    }

    private void CausarEnvenenamento() //função que ativa o efeito de queimadura
    {
        switch (nivel)
        {
            case 1:
                tempoDeEfeito = 2; //dura 2 segundos
                break;
            case 2:
                tempoDeEfeito = 4; //dura 4 segundos
                break;
            case 3:
                tempoDeEfeito = 6; //dura 6 segundos
                break;
        }

        if (!personagem._personagemAlvo.envenenamento)
        {
            personagem._personagemAlvo._velocidade = (_velocidadeDeMovimentoOriginal / 2);
            personagem._personagemAlvo.envenenamento = true;
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = false;
        if (personagem._personagemAlvo != null && personagem._personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem._personagemAlvo._velocidade = _velocidadeDeMovimentoOriginal;
            personagem._personagemAlvo.envenenamento = false;
        }
    }
}
