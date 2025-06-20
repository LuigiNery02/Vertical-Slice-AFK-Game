using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade4Guerreiro : HabilidadeBase
{
    private float _hpOriginal; //dano original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _hpOriginal = personagem._hpMaximoEInicial;
        personagem.efeitoPorAtaque = RecuperarHP;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
    }

    private void RecuperarHP() //fun��o que recupera hp
    {
        if (personagem.efeitoPorAtaqueAtivado)
        {
            switch (nivel)
            {
                case 1:
                    personagem.ReceberHP(_hpOriginal / _hpOriginal); //recupera 1% de HP 
                    break;
                case 2:
                    personagem.ReceberHP(_hpOriginal / 67); //recupera 1.5% de HP 
                    break;
                case 3:
                    personagem.ReceberHP(_hpOriginal / 50); //recupera 2% de HP 
                    break;
            }
        }
    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = false;
    }
}
