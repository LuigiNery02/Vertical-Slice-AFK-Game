using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDePersonagens : MonoBehaviour
{
    public List<PersonagemData> personagens = new List<PersonagemData>(); //lista de personagens criados

    public IAPersonagemBase[] personagem; //personagens do jogador

    [SerializeField]
    private int _personagensSelecionados; //número de personagens selecionados à batalha

    private void Awake()
    {
        if (GerenciadorDeInventario.instancia != null)
        {
            personagens = GerenciadorDeInventario.instancia.personagensCriados;
        }
    }

    public void SelecionarPersonagem(PersonagemData personagemBase, SlotPersonagemBatalha slot) //função que seleciona os personagens para à batalha
    {
        if (!slot.slotSelecionado && _personagensSelecionados != 3)
        {
            int indiceLivre = EncontrarIndiceLivre();

            if (indiceLivre != -1)
            {
                slot.slotSelecionado = true;
                slot.personagemIndice = indiceLivre;
                personagem[indiceLivre].personagem = personagemBase;
                _personagensSelecionados++;
                personagem[indiceLivre].gameObject.SetActive(true);
                personagem[indiceLivre].ReceberDadosPersonagem();
            }
        }
        else
        {
            slot.slotSelecionado = false;
            personagem[slot.personagemIndice].ResetarDadosPersonagem();
            personagem[slot.personagemIndice].personagem = null;
            slot.personagemIndice = -1;
            _personagensSelecionados--;
        }
    }
    private int EncontrarIndiceLivre() //encontra o índice livre dentre os personagens
    {
        for (int i = 0; i < personagem.Length; i++)
        {
            if (personagem[i].personagem == null || personagem[i].personagem.codigoID == "" || personagem[i].personagem.codigoID == null)
            {
                return i;
            }
        }
        return -1;
    }
}
