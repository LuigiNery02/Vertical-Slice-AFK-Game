using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade3Arqueiro : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _danoOriginal = personagem.personagem.arma.danoDistancia;
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
                personagem.ReceberHP(_danoOriginal / 20); //recupera HP referente a 5% do dano da arma
                break;
            case 2:
                personagem.ReceberHP(_danoOriginal / 10); //recupera HP referente a 10% do dano da arma
                break;
            case 3:
                personagem.ReceberHP(_danoOriginal / 5); //recupera HP referente a 20% do dano da arma
                break;
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = false;
    }
}
