using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade6Machado : HabilidadeBase
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
                //personagem.arma.velocidadeDeAtaque -= 0.175f; //reduz a velocidade de ataque em 0.175
                //dura 4 segundos
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque -= 0.175f; //reduz a velocidade de ataque em 0.175
                //dura 8 segundos
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque -= 0.175f; //reduz a velocidade de ataque em 0.175
                //dura 12 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
    }
}
