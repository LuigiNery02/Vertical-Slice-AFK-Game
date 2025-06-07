using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotPersonagemBatalha : MonoBehaviour
{
    public PersonagemData personagemData; //personagem 
    public Text apelidoPersonagem; //texto do apelido do personagem
    public Text nivelPersonagem; //texto do n�vel do personagem
    public Image imagemPersonagem; //imagem do personagem
    public Sprite[] sprites; //sprites dos personagens
    [HideInInspector]
    public int personagemIndice; //indice do personagem 
    [HideInInspector]
    public int imagemClasse; //vari�vel que verifica a imagem do personagem a depender de sua classe
    public Button botao; //bot�o do slot
    //[HideInInspector]
    public bool slotSelecionado; //vari�vel que verifica se o slot foi selecionado

    private GerenciadorDePersonagens _gerenciadorDePersonagens; //gerenciador de personagens

    public void SelecionarPersonagem() //fun��o que define o personagem selecionado pelo seu slot
    {
        _gerenciadorDePersonagens = FindObjectOfType<GerenciadorDePersonagens>();
        _gerenciadorDePersonagens.SelecionarPersonagem(personagemData, this);
    }

    public void ReceberDadosPersonagem(PersonagemData personagem) //fun��o que recebe os dados do personagem e atualiza o slot
    {
        personagemData = personagem;
        apelidoPersonagem.text = personagem.apelido;
        nivelPersonagem.text = ("Nv: " + personagem.nivel.ToString());
        switch (personagem.classe)
        {
            case Classe.Guerreiro:
                imagemClasse = 0;
                break;
            case Classe.Arqueiro:
                imagemClasse = 1;
                break;
            case Classe.Mago:
                imagemClasse = 2;
                break;
        }
        imagemPersonagem.sprite = sprites[imagemClasse];
    }
}
