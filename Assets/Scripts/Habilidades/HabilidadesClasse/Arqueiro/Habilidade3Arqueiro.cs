using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade3Arqueiro : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //_danoOriginal = personagem.arma.dano
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //checar se o ataque atingiu o inimigo
                personagem.ReceberHP(_danoOriginal / 20); //recupera HP referente a 5% do dano da arma
                break;
            case 2:
                //checar se o ataque atingiu o inimigo
                personagem.ReceberHP(_danoOriginal / 10); //recupera HP referente a 10% do dano da arma
                break;
            case 3:
                //checar se o ataque atingiu o inimigo
                personagem.ReceberHP(_danoOriginal / 5); //recupera HP referente a 20% do dano da arma
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
