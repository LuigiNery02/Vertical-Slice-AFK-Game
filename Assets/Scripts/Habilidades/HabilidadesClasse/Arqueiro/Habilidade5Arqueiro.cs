using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5Arqueiro : HabilidadeBase
{
    private List<int> _esquivaOriginal = new List<int>(); //esquiva original do personagem
    private List<IAPersonagemBase> listaDeInimigos = new List<IAPersonagemBase>(); //lista de inimigos na cena
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais dos personagens
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        listaDeInimigos.Clear();
        _esquivaOriginal.Clear();

        IAPersonagemBase[] inimigos = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase inimigo in inimigos)
        {
            if (inimigo.controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO) //verifica se é personagem inimigo
            {
                listaDeInimigos.Add(inimigo);
            }
        }

        for (int i = 0; i < listaDeInimigos.Count; i++)
        {
            _esquivaOriginal.Insert(i, listaDeInimigos[i].personagem.esquiva);
        }

        switch (nivel)
        {
            case 1:
                //diminui 100% a esquiva de todos os inimigos
                for (int i = 0; i < listaDeInimigos.Count; i++)
                {
                    listaDeInimigos[i].personagem.esquiva -= _esquivaOriginal[i];
                }
                break;
            case 2:
                //diminui 200% a esquiva de todos os inimigos
                for (int i = 0; i < listaDeInimigos.Count; i++)
                {
                    listaDeInimigos[i].personagem.esquiva -= (_esquivaOriginal[i] * 2);
                }
                break;
            case 3:
                //diminui 300% a esquiva de todos os inimigos
                for (int i = 0; i < listaDeInimigos.Count; i++)
                {
                    listaDeInimigos[i].personagem.esquiva -= (_esquivaOriginal[i] * 3);
                }
                break;
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais dos personagens
        for (int i = 0; i < listaDeInimigos.Count; i++)
        {
            if (listaDeInimigos[i] != null)
            {
                listaDeInimigos[i].personagem.esquiva = _esquivaOriginal[i];
            }
        }
    }
}
