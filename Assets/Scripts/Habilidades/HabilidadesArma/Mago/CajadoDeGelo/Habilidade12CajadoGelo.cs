using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12CajadoGelo : HabilidadeBase
{
    private int _sabedoriaOriginal; //valor original da sabedoria do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        _sabedoriaOriginal = personagem.personagem.sabedoria;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.sabedoria += 3; //ganha 3 pontos de sabedoria
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 2:
                personagem.personagem.sabedoria += 6; //ganha 6 pontos de sabedoria
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
            case 3:
                personagem.personagem.sabedoria += 10; //ganha 10 pontos de sabedoria
                personagem.personagem.DefinicoesBatalha();
                personagem.AtualizarDadosBatalha();
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.personagem.sabedoria = _sabedoriaOriginal;
        personagem.personagem.DefinicoesBatalha();
        personagem.AtualizarDadosBatalha();
    }
}
