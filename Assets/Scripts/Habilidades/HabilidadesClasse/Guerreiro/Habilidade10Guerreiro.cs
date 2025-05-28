using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10Guerreiro : HabilidadeBase
{
    private float _esquivaOriginal; //esquiva original do personagem
    private float _defesaOriginal; //defesa original do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //_esquivaOriginal = personagem.esquiva
        //_defesaOriginal = personagem.defesa;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //checa se o personagem esquivou: se sim aumenta a defesa do personagem 10%
                break;
            case 2:
                //checa se o personagem esquivou: se sim aumenta a defesa do personagem 20%
                break;
            case 3:
                //checa se o personagem esquivou: se sim aumenta a defesa do personagem 30%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.esquiva = _esquivaOriginal;
        //personagem.defesa = _defesaOriginal;
    }
}
