using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade4CajadoVenenoso : HabilidadeBase
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
                //personagem.arma.velocidadeDeAtaque -= 0.07f; //reduz a velocidade de ataque em 0.07
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque -= 0.14f; //reduz a velocidade de ataque em 0.14
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque -= 0.21f; //reduz a velocidade de ataque em 0.21
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
