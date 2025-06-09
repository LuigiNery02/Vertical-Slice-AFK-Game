using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2Mago : HabilidadeBase
{
    private float _area; //valor da área que causará dano em área
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
                //define o ataque como dano em área
                _area = 5;
                break;
            case 2:
                //define o ataque como dano em área
                _area = 10;
                break;
            case 3:
                //define o ataque como dano em área
                _area = 15;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta o ataque como não para dano em área
    }
}
