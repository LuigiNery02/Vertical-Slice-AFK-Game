using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotHabilidadeUI : MonoBehaviour
{
    public Text nomeTexto; //texto do nome da habilidade
    public Text nivelTexto; //texto do nível da habilidade
    public Image imagem; //imagem da habilidade

    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens; //sistema de criação de personagens

    private HabilidadeBase _habilidade;

    public void AtualizarSlot(HabilidadeBase habilidade) //função que atualiza o slot de habilidades
    {
        _habilidade = habilidade;

        nomeTexto.text = "";
        nivelTexto.text = "";

        nomeTexto.text = habilidade.nome;
        nivelTexto.text += ("Nv: " + habilidade.nivel);
        imagem.sprite = habilidade.spriteHabilidade;
    }

    public void DefinirHabilidadeSistemaDeCriacao()
    {
        sistemaDeCriacaoDePersonagens.DefinirHabilidade(_habilidade);
    }
}
