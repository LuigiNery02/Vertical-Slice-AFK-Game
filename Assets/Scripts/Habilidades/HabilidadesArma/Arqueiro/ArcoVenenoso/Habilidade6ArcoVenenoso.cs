using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade6ArcoVenenoso : HabilidadeBase
{
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private bool _envenenamento; //vari�vel que verifica se h� efeito de envenenamento
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
                //personagem.arma.velocidadeDeAtaque -= 0.15f; //reduz a velocidade de ataque em 0.15
                _envenenamento = true;
                //dura 4 segundos
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque -= 0.15f; //reduz a velocidade de ataque em 0.15
                _envenenamento = true;
                //dura 8 segundos
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque -= 0.15f; //reduz a velocidade de ataque em 0.15
                _envenenamento = true;
                //dura 12 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
        _envenenamento = false;
    }
}
