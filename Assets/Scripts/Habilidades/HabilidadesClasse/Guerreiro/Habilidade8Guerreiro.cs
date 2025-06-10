using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade8Guerreiro : HabilidadeBase
{
    private int _precisaoOriginal; //precisao original do personagem
    private int _esquivaOriginal; //esquiva original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _precisaoOriginal = personagem.personagem.precisao;
        _esquivaOriginal = personagem.personagem.esquiva;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.precisao += (_precisaoOriginal / 4); //aumenta a precisão em 25%
                personagem.personagem.esquiva += (_esquivaOriginal / 4); //aumenta a esquiva em 25%;
                break;
            case 2:
                personagem.personagem.precisao += (_precisaoOriginal / 2); //aumenta a precisão em 50%
                personagem.personagem.esquiva += (_esquivaOriginal / 2); //aumenta a esquiva em 50%;
                break;
            case 3:
                personagem.personagem.precisao += _precisaoOriginal; //aumenta a precisão em 100%
                personagem.personagem.esquiva += _esquivaOriginal; //aumenta a esquiva em 100%;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        _precisaoOriginal = personagem.personagem.precisao;
        _esquivaOriginal = personagem.personagem.esquiva;
    }
}
