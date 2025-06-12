using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10ArcoGelo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _hpOriginal; //hp original do personagem
    private int _precisaoOriginal; //precisão original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _hpOriginal = personagem._hpMaximoEInicial;
        _danoOriginal = personagem.personagem.arma.dano;
        _precisaoOriginal = personagem.personagem.precisao;
        personagem.efeitoPorAtaque = CausarCongelamento;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;

        switch (nivel)
        {
            case 1:
                personagem.SofrerDano((_hpOriginal / 20)); //reduz o hp em 5%
                personagem.personagem.arma.dano += (_danoOriginal / 20); //aumenta o dano em 5%
                personagem.personagem.precisao += _precisaoOriginal; //aumenta em 100% a precisão
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.SofrerDano((_hpOriginal / 10)); //reduz o hp em 10%
                personagem.personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                personagem.personagem.precisao += (_precisaoOriginal * 2); //aumenta em 200% a precisão
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.SofrerDano((_hpOriginal / 5)); ; //reduz o hp em 20%
                personagem.personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                personagem.personagem.precisao += (_precisaoOriginal * 3); //aumenta em 300% a precisão
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
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
        personagem.personagem.precisao = _precisaoOriginal;
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
