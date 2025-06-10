using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade3Guerreiro : HabilidadeBase
{
    private float _hpOriginal; //hp original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _hpOriginal = personagem._hpMaximoEInicial;
        personagem.efeitoPorAtaque = RecuperarHP;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
    }

    private void RecuperarHP() //função que recupera hp
    {
        if (personagem.efeitoPorAtaqueAtivado)
        {
            switch (nivel)
            {
                case 1:
                    personagem.ReceberHP(_hpOriginal / 20); //recupera 5% de HP para cada ataque
                    break;
                case 2:
                    personagem.ReceberHP(_hpOriginal / 10); //recupera 10% de HP para cada ataque
                    break;
                case 3:
                    personagem.ReceberHP(_hpOriginal / 5); //recupera 20% de HP para cada ataque
                    break;
            }
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = false;
    }
}
