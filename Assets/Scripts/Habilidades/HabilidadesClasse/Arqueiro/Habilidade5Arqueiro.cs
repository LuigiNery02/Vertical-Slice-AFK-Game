using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5Arqueiro : HabilidadeBase
{
    private float _esquivaOriginal; //esquiva original do personagem
    private List<IAPersonagemBase> listaDeInimigos = new List<IAPersonagemBase>(); //lista de inimigos na cena
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais dos personagens
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //diminui 20% a esquiva de todos os inimigos
                break;
            case 2:
                //diminui 30% a esquiva de todos os inimigos
                break;
            case 3:
                //diminui 50% a esquiva de todos os inimigos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais dos personagens
    }
}
