using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade1Guerreiro : HabilidadeBase
{
    private float _danoOriginal; //dano original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _danoOriginal = personagem._danoAtaqueBasico;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem._danoAtaqueBasico += (_danoOriginal / 2); //aumenta o dano em 50%
                break;
            case 2:
                personagem._danoAtaqueBasico += (_danoOriginal); //aumenta o dano em 100%
                break;
            case 3:
                personagem._danoAtaqueBasico += (_danoOriginal * 2); //aumenta o dano em 200%
                break;
        }
        
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem._danoAtaqueBasico = _danoOriginal;
    }
}
