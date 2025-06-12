using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10CajadoFogo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _hpOriginal; //hp original do personagem
    private float _defesaMagicaOriginal; //precisão original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _hpOriginal = personagem._hpMaximoEInicial;
        _danoOriginal = personagem.personagem.arma.dano;
        _defesaMagicaOriginal = personagem.personagem.defesaMagica;
        personagem.efeitoPorAtaque = CausarQueimadura;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;

        switch (nivel)
        {
            case 1:
                personagem.hpAtual -= (_hpOriginal / 20); //reduz o hp em 5%
                personagem.personagem.arma.dano += (_danoOriginal / 20); //aumenta o dano em 5%
                personagem.personagem.defesaMagica += _defesaMagicaOriginal; //aumenta em 100% a defesa mágica
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.hpAtual -= (_hpOriginal / 10); //reduz o hp em 10%
                personagem.personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                personagem.personagem.defesaMagica += (_defesaMagicaOriginal * 2); //aumenta em 200% a defesa mágica
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.hpAtual -= (_hpOriginal / 5); //reduz o hp em 20%
                personagem.personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                personagem.personagem.defesaMagica += (_defesaMagicaOriginal * 3); //aumenta em 300% a defesa mágica
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
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
        personagem.personagem.defesaMagica = _defesaMagicaOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
        personagem.efeitoPorAtaqueAtivado = false;
        if (personagem._personagemAlvo != null && personagem._personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem._personagemAlvo.queimadura = false;
        }
    }
}
