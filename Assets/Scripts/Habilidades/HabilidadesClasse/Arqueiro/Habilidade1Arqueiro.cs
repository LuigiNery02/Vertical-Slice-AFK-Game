using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade1Arqueiro : HabilidadeBase
{
    private float _velocidadeProjetilOriginal; //velocidade original do projétil do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _velocidadeProjetilOriginal = personagem.velocidadeDoProjetil;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.velocidadeDoProjetil += (_velocidadeProjetilOriginal / 10);
                break;
            case 2:
                personagem.velocidadeDoProjetil += (_velocidadeProjetilOriginal / 5);
                break;
            case 3:
                personagem.velocidadeDoProjetil += (_velocidadeProjetilOriginal / 10) * 3;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.velocidadeDoProjetil = _velocidadeProjetilOriginal;
    }
}
