using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9Arqueiro : HabilidadeBase
{
    private float _precisaoOriginal; //precis�o original do personagem
    private float esquivaOriginal; //esquiva original do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //checa se o ataque acertou
                //aumenta em 25% a precis�o
                //aumenta em 25% a esquiva
                break;
            case 2:
                //checa se o ataque acertou
                //aumenta em 50% a precis�o
                //aumenta em 50% a esquiva
                break;
            case 3:
                //checa se o ataque acertou
                //aumenta em 100% a precis�o
                //aumenta em 100% a esquiva
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
    }
}
