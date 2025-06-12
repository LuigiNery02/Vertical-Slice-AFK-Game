using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9CajadoGelo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private float _defesaMágicaOriginal; //precisão original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _velocidadeDeAtaqueOriginal = personagem.personagem.arma.velocidadeDeAtaque;
        _danoOriginal = personagem.personagem.arma.dano;
        _defesaMágicaOriginal = personagem.personagem.defesaMagica;
        personagem.efeitoPorAtaque = CausarCongelamento;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;

        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.velocidadeDeAtaque += 0.05f; //aumenta a velocidade de ataque em 0.05
                personagem.personagem.arma.dano += (_danoOriginal / 20); //aumenta o dano em 5%
                personagem.personagem.defesaMagica += _defesaMágicaOriginal; //aumenta em 100% a defesa mágica
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.personagem.arma.velocidadeDeAtaque += 0.1f; //aumenta a velocidade de ataque em 0.1
                personagem.personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano em 10%
                personagem.personagem.defesaMagica += (_defesaMágicaOriginal * 2); //aumenta em 200% a precisão
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.personagem.arma.velocidadeDeAtaque += 0.2f; //aumenta a velocidade de ataque em 0.2
                personagem.personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano em 20%
                personagem.personagem.defesaMagica += (_defesaMágicaOriginal * 3); //aumenta em 300% a precisão
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
        personagem.personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
        personagem.personagem.defesaMagica = _defesaMágicaOriginal;
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
