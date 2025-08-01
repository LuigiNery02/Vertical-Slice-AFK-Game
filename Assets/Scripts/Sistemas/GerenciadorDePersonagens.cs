using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorDePersonagens : MonoBehaviour
{
    public List<PersonagemData> personagens = new List<PersonagemData>(); //lista de personagens criados

    public IAPersonagemBase[] personagem; //personagens do jogador

    public GerenciadorDeSlotsBatalha gerenciadorDeSlots;

    public GameObject telaBatalha; //tela de batalha

    [SerializeField]
    public int _personagensSelecionados; //n�mero de personagens selecionados � batalha

    private void Awake()
    {
        if (GerenciadorDeInventario.instancia != null)
        {
            personagens = GerenciadorDeInventario.instancia.personagensCriados;
            gerenciadorDeSlots.AtualizarSlots();
        }
    }

    public void SelecionarPersonagem(PersonagemData personagemBase, SlotPersonagemBatalha slot) //fun��o que seleciona os personagens para � batalha
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
                if (_personagensSelecionados >= 3)
                {
                    _personagensSelecionados = 3;
                }
                personagem[indiceLivre].gameObject.SetActive(true);
                personagem[indiceLivre].ReceberDadosPersonagem();
            }
        }
        else
        {
            if (slot.slotSelecionado)
            {
                slot.slotSelecionado = false;
                personagem[slot.personagemIndice].ResetarDadosPersonagem();
                personagem[slot.personagemIndice].personagem = null;
                slot.personagemIndice = -1;
                _personagensSelecionados--;
                if(_personagensSelecionados <= 0)
                {
                    _personagensSelecionados = 0;
                }
            }
        }

        if(_personagensSelecionados == 3)
        {
            telaBatalha.SetActive(true);
        }
        else
        {
            telaBatalha.SetActive(false);
        }
    }
    private int EncontrarIndiceLivre() //encontra o �ndice livre dentre os personagens
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

    public void RestaurarSlotsSelecionados() //fun��o que restaura os slots selecionados
    {
        SlotPersonagemBatalha[] slots = FindObjectsOfType<SlotPersonagemBatalha>(true);

        foreach (var personagemIA in personagem)
        {
            if (personagemIA != null && personagemIA.personagem != null)
            {
                foreach (var slot in slots)
                {
                    if (slot.personagemData.codigoID == personagemIA.personagem.codigoID)
                    {
                        slot.slotSelecionado = true;
                        slot.personagemIndice = EncontrarIndiceDoPersonagem(personagemIA);
                        _personagensSelecionados++;
                        if (_personagensSelecionados >= 3)
                        {
                            _personagensSelecionados = 3;
                        }

                        personagemIA.ReceberDadosPersonagem();
                        break;
                    }
                }
            }
        }
    }


    private int EncontrarIndiceDoPersonagem(IAPersonagemBase p) //fun��o que encontra um �ndice dispon�vel para o personagem
    {
        for (int i = 0; i < personagem.Length; i++)
        {
            if (personagem[i] == p)
            {
                return i;
            }
        }
        return -1;
    }
}
