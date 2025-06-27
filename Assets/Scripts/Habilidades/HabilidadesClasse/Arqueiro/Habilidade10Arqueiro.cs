using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10Arqueiro : HabilidadeBase
{
    private float _danoOriginal; //dano original do personagem
    private float _defesaOriginal; //defesa original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //_danoOriginal = personagem.danoAtaqueDistancia;
        _defesaOriginal = personagem.personagem.defesa;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.danoAtaqueDistancia -= (_danoOriginal / 20); //diminui o ataque em 5%
                personagem.personagem.defesa += (_defesaOriginal); //aumenta a defesa em 100%
                break;
            case 2:
                //personagem.danoAtaqueDistancia -= (_danoOriginal / 10); //diminui o ataque em 10%
                personagem.personagem.defesa += (_defesaOriginal * 2); //aumenta a defesa em 200%
                break;
            case 3:
                //personagem.danoAtaqueDistancia -= (_danoOriginal / 5); //diminui o ataque em 20%
                personagem.personagem.defesa += (_defesaOriginal * 4); //aumenta a defesa em 400%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.danoAtaqueDistancia = _danoOriginal;
        personagem.personagem.defesa = _defesaOriginal;
    }
}
