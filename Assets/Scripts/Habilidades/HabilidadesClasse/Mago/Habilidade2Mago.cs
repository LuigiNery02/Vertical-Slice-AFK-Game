using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2Mago : HabilidadeBase
{
    private float _area; //valor da �rea que causar� dano em �rea
    public override void Inicializar()
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
                //define o ataque como dano em �rea
                _area = 5;
                break;
            case 2:
                //define o ataque como dano em �rea
                _area = 10;
                break;
            case 3:
                //define o ataque como dano em �rea
                _area = 15;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta o ataque como n�o para dano em �rea
    }
}
