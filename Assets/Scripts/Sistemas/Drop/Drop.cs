using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField]
    private int _valor; //valor do drop
    [SerializeField]
    private float _tempoParaIrAoInventario; //tempo para o objeto ir para o inventário a partir do momento que for ativado
    [SerializeField]
    private float _velocidade; //velocidade em que se move

    private Vector3 posicaoCanvas; //posição do "mundo" do canvas
    private Transform posicaoCanvasInventario; //inventário
    private bool podeSeMover; //verifica se pode se mover

    private SistemaDeDrop _sistemaDeDrop;
    private void OnEnable()
    {
        _sistemaDeDrop = FindObjectOfType<SistemaDeDrop>();
        posicaoCanvasInventario = GameObject.Find("BotãoInventário").transform;
        FunçõesIniciais();
    }

    private void Update()
    {
        if (podeSeMover)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicaoCanvas, _velocidade * Time.deltaTime); //move o drop
            if(transform.position == posicaoCanvas)
            {
                podeSeMover = false;
                _sistemaDeDrop.ResetarDrop();
            }
        }
    }

    private void FunçõesIniciais() //funções iniciais do drop
    {
        _sistemaDeDrop.Receberdrops(_valor);
        posicaoCanvas = Camera.main.ScreenToWorldPoint(new Vector3(posicaoCanvasInventario.position.x, posicaoCanvasInventario.position.y,Camera.main.nearClipPlane + 1f));
        StartCoroutine(TempoParaSeMover());
    }

    IEnumerator TempoParaSeMover() //função que espera em segundos o tempo para o objeto se mover para seu destino
    {
        yield return new WaitForSeconds(_tempoParaIrAoInventario);
        podeSeMover = true;
    }
}
