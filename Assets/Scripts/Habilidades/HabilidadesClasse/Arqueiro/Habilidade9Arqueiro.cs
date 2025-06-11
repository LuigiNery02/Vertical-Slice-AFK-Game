using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9Arqueiro : HabilidadeBase
{
    private int _precisaoOriginal; //precisão original do personagem
    private int _esquivaOriginal; //esquiva original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _precisaoOriginal = personagem.personagem.precisao;
        _esquivaOriginal = personagem.personagem.esquiva;
        personagem.efeitoPorAtaque = AumentarStatus;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
    }

    private void AumentarStatus() //fuñção que aumenta status específicos do personagem
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.precisao += _precisaoOriginal; //aumenta em 100% a precisão
                personagem.personagem.esquiva += _esquivaOriginal; //aumenta em 100% a esquiva
                break;
            case 2:
                personagem.personagem.precisao += (_precisaoOriginal * 2); //aumenta em 200% a precisão
                personagem.personagem.esquiva += (_esquivaOriginal * 2); //aumenta em 200% a esquiva
                break;
            case 3:
                personagem.personagem.precisao += (_precisaoOriginal * 3); //aumenta em 300% a precisão
                personagem.personagem.esquiva += (_esquivaOriginal * 3); //aumenta em 300% a esquiva
                break;
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.precisao = _precisaoOriginal;
        personagem.personagem.esquiva = _esquivaOriginal;
        personagem.efeitoPorAtaqueAtivado = false;
    }
}
