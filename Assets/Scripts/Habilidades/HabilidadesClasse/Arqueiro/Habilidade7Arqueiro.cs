using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade7Arqueiro : HabilidadeBase
{
    private float _precisaoOriginal; //precisão original do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //aumenta em 50% a precisão do personagem
                break;
            case 2:
                //aumenta em 100% a precisão do personagem
                break;
            case 3:
                //aumenta em 200% a precisão do personagem
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
    }
}
