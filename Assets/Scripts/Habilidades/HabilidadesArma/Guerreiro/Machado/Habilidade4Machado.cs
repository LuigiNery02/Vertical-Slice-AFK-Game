using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade4Machado : HabilidadeBase
{
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private void Start()
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
                //personagem.arma.velocidadeDeAtaque -= 0.075f; //reduz a velocidade de ataque em 0.075
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque -= 0.15f; //reduz a velocidade de ataque em 0.15
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque -= 0.25f; //reduz a velocidade de ataque em 0.25
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
