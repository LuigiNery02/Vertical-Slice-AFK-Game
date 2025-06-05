using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorDeSlots : MonoBehaviour
{
    [Header("UI")]
    public Transform painelSlotsPersonagens; //painel de slots
    public GameObject botaoCriar; //botão de criar personagens
    public GameObject slotPersonagens; //slot do personagem
    public List<SlotPersonagem> slots; //lista de slots de personagens

    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens; //sistema de criação de personagens

    private void Start()
    {
        ConfigurarBotaoNovoPersonagem();
    }

    public void ConfigurarBotaoNovoPersonagem() //função que configura o botão de criar personagens
    {
        botaoCriar.transform.SetAsLastSibling();
        botaoCriar.transform.localScale = Vector3.one;
        botaoCriar.GetComponent<Button>().onClick.AddListener(() =>
        {
            sistemaDeCriacaoDePersonagens.CriarPersonagem();
        });
    }

    public void AdicionarSlot() //função que cria o slot do personagem
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

        if (slotDisponivel == null) //caso não haja slot disponível
        {
            //instancia um novo slot
            GameObject novoSlotPrefab = Instantiate(slotPersonagens, painelSlotsPersonagens);
            novoSlotPrefab.transform.localScale = Vector3.one;

            //adiciona o slot à lista de slots
            slotDisponivel = novoSlotPrefab.GetComponent<SlotPersonagem>();
            slots.Add(slotDisponivel);
        }

        //reutiliza o slot
        slotDisponivel.gameObject.SetActive(true);
        slotDisponivel.ReceberDadosPersonagem(sistemaDeCriacaoDePersonagens.personagemEmCriacao);
        slotDisponivel.transform.SetSiblingIndex(painelSlotsPersonagens.childCount - 1); //antes do botão de criar personagens

        botaoCriar.transform.SetAsLastSibling();
        ConfigurarBotaoNovoPersonagem();
    }

    public void AtualizarSlots() //função que atualiza os slots
    {
        int totalPersonagens = sistemaDeCriacaoDePersonagens.personagensCriados.Count; //total de personagens criados

        //garante que a lista de slots tenha a mesma quantidade que a de personagens
        for (int i = 0; i < totalPersonagens; i++)
        {
            //atualiza ou adiciona slots a depender do número de personagens criados
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

        //desativa slots caso algum personagem tenha sido excluído
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

        //coloca o botão criar personagem ao final da lista
        botaoCriar.transform.SetAsLastSibling();
    }
}
