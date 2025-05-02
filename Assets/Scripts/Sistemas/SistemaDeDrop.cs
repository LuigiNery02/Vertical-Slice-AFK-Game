using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDeDrop : MonoBehaviour
{
    //área referente ao texto visual dos drops
    [Header("Texto")]
    [SerializeField]
    private Text _textoDosDrops; //texto visual dos drops

    //área referente aos drops visuais
    [Header("Drops")]
    [SerializeField]
    private Transform _dropPai; //pai que guarda os drops dentro de si
    [SerializeField]
    private GameObject[] _dropsVisuais; //drops

    //área referente aos sfx
    [Header("SFX")]
    [SerializeField]
    private AudioSource _sfx; //sfx do drop

    private int _drops; //valor dos drops do player
    [SerializeField]
    private int _dropsAtivos; //variável que verifica o número de drops visuais ativos

    public void Receberdrops(int drops) //função para receber drops
    {
        _drops += drops;
        AtualizarDrops();
    }
    private void AtualizarDrops() //função para atualizar visualmente os drops
    {
        _textoDosDrops.text = _drops.ToString();
    }

    public void Dropar(Transform inimigo) //função que faz o inimigo dropar
    {
        _dropsAtivos++;
        _dropsVisuais[_dropsAtivos - 1].gameObject.SetActive(true);
        _dropsVisuais[_dropsAtivos - 1].transform.parent = inimigo;
        _dropsVisuais[_dropsAtivos - 1].transform.localPosition = new Vector3(0, 2, 0);
        if (SistemaDeBatalha.usarSfxs)
        {
            _sfx.Play();
        }
    }

    public void ResetarDrop() //função que reseta o drop
    {
        _dropsVisuais[_dropsAtivos - 1].transform.parent = _dropPai;
        _dropsVisuais[_dropsAtivos - 1].transform.localPosition = Vector3.zero;
        _dropsVisuais[_dropsAtivos - 1].gameObject.SetActive(false);
        _dropsAtivos--;
    }
}
