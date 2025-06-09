using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade8Arqueiro : HabilidadeBase
{
    private float _velocidadeDeAtaqueOriginal; //velocidade de ataque original do personagem
    public override void Inicializar()
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
                personagem.hpAtual -= (personagem.hpAtual / 3.3f); //diminui em 30% a vida do personagem
                personagem._cooldown -= 0.2f; //diminui a velocidade de ataque em 0.2
                break;
            case 2:
                personagem.hpAtual -= (personagem.hpAtual / 6.7f); //diminui em 20% a vida do personagem
                personagem._cooldown -= 0.3f; //diminui a velocidade de ataque em 0.3
                break;
            case 3:
                personagem.hpAtual -= (personagem.hpAtual / 10); //diminui em 10% a vida do personagem
                personagem._cooldown -= 0.4f; //diminui a velocidade de ataque em 0.4
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos do personagem
        personagem._cooldown = _velocidadeDeAtaqueOriginal;
    }
}
