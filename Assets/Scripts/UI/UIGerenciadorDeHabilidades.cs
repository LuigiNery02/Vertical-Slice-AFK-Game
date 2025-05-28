using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGerenciadorDeHabilidades : MonoBehaviour
{
    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens;

    public GameObject painelHabilidades;
    public List<SlotHabilidadeUI> slotsHabilidade;

    public void MostrarHabilidades() //função para mostrar as habilidades do personagem
    {
        List<HabilidadeBase> lista = new List<HabilidadeBase>(); //reseta a lista

        lista = sistemaDeCriacaoDePersonagens.personagemEmCriacao.listaDeHabilidadesDeClasse; //recebe a lista de habilidades do personagem

        painelHabilidades.SetActive(true);

        foreach(var slot in slotsHabilidade)
        {
            slot.gameObject.SetActive(false);
        }

        for(int i = 0; i < lista.Count && i < slotsHabilidade.Count; i++)
        {
            slotsHabilidade[i].AtualizarSlot(lista[i]);
            slotsHabilidade[i].gameObject.SetActive(true);
        }
    }
}
