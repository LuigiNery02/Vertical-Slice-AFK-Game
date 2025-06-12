using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2ArcoFogo : HabilidadeBase
{
    private float _danoOriginal; //dano original da arma
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais da arma do personagem
        _danoOriginal = personagem.personagem.arma.dano;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.arma.dano += (_danoOriginal / 20); //aumenta o dano da arma em 5%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.personagem.arma.dano += (_danoOriginal / 10); //aumenta o dano da arma em 10%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.personagem.arma.dano += (_danoOriginal / 5); //aumenta o dano da arma em 20%
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.arma.dano = _danoOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
    }
}
