using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDeUI : MonoBehaviour
{
    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens;

    public GameObject painelHabilidadesAtivas;
    public GameObject painelHabilidadesPassivas;
    public List<SlotHabilidadeUI> slotsHabilidadesAtivas;
    public List<SlotHabilidadeUI> slotsHabilidadesPassivas;

    public GameObject painelEquipamentos;
    public List<SlotEquipamentoUI> slotsEquipamentos;

    public void MostrarHabilidadesAtiva(string tipo) //fun��o para mostrar as habilidades ativas do personagem
    {
        List<HabilidadeAtiva> listaHabilidadesAtivas = new List<HabilidadeAtiva>(); //reseta a lista

        if (tipo == "Classe")
        {
            listaHabilidadesAtivas = sistemaDeCriacaoDePersonagens.personagemEmCriacao.listaDeHabilidadesAtivasDeClasse; //recebe a lista de habilidades ativas de classe do personagem
        }
        else if(tipo == "Arma")
        {
            listaHabilidadesAtivas = sistemaDeCriacaoDePersonagens.personagemEmCriacao.listaDeHabilidadesAtivasDeArma; //recebe a lista de habilidades ativas de arma do personagem
        }
        painelHabilidadesAtivas.SetActive(true);

        foreach(var slot in slotsHabilidadesAtivas)
        {
            slot.gameObject.SetActive(false);
        }

        for(int i = 0; i < listaHabilidadesAtivas.Count && i < slotsHabilidadesAtivas.Count; i++)
        {
            var hab = listaHabilidadesAtivas[i];

            slotsHabilidadesAtivas[i].AtualizarSlotHabilidadeAtiva(listaHabilidadesAtivas[i]);
            slotsHabilidadesAtivas[i].gameObject.SetActive(true);
        }
    }

    public void MostrarHabilidadesPassiva(string tipo) //fun��o para mostrar as habilidades passivas do personagem
    {
        List<HabilidadePassiva> listaHabilidadesPassivas = new List<HabilidadePassiva>(); //reseta a lista

        if (tipo == "Classe")
        {
            listaHabilidadesPassivas = sistemaDeCriacaoDePersonagens.personagemEmCriacao.listaDeHabilidadesPassivasDeClasse; //recebe a lista de habilidades passivas de classe do personagem
        }
        else if (tipo == "Arma")
        {
            listaHabilidadesPassivas = sistemaDeCriacaoDePersonagens.personagemEmCriacao.listaDeHabilidadesPassivasDeArma; //recebe a lista de habilidades passivas de arma do personagem
        }
        painelHabilidadesPassivas.SetActive(true);

        foreach (var slot in slotsHabilidadesPassivas)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = 0; i < listaHabilidadesPassivas.Count && i < slotsHabilidadesPassivas.Count; i++)
        {
            slotsHabilidadesPassivas[i].AtualizarSlotHabilidadePassiva(listaHabilidadesPassivas[i]);
            slotsHabilidadesPassivas[i].gameObject.SetActive(true);
        }
    }

    public void ChecarEquipamento(int indice) //fun��o que define se deve desequipar um equipamento ou exibir a tela de equipamentos a depender do equipamento selecionado
    {
        switch(indice)
        {
            case 1:
                if(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoCabecaAcessorio != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoCabecaAcessorio);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 2:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoCabecaTopo != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoCabecaTopo);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 3:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoCabecaMedio != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoCabecaMedio);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 4:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoCabecaBaixo != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoCabecaBaixo);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 5:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoArmadura != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoArmadura);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 6:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoBracadeira != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoBracadeira);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 7:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoMaoEsquerda != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoMaoEsquerda);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 8:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoMaoDireita != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoMaoDireita);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 9:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoBota != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoBota);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 10:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoAcessorio1 != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoAcessorio1);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 11:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoAcessorio2 != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoAcessorio2);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
            case 12:
                if (sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoBuffConsumivel != null)
                {
                    sistemaDeCriacaoDePersonagens.DefinirEquipamento(sistemaDeCriacaoDePersonagens.personagemEmCriacao.equipamentoBuffConsumivel);
                }
                else
                {
                    MostrarEquipamentos(indice);
                }
                break;
        }
    }

    public void MostrarEquipamentos(int indice) //fun��o para mostrar os equipamentos
    {
        List<EquipamentoBase> lista = new List<EquipamentoBase>(); //reseta a lista

        switch (indice)
        {
            case 1:
                lista = GerenciadorDeInventario.instancia.equipamentosCabecaAcessorio;
                break;
            case 2:
                lista = GerenciadorDeInventario.instancia.equipamentosCabecaTopo;
                break;
            case 3:
                lista = GerenciadorDeInventario.instancia.equipamentosCabecaMedio;
                break;
            case 4:
                lista = GerenciadorDeInventario.instancia.equipamentosCabecaBaixo;
                break;
            case 5:
                lista = GerenciadorDeInventario.instancia.equipamentosArmadura;
                break;
            case 6:
                lista = GerenciadorDeInventario.instancia.equipamentosBracadeira;
                break;
            case 7:
                lista = GerenciadorDeInventario.instancia.equipamentosMaoEsquerda;
                break;
            case 8:
                lista = GerenciadorDeInventario.instancia.equipamentosMaoDireita;
                break;
            case 9:
                lista = GerenciadorDeInventario.instancia.equipamentosBota;
                break;
            case 10:
                lista = GerenciadorDeInventario.instancia.equipamentosAcessorio1;
                break;
            case 11:
                lista = GerenciadorDeInventario.instancia.equipamentosAcessorio2;
                break;
            case 12:
                lista = GerenciadorDeInventario.instancia.equipamentosBuffConsumivel;
                break;
        }

        painelEquipamentos.SetActive(true);

        foreach (var slot in slotsEquipamentos)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = 0; i < lista.Count && i < slotsEquipamentos.Count; i++)
        {
            slotsEquipamentos[i].AtualizarSlot(lista[i]);
            slotsEquipamentos[i].gameObject.SetActive(true);
        }
    }
}
