using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10CajadoVenenoso : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _hpOriginal; //hp original do personagem
    private float _ataqueMagicoOriginal; //ataque mágico original do personagem
    private float _velocidadeDeMovimentoOriginal; //velocidade de movimento do inimigo
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _hpOriginal = personagem._hpMaximoEInicial;
        _danoOriginal = personagem.personagem.arma.dano;
        _ataqueMagicoOriginal = personagem.danoAtaqueMagico;
        personagem.efeitoPorAtaque = CausarEnvenenamento;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
        _velocidadeDeMovimentoOriginal = personagem._personagemAlvo._velocidade;

        switch (nivel)
        {
            case 1:
                personagem.SofrerDano((_hpOriginal / 20)); //reduz o hp em 5%
                personagem.personagem.arma.dano += (_danoOriginal / 20); //aumenta o dano em 5%
                personagem.danoAtaqueMagico += (_ataqueMagicoOriginal / 2); //aumenta em 50% o ataque mágico
                break;
            case 2:
                personagem.SofrerDano((_hpOriginal / 10)); //reduz o hp em 10%
                personagem.personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                personagem.danoAtaqueMagico += _ataqueMagicoOriginal; //aumenta em 100% o ataque mágico
                break;
            case 3:
                personagem.SofrerDano((_hpOriginal / 5)); ; //reduz o hp em 20%
                personagem.personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                personagem.danoAtaqueMagico += (_ataqueMagicoOriginal * 2); //aumenta em 200% o ataque mágico
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
        personagem.personagem.ataqueMagico = _ataqueMagicoOriginal;
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
