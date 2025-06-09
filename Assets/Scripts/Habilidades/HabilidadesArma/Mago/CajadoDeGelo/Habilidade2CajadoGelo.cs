using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2CajadoGelo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        //_danoOriginal = personagem.arma.dano;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.arma.dano += (_danoOriginal / 100); //aumenta o dano da arma em 1%
                break;
            case 2:
                //personagem.arma.dano += (_danoOriginal / 100) * 3; //aumenta o dano da arma em 3%
                break;
            case 3:
                //personagem.arma.dano += (_danoOriginal / 100) * 5; //aumenta o dano da arma em 5%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
