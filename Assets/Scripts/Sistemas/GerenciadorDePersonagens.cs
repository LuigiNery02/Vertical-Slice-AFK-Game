using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDePersonagens : MonoBehaviour
{
    public List<PersonagemData> personagens = new List<PersonagemData>();
    public IAPersonagemBase[] personagem; //personagens fixos na cena
    public List<IAPersonagemBase> personagensAtivos = new List<IAPersonagemBase>();
    public GerenciadorDeSlotsBatalha gerenciadorDeSlots;
    public GameObject telaBatalha;
    [HideInInspector] 
    public int _personagensSelecionados;

    private void Awake()
    {
        if (GerenciadorDeInventario.instancia != null)
        {
            personagens = GerenciadorDeInventario.instancia.personagensCriados;
            gerenciadorDeSlots.AtualizarSlots();
        }
    }

    public void SelecionarPersonagem(PersonagemData personagemBase, SlotPersonagemBatalha slot)
    {
        if (!slot.slotSelecionado)
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

                AtualizarListaAtivos();
            }
        }
        else
        {
            slot.slotSelecionado = false;
            personagem[slot.personagemIndice].ResetarDadosPersonagem();
            personagem[slot.personagemIndice].personagem = null;
            slot.personagemIndice = -1;
            _personagensSelecionados--;
            if (_personagensSelecionados < 0)
            {
                _personagensSelecionados = 0;
            }

            AtualizarListaAtivos();
        }

        telaBatalha.SetActive(_personagensSelecionados > 0);
    }

    private int EncontrarIndiceLivre()
    {
        for (int i = 0; i < personagem.Length; i++)
        {
            if (personagem[i].personagem == null || string.IsNullOrEmpty(personagem[i].personagem.codigoID))
                return i;
        }
        return -1;
    }

    private void AtualizarListaAtivos()
    {
        personagensAtivos.Clear();

        SlotPersonagemBatalha[] slots = FindObjectsOfType<SlotPersonagemBatalha>(true);

        foreach (var p in personagem)
        {
            bool estaSelecionado = false;

            foreach (var slot in slots)
            {
                if (slot.slotSelecionado && slot.personagemIndice >= 0 && personagem[slot.personagemIndice] == p)
                {
                    estaSelecionado = true;
                    break;
                }
            }

            if (estaSelecionado && p != null && p.personagem != null)
            {
                personagensAtivos.Add(p);
            }
        }
    }

    public void RestaurarSlotsSelecionados()
    {
        _personagensSelecionados = 0;
        personagensAtivos.Clear();

        SlotPersonagemBatalha[] slots = FindObjectsOfType<SlotPersonagemBatalha>(true);
        foreach (var slot in slots)
        {
            slot.slotSelecionado = false;
            slot.personagemIndice = -1;
            slot.check.SetActive(false);
        }

        foreach (var p in personagem)
        {
            if (p != null)
            {
                p.ResetarDadosPersonagem();
                p.personagem = null;
                p.gameObject.SetActive(false);
            }
        }

        foreach (var personagemIA in personagem)
        {
            if (personagemIA != null && personagemIA.personagem != null)
            {
                foreach (var slot in slots)
                {
                    if (slot.personagemData != null && slot.personagemData.codigoID == personagemIA.personagem.codigoID)
                    {
                        slot.slotSelecionado = true;
                        slot.personagemIndice = EncontrarIndiceDoPersonagem(personagemIA);
                        slot.check.SetActive(true);

                        _personagensSelecionados++;
                        if (!personagensAtivos.Contains(personagemIA))
                        {
                            personagensAtivos.Add(personagemIA);
                        }

                        personagemIA.ReceberDadosPersonagem();
                        personagemIA.gameObject.SetActive(true);
                        break;
                    }
                }
            }
        }

        telaBatalha.SetActive(_personagensSelecionados > 0);
    }

    private int EncontrarIndiceDoPersonagem(IAPersonagemBase p)
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
