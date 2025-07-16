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

    private HabilidadeAtiva _habilidadeAtiva;
    private HabilidadePassiva _habilidadePassiva;

    public void AtualizarSlotHabilidadeAtiva(HabilidadeAtiva habilidade)
    {
        _habilidadeAtiva = habilidade;
        nomeTexto.text = habilidade.nome;
        nivelTexto.text = ("Nv: " + habilidade.nivel);
        imagem.sprite = habilidade.spriteHabilidade;

        Button botaoDefinir = GetComponent<Button>();
        botaoDefinir.onClick.RemoveAllListeners();

        HabilidadeAtiva habilidadeCapturada = _habilidadeAtiva;

        botaoDefinir.onClick.AddListener(() =>
        {
            sistemaDeCriacaoDePersonagens.DefinirHabilidadeAtiva(habilidadeCapturada);
        });
    }


    public void AtualizarSlotHabilidadePassiva(HabilidadePassiva habilidade) //função que atualiza o slot de habilidades passivas
    {
        _habilidadePassiva = habilidade;
        nomeTexto.text = habilidade.nome;
        nivelTexto.text = ("Nv: " + habilidade.nivel);
        imagem.sprite = habilidade.spriteHabilidade;

        Button botaoDefinir = GetComponent<Button>();
        botaoDefinir.onClick.RemoveAllListeners();

        HabilidadePassiva habilidadeCapturada = _habilidadePassiva;

        botaoDefinir.onClick.AddListener(() =>
        {
            sistemaDeCriacaoDePersonagens.DefinirHabilidadePassiva(habilidadeCapturada);
        });
    }
}
