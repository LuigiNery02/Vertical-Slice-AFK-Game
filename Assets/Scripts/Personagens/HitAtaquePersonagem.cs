using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class HitAtaquePersonagem : MonoBehaviour
{
    private IAPersonagemBase _personagemPai; //personagem que criou este ataque
    private Transform _alvo; //alvo do ataque
    private float _velocidade; //velocidade do ataque
    [SerializeField]
    private Vector3 _posicaoInicial; //posição inicial do objeto

    [HideInInspector]
    public bool longaDistancia; //variável para verificar se este ataque é de longa distancia

    private void OnEnable() //quando for ativado
    {
        _personagemPai = transform.GetComponentInParent<IAPersonagemBase>(); //encontra o personagem que criou este ataque
    }

    private void Update()
    {
        //se for um hit de longa distancia, se move até o alvo
        if(longaDistancia)
        {
            transform.position = Vector3.MoveTowards(transform.position, _alvo.position, _velocidade * Time.deltaTime); //move o ataque
        }
    }
    private void OnTriggerEnter(Collider other) //quando colidir com um objeto
    {
        if(other.GetComponent<IAPersonagemBase>() != null && other != _personagemPai) //se o objeto for um personagem
        {
            //define para o personagem que este ataque colidiu com um personagem
            IAPersonagemBase alvoDoDAno = other.GetComponent<IAPersonagemBase>();
            _personagemPai.CausarDano(alvoDoDAno);
            gameObject.SetActive(false);
        }
    }

    public void MoverAteAlvo(Transform alvo, float velocidade) //função que recebe variáveis necessárias para que o hit se mova
    {
        if(alvo == null)
        {
            return;
        }

        _alvo = alvo;
        _velocidade = velocidade;
        longaDistancia = true;
    }

    public void ResetarPosição() //reseta a posição do hit
    {
        transform.localPosition = _posicaoInicial;
    }
}
