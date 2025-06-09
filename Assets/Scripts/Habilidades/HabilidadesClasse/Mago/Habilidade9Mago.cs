using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9Mago : HabilidadeBase
{
    private float _ataqueMagicoOriginal; //ataque m�gico original do personagem
    private float _defesaMagicaOriginal; //defesa m�gico original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //aumenta o ataque m�gico em 25%
                //aumenta a defesa m�gica em 25%
                break;
            case 2:
                //aumenta o ataque m�gico em 50%
                //aumenta a defesa m�gica em 50%
                break;
            case 3:
                //aumenta o ataque m�gico em 100%
                //aumenta a defesa m�gica em 100%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
    }
}
