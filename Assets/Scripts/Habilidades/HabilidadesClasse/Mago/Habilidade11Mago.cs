using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11Mago : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeAliados = new List<IAPersonagemBase>(); //lista de aliados na cena
    private List<int> _pontosHabilidadeForcaOriginal = new List<int>();
    private List<int> _pontosHabilidadeAgilidadeOriginal = new List<int>();
    private List<int> _pontosHabilidadeDestrezaOriginal = new List<int>();
    private List<int> _pontosHabilidadeConstituicaoOriginal = new List<int>();
    private List<int> _pontosHabilidadeInteligenciaOriginal = new List<int>();
    private List<int> _pontosHabilidadeSabedoriaOriginal = new List<int>();
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        listaDeAliados.Clear();
        _pontosHabilidadeForcaOriginal.Clear();
        _pontosHabilidadeAgilidadeOriginal.Clear();
        _pontosHabilidadeDestrezaOriginal.Clear();
        _pontosHabilidadeConstituicaoOriginal.Clear();
        _pontosHabilidadeInteligenciaOriginal.Clear();
        _pontosHabilidadeSabedoriaOriginal.Clear();

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
            _pontosHabilidadeForcaOriginal.Insert(i, listaDeAliados[i].personagem.forca);
            _pontosHabilidadeAgilidadeOriginal.Insert(i, listaDeAliados[i].personagem.agilidade);
            _pontosHabilidadeDestrezaOriginal.Insert(i, listaDeAliados[i].personagem.destreza);
            _pontosHabilidadeConstituicaoOriginal.Insert(i, listaDeAliados[i].personagem.constituicao);
            _pontosHabilidadeInteligenciaOriginal.Insert(i, listaDeAliados[i].personagem.inteligencia);
            _pontosHabilidadeSabedoriaOriginal.Insert(i, listaDeAliados[i].personagem.sabedoria);
        }

        switch (nivel)
        {
            case 1:
                //aumenta em 2 todos os pontos de habilidades dos personagens + os pontos de suporte do personagem
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i].personagem.forca += (2 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.agilidade += (2 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.destreza += (2 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.constituicao += (2 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.inteligencia += (2 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.sabedoria += (2 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.DefinicoesBatalha();
                        listaDeAliados[i].AtualizarDadosBatalha();
                    }
                }
                tempoDeEfeito = 3;
                break;
            case 2:
                //aumenta em 4 todos os pontos de habilidades dos personagens + os pontos de suporte do personagem
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i].personagem.forca += (4 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.agilidade += (4 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.destreza += (4 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.constituicao += (4 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.inteligencia += (4 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.sabedoria += (4 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.DefinicoesBatalha();
                        listaDeAliados[i].AtualizarDadosBatalha();
                    }
                }

                tempoDeEfeito = 5;
                break;
            case 3:
                //aumenta em 6 todos os pontos de habilidades dos personagens + os pontos de suporte do personagem
                for (int i = 0; i < listaDeAliados.Count; i++)
                {
                    if (listaDeAliados[i] != null)
                    {
                        listaDeAliados[i].personagem.forca += (6 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.agilidade += (6 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.destreza += (6 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.constituicao += (6 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.inteligencia += (6 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.sabedoria += (6 + ((int)personagem.personagem.suporte));
                        listaDeAliados[i].personagem.DefinicoesBatalha();
                        listaDeAliados[i].AtualizarDadosBatalha();
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
                listaDeAliados[i].personagem.forca = _pontosHabilidadeForcaOriginal[i];
                listaDeAliados[i].personagem.agilidade = _pontosHabilidadeAgilidadeOriginal[i];
                listaDeAliados[i].personagem.destreza = _pontosHabilidadeDestrezaOriginal[i];
                listaDeAliados[i].personagem.constituicao = _pontosHabilidadeConstituicaoOriginal[i];
                listaDeAliados[i].personagem.inteligencia = _pontosHabilidadeInteligenciaOriginal[i];
                listaDeAliados[i].personagem.sabedoria = _pontosHabilidadeSabedoriaOriginal[i];
                listaDeAliados[i].personagem.DefinicoesBatalha();
                listaDeAliados[i].AtualizarDadosBatalha();
            }
        }
    }
}
