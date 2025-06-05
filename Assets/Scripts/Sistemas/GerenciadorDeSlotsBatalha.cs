using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorDeSlotsBatalha : MonoBehaviour
{
    [Header("UI")]
    public Transform painelSlotsPersonagens; //painel de slots
    public GameObject slotPersonagens; //slot do personagem
    public List<SlotPersonagemBatalha> slots; //lista de slots de personagens

    public GerenciadorDePersonagens gerenciadirDePersonagens; //gerenciador de personagens

    private void Start()
    {
        AtualizarSlots();
    }

    public void AtualizarSlots()
    {
        // Desativa todos os slots existentes
        foreach (var slot in slots)
        {
            slot.gameObject.SetActive(false);
        }

        // Verifica se o gerenciador de personagens foi definido
        if (gerenciadirDePersonagens == null)
        {
            Debug.LogWarning("Gerenciador de personagens não atribuído.");
            return;
        }

        // Para cada personagem, ativa um slot correspondente
        for (int i = 0; i < gerenciadirDePersonagens.personagens.Count; i++)
        {
            slots[i].gameObject.SetActive(true);
            slots[i].ReceberDadosPersonagem(gerenciadirDePersonagens.personagens[i]);

            // Se quiser, aqui você pode associar os dados ao slot:
            //slots[i].ConfigurarSlot(gerenciadirDePersonagens.personagens[i]); // precisa de um método no slot
        }
    }
}
