using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9Mago : HabilidadeBase
{
    private float _ataqueMagicoOriginal; //ataque mágico original do personagem
    private float _defesaMagicaOriginal; //defesa mágico original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //aumenta o ataque mágico em 25%
                //aumenta a defesa mágica em 25%
                break;
            case 2:
                //aumenta o ataque mágico em 50%
                //aumenta a defesa mágica em 50%
                break;
            case 3:
                //aumenta o ataque mágico em 100%
                //aumenta a defesa mágica em 100%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
    }
}
