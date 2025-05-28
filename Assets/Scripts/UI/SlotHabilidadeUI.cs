using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotHabilidadeUI : MonoBehaviour
{
    public Text nomeTexto;
    public Text nivelTexto;
    public Image imagem;
    public SistemaDeCriacaoDePersonagens sistemaDeCriacaoDePersonagens;

    private HabilidadeBase _habilidade;

    public void AtualizarSlot(HabilidadeBase habilidade)
    {
        _habilidade = habilidade;

        nomeTexto.text = "";
        nivelTexto.text = "";

        nomeTexto.text = habilidade.nome;
        nivelTexto.text += (" " + habilidade.nivel);
        imagem.sprite = habilidade.spriteHabilidade;
    }

    public void DefinirHabilidadeSistemaDeCriacao()
    {
        sistemaDeCriacaoDePersonagens.DefinirHabilidade(_habilidade);
    }
}
