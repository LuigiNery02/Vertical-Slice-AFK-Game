using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade6Guerreiro : HabilidadeBase
{
    private float _defesaOriginal; //defesa original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _defesaOriginal = personagem.personagem.defesa;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.defesa += (_defesaOriginal / 2); //aumenta em 50% a defesa do personagem
                break;
            case 2:
                personagem.personagem.defesa += _defesaOriginal; //aumenta em 100% a defesa do personagem
                break;
            case 3:
                personagem.personagem.defesa += (_defesaOriginal * 2); //aumenta em 200% a defesa do personagem
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.defesa = _defesaOriginal;
    }
}
