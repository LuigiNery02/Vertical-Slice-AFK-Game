using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade3Mago : HabilidadeBase
{
    private float _dano; //dano que o personagem causou ao inimigo
    public override void Inicializar()
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
                //checar se o ataque atingiu o inimigo
                //personagem.ReceberHP(_dano / 20); //recupera 5% de HP referente ao dano causado
                break;
            case 2:
                //checar se o ataque atingiu o inimigo
                //personagem.ReceberHP(_dano / 10); //recupera 10% de HP referente ao dano causado
                break;
            case 3:
                //checar se o ataque atingiu o inimigo
                //personagem.ReceberHP(_dano / 5); //recupera 20% de HP referente ao dano causado
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
