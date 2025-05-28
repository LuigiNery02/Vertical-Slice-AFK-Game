using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade3Guerreiro : HabilidadeBase
{
    private float _hpOriginal; //hp original do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _hpOriginal = personagem._hpMaximoEInicial;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //checar se o ataque atingiu o inimigo
                personagem.ReceberHP(_hpOriginal / 20); //recupera 5% de HP para cada ataque
                break;
            case 2:
                //checar se o ataque atingiu o inimigo
                personagem.ReceberHP(_hpOriginal / 10); //recupera 10% de HP para cada ataque
                break;
            case 3:
                //checar se o ataque atingiu o inimigo
                personagem.ReceberHP(_hpOriginal / 5); //recupera 20% de HP para cada ataque
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
