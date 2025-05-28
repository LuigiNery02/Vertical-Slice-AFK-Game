using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade7Guerreiro : HabilidadeBase
{
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _velocidadeDeAtaqueOriginal = personagem._cooldown;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem._cooldown -= 0.1f; //diminui em 0,1 a velocidade de ataque do personagem
                break;
            case 2:
                personagem._cooldown -= 0.2f; //diminui em 0,2 a velocidade de ataque do personagem
                break;
            case 3:
                personagem._cooldown -= 0.3f; //diminui em 0,3 a velocidade de ataque do personagem
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem._cooldown = _velocidadeDeAtaqueOriginal;
    }
}
