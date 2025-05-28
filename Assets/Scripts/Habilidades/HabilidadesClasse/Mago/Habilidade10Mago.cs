using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10Mago : HabilidadeBase
{
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
                //checa se o personagem tomou dano: se sim aumenta a defesa do personagem em 10%
                break;
            case 2:
                //checa se o personagem tomou dano: se sim aumenta a defesa do personagem em 20%
                break;
            case 3:
                //checa se o personagem tomou dano: se sim aumenta a defesa do personagem em 30%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.defesa = _defesaOriginal;
    }
}
