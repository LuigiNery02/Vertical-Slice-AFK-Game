using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade8Guerreiro : HabilidadeBase
{
    private float _precisaoOriginal; //precisao original do personagem
    private float _esquivaOriginal; //esquiva original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //_precisaoOriginal = personagem.precisao;
        //_esquivaOriginal = personagem.esquiva
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.precisao += (_precisaoOriginal / 4); //aumenta a precis�o em 25%
                //personagem.esquiva += (_esquivaOriginal / 4); //aumenta a esquiva em 25%;
                break;
            case 2:
                //personagem.precisao += (_precisaoOriginal / 2); //aumenta a precis�o em 50%
                //personagem.esquiva += (_esquivaOriginal / 2); //aumenta a esquiva em 50%;
                break;
            case 3:
                //personagem.precisao += _precisaoOriginal; //aumenta a precis�o em 100%
                //personagem.esquiva += _esquivaOriginal; //aumenta a esquiva em 100%;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //_precisaoOriginal = personagem.precisao;
        //_esquivaOriginal = personagem.esquiva;
    }
}
