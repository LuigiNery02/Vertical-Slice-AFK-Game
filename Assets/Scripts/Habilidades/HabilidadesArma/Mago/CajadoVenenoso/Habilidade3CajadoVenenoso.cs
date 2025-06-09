using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade3CajadoVenenoso : HabilidadeBase
{
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original da arma
    private bool _envenenamento; //variável que verifica se há efeito de envenenamento
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
                //personagem.arma.velocidadeDeAtaque -= 0.08f; //reduz a velocidade de ataque em 0.08
                _envenenamento = true;
                break;
            case 2:
                //personagem.arma.velocidadeDeAtaque -= 0.16f; //reduz a velocidade de ataque em 0.16
                _envenenamento = true;
                break;
            case 3:
                //personagem.arma.velocidadeDeAtaque -= 0.24f; //reduz a velocidade de ataque em 0.24
                _envenenamento = true;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.velocidadeDeAtaque = _velocidadeDeAtaqueOriginal;
        _envenenamento = false;
    }
}
