using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGerenciadorDeHabilidades : MonoBehaviour
{
    public GerenciadorDeInventario inventario;
    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens;

    public GameObject painelHabilidades;
    public List<SlotHabilidadeUI> slotsHabilidade;

    public void MostrarHabilidades()
    {
        List<HabilidadeBase> lista = new List<HabilidadeBase>();

        if(sistemaDeCriacaoDePersonagens.personagemEmCriacao.classe == Classe.Guerreiro)
        {
            lista = inventario.habilidadesClasseGuerreiro;
        }
        else if(sistemaDeCriacaoDePersonagens.personagemEmCriacao.classe == Classe.Arqueiro)
        {
            lista = inventario.habilidadesClasseArqueiro;
        }
        else if(sistemaDeCriacaoDePersonagens.personagemEmCriacao.classe == Classe.Mago)
        {
            lista = inventario.habilidadesClasseMago;
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
