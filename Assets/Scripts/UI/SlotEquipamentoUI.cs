using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotEquipamentoUI : MonoBehaviour
{
    public Text nomeTexto; //texto do nome do equipamento
    public Text efeitoTexto; //texto do efeito do equipamento
    public Image imagem; //imagem do equipamento

    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens; //sistema de cria��o de personagens

    private EquipamentoBase _equipamento;

    public void AtualizarSlot(EquipamentoBase equipamento) //fun��o que atualiza o slot de equipamentos
    {
        _equipamento = equipamento;

        nomeTexto.text = "";
        efeitoTexto.text = "";

        nomeTexto.text = equipamento.nome;
        efeitoTexto.text = equipamento.descricaoCurta;
        imagem.sprite = equipamento.icone;
    }

    public void DefinirEquipamentoSistemaDeCriacao()
    {
        sistemaDeCriacaoDePersonagens.DefinirEquipamento(_equipamento);
    }
}
