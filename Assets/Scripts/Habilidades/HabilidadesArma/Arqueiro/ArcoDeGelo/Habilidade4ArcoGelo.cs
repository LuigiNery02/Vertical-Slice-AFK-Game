using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade4ArcoGelo : HabilidadeBase
{
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        //_velocidadeDeAtaqueOriginal = personagem.arma.velocidadeDeAtaque;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.arma.velocidadeDeAtaque -= 0.02f; //reduz a velocidade de ataque em 0.02
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque -= 0.04f; //reduz a velocidade de ataque em 0.04
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque -= 0.08f; //reduz a velocidade de ataque em 0.08
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
