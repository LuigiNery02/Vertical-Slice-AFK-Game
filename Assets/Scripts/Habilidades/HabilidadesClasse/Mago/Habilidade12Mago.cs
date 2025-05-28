using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12Mago : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //encontra todos os aliados na cena
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //aumenta em 25% o hp dos aliados
                break;
            case 2:
                //aumenta em 50% o hp dos aliados
                break;
            case 3:
                //aumenta em 100% o hp dos aliados
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
