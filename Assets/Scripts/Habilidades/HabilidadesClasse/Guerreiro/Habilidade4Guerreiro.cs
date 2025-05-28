using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade4Guerreiro : HabilidadeBase
{
    private float _hpOriginal; //dano original do personagem
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
                personagem.ReceberHP(_hpOriginal / _hpOriginal); //recupera 1% de HP 
                break;
            case 2:
                //checar se o ataque atingiu o inimigo
                personagem.ReceberHP(_hpOriginal / 67); //recupera 1.5% de HP 
                break;
            case 3:
                //checar se o ataque atingiu o inimigo
                personagem.ReceberHP(_hpOriginal / 50); //recupera 2% de HP 
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
