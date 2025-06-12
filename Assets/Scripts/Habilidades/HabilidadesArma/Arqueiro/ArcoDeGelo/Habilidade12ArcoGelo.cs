using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12ArcoGelo : HabilidadeBase
{
    private int _destrezaOriginal; //valor original da destreza do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        _destrezaOriginal = personagem.personagem.destreza;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.destreza += 3; //ganha 3 pontos de destreza
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.personagem.destreza += 6; //ganha 6 pontos de destreza
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.personagem.destreza += 10; //ganha 10 pontos de destreza
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.personagem.destreza = _destrezaOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
    }
}
