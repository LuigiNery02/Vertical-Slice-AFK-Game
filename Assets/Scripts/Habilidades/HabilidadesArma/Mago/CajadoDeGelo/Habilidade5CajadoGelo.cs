using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5CajadoGelo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    private bool _congelamento; //variável que verifica se há efeito de congelamento
    private void Start()
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
                //personagem.arma.dano += (_danoOriginal); //dobra o ataque
                _congelamento = true;
                //dura 1,5 segundos
                break;
            case 2:
                //personagem.arma.dano += (_danoOriginal); //dobra o ataque
                _congelamento = true;
                //dura 2,5 segundos
                break;
            case 3:
                //personagem.arma.dano += (_danoOriginal); //dobra o ataque
                _congelamento = true;
                //dura 4,5 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.arma.dano = _danoOriginal;
        _congelamento = false;
    }
}
