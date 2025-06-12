using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11ArcoGelo : HabilidadeBase
{
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
        personagem.efeitoPorAtaque = CausarCongelamento;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
    }

    private void CausarCongelamento() //função que ativa o efeito de congelamento
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

        if (!personagem._personagemAlvo.congelamento)
        {
            personagem._personagemAlvo.congelamento = true;
            personagem._personagemAlvo.VerificarComportamento("paralisia");
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        if (personagem._personagemAlvo != null && personagem._personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem._personagemAlvo.congelamento = false;
            personagem._personagemAlvo.VerificarComportamento("selecionarAlvo");
        }
        personagem.efeitoPorAtaqueAtivado = false;
    }
}
