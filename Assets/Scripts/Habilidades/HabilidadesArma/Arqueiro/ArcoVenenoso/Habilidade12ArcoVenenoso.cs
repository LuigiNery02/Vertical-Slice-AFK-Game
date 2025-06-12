using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12ArcoVenenoso : HabilidadeBase
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
        if (!personagem._personagemAlvo.envenenamento)
        {
            switch (nivel)
            {
                case 1:
                    personagem._personagemAlvo._velocidade = (_velocidadeDeMovimentoOriginal / 2);
                    break;
                case 2:
                    personagem._personagemAlvo._velocidade = (_velocidadeDeMovimentoOriginal / 3);
                    break;
                case 3:
                    personagem._personagemAlvo._velocidade = (_velocidadeDeMovimentoOriginal / 4);
                    break;
            }
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
