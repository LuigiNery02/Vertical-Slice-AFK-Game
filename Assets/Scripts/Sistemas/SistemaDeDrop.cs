using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

sealed class SistemaDeDrop : MonoBehaviour
{
    //área referente ao texto visual dos drops
    [Header("Texto")]
    [SerializeField]
    private Text _textoDosDrops; //texto visual dos drops

    private int _drops; //valor dos drops do player

    public void Receberdrops(int drops) //função para receber drops
    {
        _drops += drops;
        AtualizarDrops();
    }
    private void AtualizarDrops() //função para atualizar visualmente os drops
    {
        _textoDosDrops.text = _drops.ToString();
    }
}
