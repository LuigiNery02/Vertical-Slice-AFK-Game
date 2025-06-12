using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5ArcoGelo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _danoOriginal = personagem.personagem.arma.dano;
        personagem.efeitoPorAtaque = CausarCongelamento;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;

        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.dano += (_danoOriginal); //dobra o ataque
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 2; //dura 2 segundos
                break;
            case 2:
                personagem.personagem.arma.dano += (_danoOriginal); //dobra o ataque
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 3; //dura 3 segundos
                break;
            case 3:
                personagem.personagem.arma.dano += (_danoOriginal); //dobra o ataque
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 5; //dura 5 segundos
                break;
        }
    }

    private void CausarCongelamento() //função que ativa o efeito de congelamento
    {
        if (!personagem._personagemAlvo.congelamento)
        {
            personagem._personagemAlvo.congelamento = true;
            personagem._personagemAlvo.VerificarComportamento("paralisia");
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.arma.dano = _danoOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
        if (personagem._personagemAlvo != null && personagem._personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem._personagemAlvo.congelamento = false;
            personagem._personagemAlvo.VerificarComportamento("selecionarAlvo");
        }
        personagem.efeitoPorAtaqueAtivado = false;
    }
}
