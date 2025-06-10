using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Habilidade9Guerreiro : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeInimigos = new List<IAPersonagemBase>(); //lista de inimigos na cena
    private int _numeroDeAtaques; //numero de ataques do personagem
    private int _ataquesAcertados; //número de ataques acertados pelo personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
        personagem.efeitoPorAtaque = AtacarTodos;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;

        _ataquesAcertados = 0;

        //cria a lista com os personagens que não são alvos do personagem
        listaDeInimigos.Clear();

        IAPersonagemBase[] inimigos = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase inimigo in inimigos)
        {
            if (inimigo.controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO && inimigo != personagem._personagemAlvo) //verifica se é personagem do jogador
            {
                listaDeInimigos.Add(inimigo);
            }
        }
    }

    private void AtacarTodos() //função que causa dano a todos os inimigos que não são alvos do personagem
    {
        if (personagem.efeitoPorAtaqueAtivado)
        {
            _ataquesAcertados++;

            switch (nivel)
            {
                case 1:
                    _numeroDeAtaques = 1;

                    for (int i = 0; i < listaDeInimigos.Count; i++)
                    {
                        listaDeInimigos[i].SofrerDano(personagem._danoAtaqueBasico - listaDeInimigos[i].personagem.defesa);

                        personagem.personagem.GanharEXP(personagem._danoAtaqueBasico - listaDeInimigos[i].personagem.defesa);

                        if (SistemaDeBatalha.usarSliders)
                        {
                            listaDeInimigos[i].textoHP.gameObject.SetActive(true);
                            listaDeInimigos[i].textoHP.text = ("-" + (personagem._danoAtaqueBasico - listaDeInimigos[i].personagem.defesa));
                            personagem.DesativarTextoHPPersonagem(listaDeInimigos[i]);
                        }
                    }

                    if (_ataquesAcertados == _numeroDeAtaques)
                    {
                        RemoverEfeito();
                        personagem.EsperarRecargaHabilidade(this, this.tempoDeRecarga);
                    }
                    break;
                case 2:
                    _numeroDeAtaques = 2;

                    for (int i = 0; i < listaDeInimigos.Count; i++)
                    {
                        listaDeInimigos[i].SofrerDano(personagem._danoAtaqueBasico - listaDeInimigos[i].personagem.defesa);

                        personagem.personagem.GanharEXP(personagem._danoAtaqueBasico - listaDeInimigos[i].personagem.defesa);

                        if (SistemaDeBatalha.usarSliders)
                        {
                            listaDeInimigos[i].textoHP.gameObject.SetActive(true);
                            listaDeInimigos[i].textoHP.text = ("-" + (personagem._danoAtaqueBasico - listaDeInimigos[i].personagem.defesa));
                            personagem.DesativarTextoHPPersonagem(listaDeInimigos[i]);
                        }
                    }

                    if (_ataquesAcertados == _numeroDeAtaques)
                    {
                        RemoverEfeito();
                        personagem.EsperarRecargaHabilidade(this, this.tempoDeRecarga);
                    }
                    break;
                case 3:
                    _numeroDeAtaques = 3;

                    for (int i = 0; i < listaDeInimigos.Count; i++)
                    {
                        listaDeInimigos[i].SofrerDano(personagem._danoAtaqueBasico - listaDeInimigos[i].personagem.defesa);

                        personagem.personagem.GanharEXP(personagem._danoAtaqueBasico - listaDeInimigos[i].personagem.defesa);

                        if (SistemaDeBatalha.usarSliders)
                        {
                            listaDeInimigos[i].textoHP.gameObject.SetActive(true);
                            listaDeInimigos[i].textoHP.text = ("-" + (personagem._danoAtaqueBasico - listaDeInimigos[i].personagem.defesa));
                            personagem.DesativarTextoHPPersonagem(listaDeInimigos[i]);
                        }
                    }

                    if (_ataquesAcertados == _numeroDeAtaques)
                    {
                        RemoverEfeito();
                        personagem.EsperarRecargaHabilidade(this, this.tempoDeRecarga);
                    }
                    break;
            }
        }
    }

    public void DesativarTextoHP()
    {
        for (int i = 0; i < listaDeInimigos.Count; i++)
        {
            listaDeInimigos[i].textoHP.gameObject.SetActive(false);
        }   
    }
    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = false;
        _ataquesAcertados = 0;
        listaDeInimigos = new List<IAPersonagemBase>(); //reseta a lista
    }
}
