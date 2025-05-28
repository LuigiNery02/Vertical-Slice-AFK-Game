using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12Guerreiro : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    private float[] _velocidadeDeAtaqueOriginal; //velocidade de ataque original dos personagens aliados
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //atribui cada dano a cada personagem aliado da lista
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //diminui a velocidade de ataque dos peronsagens aliados em 0.1 
                tempoDeEfeito = 3;
                break;
            case 2:
                //diminui a velocidade de ataque dos peronsagens aliados em 0.2
                tempoDeEfeito = 5;
                break;
            case 3:
                //diminui a velocidade de ataque dos peronsagens aliados em 0.3 
                tempoDeEfeito = 10;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
    }
}
