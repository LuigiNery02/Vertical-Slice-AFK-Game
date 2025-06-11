using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade7Arqueiro : HabilidadeBase
{
    private int _precisaoOriginal; //precisão original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _precisaoOriginal = personagem.personagem.precisao;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //aumenta em 100% a precisão do personagem
                personagem.personagem.precisao += _precisaoOriginal;
                break;
            case 2:
                //aumenta em 200% a precisão do personagem
                personagem.personagem.precisao += (_precisaoOriginal * 2);
                break;
            case 3:
                //aumenta em 300% a precisão do personagem
                personagem.personagem.precisao += (_precisaoOriginal * 3);
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.precisao = _precisaoOriginal;
    }
}
