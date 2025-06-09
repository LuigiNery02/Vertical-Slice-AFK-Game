using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade3CajadoGelo : HabilidadeBase
{
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private bool _congelamento; //vari�vel que verifica se h� efeito de congelamento
    public override void Inicializar()
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
                //personagem.arma.velocidadeDeAtaque -= 0.06f; //reduz a velocidade de ataque em 0.06
                _congelamento = true;
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque -= 0.12f; //reduz a velocidade de ataque em 0.12
                _congelamento = true;
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque -= 0.18f; //reduz a velocidade de ataque em 0.18
                _congelamento = true;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
        _congelamento = false;
    }
}
