using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotPersonagem : MonoBehaviour
{
    public PersonagemData personagemData; //personagem 
    public Text apelidoPersonagem; //texto do apelido do personagem
    public Image imagemPersonagem; //imagem do personagem
    public Button botaoEditarPersonagem; //botão de editar personagem
    public Button botaoDeletarPersonagem; //botão de deletar personagem
    public Sprite[] sprites; //sprites dos personagens
    [HideInInspector]
    public int personagemIndice; //indice do personagem 
    [HideInInspector]
    public int imagemClasse; //variável que verifica a imagem do personagem a depender de sua classe

    private SistemaDeCriacaoDePersonagens _sistemaDeCriacaoDePersonagens; //sistema de criação de personagens

    private void Start()
    {
        _sistemaDeCriacaoDePersonagens = FindObjectOfType<SistemaDeCriacaoDePersonagens>(); //encontra o sistema de criação de personagens na cena
        botaoEditarPersonagem.onClick.AddListener(() =>
        {
            EditarPersonagem();
        });
        botaoDeletarPersonagem.onClick.AddListener(() =>
        {
            DeletarPersonagem();
        });
    }

    public void ReceberDadosPersonagem(PersonagemData personagem) //função que recebe os dados do personagem e atualiza o slot
    {
        personagemData = personagem;
        apelidoPersonagem.text = personagem.apelido;
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

    private void EditarPersonagem() //função de editar o personagem
    {
        _sistemaDeCriacaoDePersonagens.ResetarTelaPersonagem();
        _sistemaDeCriacaoDePersonagens.personagemEmCriacao = _sistemaDeCriacaoDePersonagens.personagensCriados[personagemIndice];
        _sistemaDeCriacaoDePersonagens._imagemClasseAtual = imagemClasse;
        _sistemaDeCriacaoDePersonagens.AtualizarTelaPersonagem();
        _sistemaDeCriacaoDePersonagens.EditarPersonagem();
    }

    private void DeletarPersonagem() //função de deletar o personagem
    {
        _sistemaDeCriacaoDePersonagens.DeletarPersonagemCriado(personagemIndice);
    }
}
