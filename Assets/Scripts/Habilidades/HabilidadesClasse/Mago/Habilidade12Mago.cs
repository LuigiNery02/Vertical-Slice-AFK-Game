using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12Mago : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        listaDeAliados.Clear();

        IAPersonagemBase[] aliados = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase aliado in aliados)
        {
            if (aliado.controlador == personagem.controlador && aliado != personagem) //verifica se é personagem do jogador
            {
                listaDeAliados.Add(aliado);
            }
        }

        switch (nivel)
        {
            case 1:
                //aumenta em 25% o hp dos aliados
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    listaDeAliados[i].ReceberHP((listaDeAliados[i]._hpMaximoEInicial / 4));
                }
                break;
            case 2:
                //aumenta em 50% o hp dos aliados
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    listaDeAliados[i].ReceberHP((listaDeAliados[i]._hpMaximoEInicial / 2));
                }
                break;
            case 3:
                //aumenta em 100% o hp dos aliados
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    listaDeAliados[i].ReceberHP(listaDeAliados[i]._hpMaximoEInicial);
                }
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
