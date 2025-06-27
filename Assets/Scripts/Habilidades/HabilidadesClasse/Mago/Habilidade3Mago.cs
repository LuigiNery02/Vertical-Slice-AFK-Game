using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade3Mago : HabilidadeBase
{
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        personagem.efeitoPorAtaque = RecuperarHP;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
    }

    private void RecuperarHP() //função que recupera hp
    {
        switch (nivel)
        {
            case 1:
                //personagem.ReceberHP(personagem.danoAtaqueMagico / 20); //recupera 5% de HP referente ao dano causado
                break;
            case 2:
                //personagem.ReceberHP(personagem.danoAtaqueMagico / 10); //recupera 10% de HP referente ao dano causado
                break;
            case 3:
                //personagem.ReceberHP(personagem.danoAtaqueMagico / 5); //recupera 20% de HP referente ao dano causado
                break;
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = false;
    }
}
