using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5Guerreiro : HabilidadeBase
{
    private float _danoOriginal; //dano original do personagem
    private List<IAPersonagemBase> listaDeInimigos = new List<IAPersonagemBase>(); //lista de inimigos na cena
    private void Start()
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
                //coloca como alvo dos inimigos o personagem
                personagem._danoAtaqueBasico += (_danoOriginal / 20); //aumenta o dano em 5%
                break;
            case 2:
                //coloca como alvo dos inimigos o personagem
                personagem._danoAtaqueBasico += (_danoOriginal / 10); //aumenta o dano em 10%
                break;
            case 3:
                //coloca como alvo dos inimigos o personagem
                personagem._danoAtaqueBasico += (_danoOriginal / 5); //aumenta o dano em 20%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem._danoAtaqueBasico = _danoOriginal;
    }
}
