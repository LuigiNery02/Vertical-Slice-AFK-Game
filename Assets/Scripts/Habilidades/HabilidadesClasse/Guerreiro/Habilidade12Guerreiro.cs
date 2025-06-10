using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12Guerreiro : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    private List<float> _velocidadeDeAtaqueOriginal = new List<float>(); //velocidade de ataque original dos personagens aliados
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
        
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        listaDeAliados.Clear();
        _velocidadeDeAtaqueOriginal.Clear();

        IAPersonagemBase[] aliados = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase aliado in aliados)
        {
            if (aliado.controlador == personagem.controlador && aliado != personagem) //verifica se é personagem do jogador
            {
                listaDeAliados.Add(aliado);
            }
        }

        for(int i = 0; i < listaDeAliados.Count; i++)
        {
            _velocidadeDeAtaqueOriginal.Insert(i, listaDeAliados[i]._cooldown);
        }

        switch (nivel)
        {
            case 1:
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i]._cooldown -= 0.1f;
                    }
                }
                tempoDeEfeito = 3;
                break;
            case 2:
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i]._cooldown -= 0.2f;
                    }
                }
                tempoDeEfeito = 5;
                break;
            case 3:
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i]._cooldown -= 0.3f;
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
                listaDeAliados[i]._cooldown = _velocidadeDeAtaqueOriginal[i];
            }
        }
    }
}
