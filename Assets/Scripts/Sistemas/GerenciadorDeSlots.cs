using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorDeSlots : MonoBehaviour
{
    [Header("UI")]
    public Transform painelSlotsPersonagens; //painel de slots
    public GameObject botaoCriar; //bot�o de criar personagens
    public GameObject slotPersonagens; //slot do personagem
    public List<SlotPersonagem> slots; //lista de slots de personagens

    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens; //sistema de cria��o de personagens

    private void Start()
    {
        ConfigurarBotaoNovoPersonagem();
    }

    public void ConfigurarBotaoNovoPersonagem() //fun��o que configura o bot�o de criar personagens
    {
        botaoCriar.transform.SetAsLastSibling();
        botaoCriar.transform.localScale = Vector3.one;
        botaoCriar.GetComponent<Button>().onClick.AddListener(() =>
        {
            sistemaDeCriacaoDePersonagens.CriarPersonagem();
        });
    }

    public void AdicionarSlot() //fun��o que cria o slot do personagem
    {
        SlotPersonagem slotDisponivel = null;

        //procura um slot desativado na lista
        foreach(var slot in slots)
        {
            if(!slot.gameObject.activeSelf)
            {
                slotDisponivel = slot;
                break;
            }
        }

        if (slotDisponivel == null) //caso n�o haja slot dispon�vel
        {
            //instancia um novo slot
            GameObject novoSlotPrefab = Instantiate(slotPersonagens, painelSlotsPersonagens);
            novoSlotPrefab.transform.localScale = Vector3.one;

            //adiciona o slot � lista de slots
            slotDisponivel = novoSlotPrefab.GetComponent<SlotPersonagem>();
            slots.Add(slotDisponivel);
        }

        //reutiliza o slot
        slotDisponivel.gameObject.SetActive(true);
        slotDisponivel.ReceberDadosPersonagem(sistemaDeCriacaoDePersonagens.personagemEmCriacao);
        slotDisponivel.transform.SetSiblingIndex(painelSlotsPersonagens.childCount - 1); //antes do bot�o de criar personagens

        botaoCriar.transform.SetAsLastSibling();
        ConfigurarBotaoNovoPersonagem();
    }

    public void AtualizarSlots() //fun��o que atualiza os slots
    {
        int totalPersonagens = sistemaDeCriacaoDePersonagens.personagensCriados.Count; //total de personagens criados

        //garante que a lista de slots tenha a mesma quantidade que a de personagens
        for (int i = 0; i < totalPersonagens; i++)
        {
            //atualiza ou adiciona slots a depender do n�mero de personagens criados
            if (i < slots.Count)
            {
                //atualiza os dados do slot
                slots[i].personagemIndice = i;
                slots[i].ReceberDadosPersonagem(sistemaDeCriacaoDePersonagens.personagensCriados[i]);
            }
            else
            {
                //adiciona o slot
                GameObject novoSlotGO = Instantiate(slotPersonagens, painelSlotsPersonagens);
                novoSlotGO.transform.localScale = Vector3.one;

                var slot = novoSlotGO.GetComponent<SlotPersonagem>();
                slot.personagemIndice = i;
                slot.ReceberDadosPersonagem(sistemaDeCriacaoDePersonagens.personagensCriados[i]);

                slots.Add(slot);
            }
        }

        //desativa slots caso algum personagem tenha sido exclu�do
        for (int i = totalPersonagens; i < slots.Count; i++)
        {
            slots[i].gameObject.SetActive(false);
        }

        //ativa os slots dos personagens existentes
        for (int i = 0; i < totalPersonagens; i++)
        {
            if (!slots[i].gameObject.activeSelf)
            {
                slots[i].gameObject.SetActive(true);
            }  
        }

        //coloca o bot�o criar personagem ao final da lista
        botaoCriar.transform.SetAsLastSibling();
    }
}
