using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5ArcoFogo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _danoOriginal = personagem.personagem.arma.dano;
        personagem.efeitoPorAtaque = CausarQueimadura;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;

        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.dano += _danoOriginal; //dobra o ataque
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 2; //dura 2 segundos
                break;
            case 2:
                personagem.personagem.arma.dano += _danoOriginal; //dobra o ataque
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 3; //dura 3 segundos
                break;
            case 3:
                personagem.personagem.arma.dano += _danoOriginal; //dobra o ataque
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 5;//dura 5 segundos
                break;
        }
    }

    private void CausarQueimadura() //fun��o que ativa o efeito de queimadura
    {
        if (!personagem._personagemAlvo.queimadura)
        {
            personagem._personagemAlvo.danoQueimadura = 2;
            personagem._personagemAlvo.queimadura = true;
            personagem._personagemAlvo.Queimadura();
        }
    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.arma.dano = _danoOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
        personagem.efeitoPorAtaqueAtivado = false;
        if (personagem._personagemAlvo != null && personagem._personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem._personagemAlvo.queimadura = false;
        }
    }
}
