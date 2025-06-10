using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5Guerreiro : HabilidadeBase
{
    private float _danoOriginal; //dano original do personagem
    private List<IAPersonagemBase> listaDeInimigos = new List<IAPersonagemBase>(); //lista de inimigos na cena
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _danoOriginal = personagem._danoAtaqueBasico;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        listaDeInimigos.Clear();

        IAPersonagemBase[] inimigos = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase inimigo in inimigos)
        {
            if (inimigo.controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO) //verifica se é personagem do jogador
            {
                listaDeInimigos.Add(inimigo);
            }
        }

        switch (nivel)
        {
            case 1:
                //coloca o personagem como alvo de todos os inimigos
                for(int i = 0; i < listaDeInimigos.Count; i++)
                {
                    listaDeInimigos[i]._personagemAlvo = personagem;
                    listaDeInimigos[i].VerificarComportamento("selecionarAlvo");
                }
                personagem._danoAtaqueBasico += (_danoOriginal / 20); //aumenta o dano em 5%
                break;
            case 2:
                //coloca o personagem como alvo de todos os inimigos
                for (int i = 0; i < listaDeInimigos.Count; i++)
                {
                    listaDeInimigos[i]._personagemAlvo = personagem;
                    listaDeInimigos[i].VerificarComportamento("selecionarAlvo");
                }
                personagem._danoAtaqueBasico += (_danoOriginal / 10); //aumenta o dano em 10%
                break;
            case 3:
                //coloca o personagem como alvo de todos os inimigos
                for (int i = 0; i < listaDeInimigos.Count; i++)
                {
                    listaDeInimigos[i]._personagemAlvo = personagem;
                    listaDeInimigos[i].VerificarComportamento("selecionarAlvo");
                }
                personagem._danoAtaqueBasico += (_danoOriginal / 5); //aumenta o dano em 20%
                break;
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem._danoAtaqueBasico = _danoOriginal;
    }
}
