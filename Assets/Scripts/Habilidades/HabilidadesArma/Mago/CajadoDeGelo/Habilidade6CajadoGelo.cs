using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade6CajadoGelo : HabilidadeBase
{
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _velocidadeDeAtaqueOriginal = personagem.personagem.arma.velocidadeDeAtaque;
        personagem.efeitoPorAtaque = CausarCongelamento;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;

        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.3f; //reduz a velocidade de ataque em 0.3
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 4; //dura 4 segundos
                break;
            case 2:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.3f; //reduz a velocidade de ataque em 0.3
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 8; //dura 8 segundos
                break;
            case 3:
                personagem.personagem.arma.velocidadeDeAtaque -= 0.3f; //reduz a velocidade de ataque em 0.3
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                tempoDeEfeito = 12; //dura 12 segundos
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
        personagem.personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
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
