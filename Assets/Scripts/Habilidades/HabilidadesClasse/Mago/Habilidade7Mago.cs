using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade7Mago : HabilidadeBase
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
                personagem.ReceberHP(_hpOriginal / 4); //recupera 25% de hp
                break;
            case 2:
                personagem.ReceberHP(_hpOriginal / 2); //recupera 50% de hp
                break;
            case 3:
                personagem.ReceberHP(_hpOriginal); //recupera 100% de hp
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
