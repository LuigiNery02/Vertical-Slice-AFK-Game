using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11Arqueiro : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    private List<float> _defesaOriginal =  new List<float>(); //defesa original dos personagens aliados
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        listaDeAliados.Clear();
        _defesaOriginal.Clear();

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
            _defesaOriginal.Insert(i, listaDeAliados[i].personagem.defesa);
        }

        switch (nivel)
        {
            case 1:
                //aumenta a defesa dos peronsagens aliados em 10% + os pontos de suporte do personagem
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i].personagem.defesa += (_defesaOriginal[i] / 10) + personagem.personagem.suporte;
                    }
                }
                tempoDeEfeito = 3;
                break;
            case 2:
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i].personagem.defesa += (_defesaOriginal[i] / 10) + personagem.personagem.suporte;
                    }
                }
                tempoDeEfeito = 5;
                break;
            case 3:
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i].personagem.defesa += (_defesaOriginal[i] / 10) + personagem.personagem.suporte;
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
                listaDeAliados[i].personagem.defesa = _defesaOriginal[i];
            }
        }
    }
}
