using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12Arqueiro : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    private List<int> _esquivaOriginal = new List<int>(); //esquiva original dos personagens aliados
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //atribui cada esquiva a cada personagem aliado da lista
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        listaDeAliados.Clear();
        _esquivaOriginal.Clear();

        IAPersonagemBase[] aliados = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase aliado in aliados)
        {
            if (aliado.controlador == personagem.controlador && aliado != personagem) //verifica se é personagem do jogador
            {
                listaDeAliados.Add(aliado);
            }
        }

        for (int i = 0; i < listaDeAliados.Count; i++)
        {
            _esquivaOriginal.Insert(i, listaDeAliados[i].personagem.esquiva);
        }

        switch (nivel)
        {
            case 1:
                //aumenta a esquiva dos personagens aliados em 100%
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i].personagem.esquiva += _esquivaOriginal[i];
                    }
                }
                tempoDeEfeito = 3;
                break;
            case 2:
                //aumenta a esquiva dos personagens aliados em 200%
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i].personagem.esquiva += (_esquivaOriginal[i] * 2);
                    }
                }
                tempoDeEfeito = 5;
                break;
            case 3:
                //aumenta a esquiva dos personagens aliados em 300%
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i].personagem.esquiva += (_esquivaOriginal[i] * 3);
                    }
                }
                tempoDeEfeito = 10;
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        for (int i = 0; i < listaDeAliados.Count; i++)
        {
            if (listaDeAliados[i] != null)
            {
                listaDeAliados[i].personagem.esquiva = _esquivaOriginal[i];
            }
        }
    }
}
