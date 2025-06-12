using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5CajadoFogo : HabilidadeBase
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
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;

        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.dano += _danoOriginal; //dobra o ataque
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 2.5f; //dura 2,5 segundos
                break;
            case 2:
                personagem.personagem.arma.dano += _danoOriginal; //dobra o ataque
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 3.5f; //dura 3,5 segundos
                break;
            case 3:
                personagem.personagem.arma.dano += _danoOriginal; //dobra o ataque
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 6; //dura 6 segundos
                break;
        }
    }

    private void CausarQueimadura() //função que ativa o efeito de queimadura
    {
        if (!personagem._personagemAlvo.queimadura)
        {
            personagem._personagemAlvo.danoQueimadura = 2;
            personagem._personagemAlvo.queimadura = true;
            personagem._personagemAlvo.Queimadura();
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
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
