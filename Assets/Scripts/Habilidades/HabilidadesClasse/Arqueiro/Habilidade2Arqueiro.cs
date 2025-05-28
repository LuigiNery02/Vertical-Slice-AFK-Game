using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2Arqueiro : HabilidadeBase
{
    private int _numeroDeProjeteis; //n�mero de proj�teis
    private float _danoOriginalProjetil; //valor do dano original do proj�til
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
                _numeroDeProjeteis = 2;
                break;
            case 2:
                _numeroDeProjeteis = 3;
                break;
            case 3:
                _numeroDeProjeteis = 3;
                //aumenta o dano dos proj�teis em 20%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
    }
}
