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

    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens; //sistema de criação de personagens
    private GameObject botaoCriarAtual; //botão atual de criar personagem

    private void Start()
    {
        AdicionarBotaoNovoPersonagem();
    }

    public void AdicionarBotaoNovoPersonagem() //função que adiciona o botão de criar novo personagem
    {
        botaoCriarAtual = Instantiate(botaoCriar, painelSlotsPersonagens); //instancia o botão de criar personagem
        botaoCriarAtual.transform.localScale = Vector3.one;
        botaoCriarAtual.GetComponent<Button>().onClick.AddListener(() =>
        {
            sistemaDeCriacaoDePersonagens.CriarPersonagem();
        });
    }

    public void SubstituirBotaoPorSlot() //função que cria o slot do personagem
    {
        Destroy(botaoCriarAtual);
        GameObject novoSlot = Instantiate(slotPersonagens, painelSlotsPersonagens);
        novoSlot.transform.localScale = Vector3.one;
        novoSlot.GetComponent<SlotPersonagem>().ReceberDadosPersonagem(sistemaDeCriacaoDePersonagens.personagemEmCriacao);
        botaoCriar.transform.SetAsLastSibling();
        AdicionarBotaoNovoPersonagem();
    }

    public void AtualizarSlots()
    {
        foreach(Transform filho in painelSlotsPersonagens)
        {
            if(filho.gameObject != botaoCriarAtual)
            {
                Destroy(filho.gameObject);
            }
        }

        for (int i = 0; i < sistemaDeCriacaoDePersonagens.personagensCriados.Count; i++)
        {
            GameObject novoSlot = Instantiate(slotPersonagens, painelSlotsPersonagens);
            novoSlot.transform.localScale = Vector3.one;

            var slot = novoSlot.GetComponent<SlotPersonagem>();
            slot.personagemIndice = i;
            slot.ReceberDadosPersonagem(sistemaDeCriacaoDePersonagens.personagensCriados[i]);
        }

        botaoCriarAtual.transform.SetAsLastSibling();
    }

}
