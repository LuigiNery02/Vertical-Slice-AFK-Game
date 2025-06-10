using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11Guerreiro : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    private List<float> _danoMeleeOriginal = new List<float>(); //dano de ataque melee original dos personagens aliados
    private List<float> _danoDistanciaOriginal = new List<float>(); //dano de ataque distancia original dos personagens aliados
    private List<float> _danoMagicoOriginal = new List<float>(); //dano de ataque magico original dos personagens aliados
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        listaDeAliados.Clear();
        _danoMeleeOriginal.Clear();
        _danoDistanciaOriginal.Clear();
        _danoMagicoOriginal.Clear();

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
            _danoMeleeOriginal.Insert(i, listaDeAliados[i]._danoAtaqueBasico);
            _danoDistanciaOriginal.Insert(i, listaDeAliados[i].danoAtaqueDistancia);
            _danoMagicoOriginal.Insert(i, listaDeAliados[i].danoAtaqueMagico);
        }

        switch (nivel)
        {
            case 1:
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i]._danoAtaqueBasico += (_danoMeleeOriginal[i] / 10) + personagem.personagem.suporte;
                        listaDeAliados[i].danoAtaqueDistancia += (_danoDistanciaOriginal[i] / 10) + personagem.personagem.suporte;
                        listaDeAliados[i].danoAtaqueMagico += (_danoMagicoOriginal[i] / 10) + personagem.personagem.suporte;
                    }
                }
                tempoDeEfeito = 3;
                break;
            case 2:
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i]._danoAtaqueBasico += (_danoMeleeOriginal[i] / 10) + personagem.personagem.suporte;
                        listaDeAliados[i].danoAtaqueDistancia += (_danoDistanciaOriginal[i] / 10) + personagem.personagem.suporte;
                        listaDeAliados[i].danoAtaqueMagico += (_danoMagicoOriginal[i] / 10) + personagem.personagem.suporte;
                    }
                }
                tempoDeEfeito = 5;
                break;
            case 3:
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i]._danoAtaqueBasico += (_danoMeleeOriginal[i] / 10) + personagem.personagem.suporte;
                        listaDeAliados[i].danoAtaqueDistancia += (_danoDistanciaOriginal[i] / 10) + personagem.personagem.suporte;
                        listaDeAliados[i].danoAtaqueMagico += (_danoMagicoOriginal[i] / 10) + personagem.personagem.suporte;
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
                listaDeAliados[i]._danoAtaqueBasico = _danoMeleeOriginal[i];
                listaDeAliados[i].danoAtaqueDistancia = _danoDistanciaOriginal[i];
                listaDeAliados[i].danoAtaqueMagico = _danoMagicoOriginal[i];
            }
        }
    }
}
