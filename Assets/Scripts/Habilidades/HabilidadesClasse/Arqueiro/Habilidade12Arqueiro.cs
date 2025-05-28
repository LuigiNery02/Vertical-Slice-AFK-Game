using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12Arqueiro : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    private float[] _esquivaOriginal; //esquiva original dos personagens aliados
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //atribui cada esquiva a cada personagem aliado da lista
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //aumenta a esquiva dos personagens aliados em 10%
                tempoDeEfeito = 3;
                break;
            case 2:
                //aumenta a esquiva dos personagens aliados em 20%
                tempoDeEfeito = 5;
                break;
            case 3:
                //aumenta a esquiva dos personagens aliados em 30%
                tempoDeEfeito = 10;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
    }
}
