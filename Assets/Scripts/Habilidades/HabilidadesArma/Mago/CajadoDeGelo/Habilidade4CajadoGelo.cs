using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade4CajadoGelo : HabilidadeBase
{
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        //_velocidadeDeAtaqueOriginal = personagem.arma.velocidadeDeAtaque;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.arma.velocidadeDeAtaque -= 0.04f; //reduz a velocidade de ataque em 0.04
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque -= 0.08f; //reduz a velocidade de ataque em 0.08
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque -= 0.16f; //reduz a velocidade de ataque em 0.16
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {

    }
}
