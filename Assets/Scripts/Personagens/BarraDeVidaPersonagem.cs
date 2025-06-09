using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class BarraDeVidaPersonagem : MonoBehaviour
{
    private Quaternion _rotacaoInicial; //rotação inicial do slider
    private Camera _camera; //camera do jogo
    void Start()
    {
        _camera = Camera.main; //encontra a camera na cena
        _rotacaoInicial = transform.rotation; //guarda a rotação inicial
    }
    private void LateUpdate()
    {
        if (_camera == null)
        {
            return;
        }
        transform.rotation = _rotacaoInicial; //faz com que o slider sempre olhe para a câmera
    }
}
