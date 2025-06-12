using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10CajadoGelo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _hpOriginal; //hp original do personagem
    private int _sabedoriaOriginal; //sabedoria original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _hpOriginal = personagem._hpMaximoEInicial;
        _danoOriginal = personagem.personagem.arma.dano;
        _sabedoriaOriginal = personagem.personagem.sabedoria;
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
                personagem.personagem.sabedoria += 1; //aumenta em 1 ponto a sabedoria
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.SofrerDano((_hpOriginal / 10)); //reduz o hp em 10%
                personagem.personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                personagem.personagem.sabedoria += 2; //aumenta em 2 pontos a sabedoria
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.SofrerDano((_hpOriginal / 5)); ; //reduz o hp em 20%
                personagem.personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                personagem.personagem.sabedoria += 3; //aumenta em 3 pontos a sabedoria
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
        personagem.personagem.sabedoria = _sabedoriaOriginal;
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
