using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11ArcoFogo : HabilidadeBase
{
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
        personagem.efeitoPorAtaque = CausarQueimadura;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
    }

    private void CausarQueimadura() //função que ativa o efeito de queimadura
    {
        switch (nivel)
        {
            case 1:
                tempoDeEfeito = 3; //dura 3 segundos
                break;
            case 2:
                tempoDeEfeito = 5; //dura 5 segundos
                break;
            case 3:
                tempoDeEfeito = 7; //dura 7 segundos
                break;
        }

        if (!personagem._personagemAlvo.queimadura)
        {
            personagem._personagemAlvo.danoQueimadura = 2;
            personagem._personagemAlvo.queimadura = true;
            personagem._personagemAlvo.Queimadura();
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = false;
        if (personagem._personagemAlvo != null && personagem._personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem._personagemAlvo.queimadura = false;
        }
    }
}
