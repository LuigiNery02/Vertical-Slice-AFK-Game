using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12ArcoGelo : HabilidadeBase
{
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.destreza += 3; //ganha 3 pontos de destreza
                break;
            case 2:
                //personagem.destreza += 6; //ganha 6 pontos de destreza
                break;
            case 3:
                //personagem.destreza += 10; //ganha 10 pontos de destreza
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {

    }
}
