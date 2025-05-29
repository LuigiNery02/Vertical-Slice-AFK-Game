using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGerenciadorDeHabilidades : MonoBehaviour
{
    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens;

    public GameObject painelHabilidades;
    public List<SlotHabilidadeUI> slotsHabilidade;

    public void MostrarHabilidades(string tipo) //fun��o para mostrar as habilidades do personagem
    {
        List<HabilidadeBase> lista = new List<HabilidadeBase>(); //reseta a lista

        if (tipo == "Classe")
        {
            lista = sistemaDeCriacaoDePersonagens.personagemEmCriacao.listaDeHabilidadesDeClasse; //recebe a lista de habilidades de classe do personagem
        }
        else if(tipo == "Arma")
        {
            lista = sistemaDeCriacaoDePersonagens.personagemEmCriacao.listaDeHabilidadesDeArma; //recebe a lista de habilidades de arma do personagem
        }
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
