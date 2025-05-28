using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11Arqueiro : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    private float[] _defesaOriginal; //defesa original dos personagens aliados
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //atribui cada defesa a cada personagem aliado da lista
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //aumenta a defesa dos peronsagens aliados em 10% + os pontos de suporte do personagem
                tempoDeEfeito = 3;
                break;
            case 2:
                //aumenta a defesa dos peronsagens aliados em 10% + os pontos de suporte do personagem
                tempoDeEfeito = 5;
                break;
            case 3:
                //aumenta a defesa dos peronsagens aliados em 10% + os pontos de suporte do personagem
                tempoDeEfeito = 10;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
    }
}
