using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade1Arqueiro : HabilidadeBase
{
    private float _velocidadeProjetilOriginal; //velocidade original do projétil do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //_velocidadeProjetilOriginal = ...
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //aumenta a velocidade do projétil do personagem em 10%
                break;
            case 2:
                //aumenta a velocidade do projétil do personagem em 20%
                break;
            case 3:
                //aumenta a velocidade do projétil do personagem em 30%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
    }
}
