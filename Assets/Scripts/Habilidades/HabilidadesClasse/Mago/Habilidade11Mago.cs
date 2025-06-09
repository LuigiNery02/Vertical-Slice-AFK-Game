using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11Mago : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    private int[] _pontosHabilidadeForcaOriginal;
    private int[] _pontosHabilidadeAgilidadeOriginal;
    private int[] _pontosHabilidadeDestrezaOriginal;
    private int[] _pontosHabilidadeConstituicaoOriginal;
    private int[] _pontosHabilidadeInteligenciaOriginal;
    private int[] _pontosHabilidadeSabedoriaOriginal;

    private int _pontosHabilidades;
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //encontra todos os aliados na cena
        //define os dados dos personagens
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                _pontosHabilidades = 2;
                //aumenta em 2 todos os pontos de habilidades dos personagens + os pontos de suporte do personagem
                tempoDeEfeito = 3;
                break;
            case 2:
                _pontosHabilidades = 4;
                //aumenta em 4 todos os pontos de habilidades dos personagens + os pontos de suporte do personagem
                tempoDeEfeito = 5;
                break;
            case 3:
                _pontosHabilidades = 6;
                //aumenta em 6 todos os pontos de habilidades dos personagens + os pontos de suporte do personagem
                tempoDeEfeito = 10;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
    }
}
