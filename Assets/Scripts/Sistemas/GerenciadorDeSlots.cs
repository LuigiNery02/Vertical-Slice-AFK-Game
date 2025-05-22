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

    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens; //sistema de cria��o de personagens
    private GameObject botaoCriarAtual; //bot�o atual de criar personagem

    private void Start()
    {
        AdicionarBotaoNovoPersonagem();
    }

    public void AdicionarBotaoNovoPersonagem() //fun��o que adiciona o bot�o de criar novo personagem
    {
        botaoCriarAtual = Instantiate(botaoCriar, painelSlotsPersonagens); //instancia o bot�o de criar personagem
        botaoCriarAtual.transform.localScale = Vector3.one;
        botaoCriarAtual.GetComponent<Button>().onClick.AddListener(() =>
        {
            sistemaDeCriacaoDePersonagens.CriarPersonagem();
        });
    }

    public void SubstituirBotaoPorSlot() //fun��o que cria o slot do personagem
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
