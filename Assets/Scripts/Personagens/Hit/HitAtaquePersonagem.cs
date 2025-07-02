using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class HitAtaquePersonagem : MonoBehaviour
{
    private IAPersonagemBase _personagemPai; //personagem que criou este ataque
    private Transform _alvo; //alvo do ataque
    private float _velocidade; //velocidade do ataque
    [SerializeField]
    private Vector3 _posicaoInicial; //posi��o inicial do objeto
    private TrailRenderer _trailRenderer; //trilha da movimenta��o do hit

    [HideInInspector]
    public bool longaDistancia; //vari�vel para verificar se este ataque � de longa distancia
    [HideInInspector]
    public bool usarSFX; //vari�vel para verificar se deve usar SFX

    private void OnEnable() //quando for ativado
    {
        _personagemPai = transform.GetComponentInParent<IAPersonagemBase>(); //encontra o personagem que criou este ataque
    }

    private void OnDisable() //quando for desativado
    {
        //desativa a trilha caso for um proj�til
        if (longaDistancia)
        {
            if (_trailRenderer != null)
            {
                _trailRenderer.enabled = false;
            }
        }
    }

    private void Update()
    {
        //se desativa caso o personagem que criou este ataque est� morto
        if(_personagemPai._comportamento != EstadoDoPersonagem.ATACANDO && _personagemPai._comportamento != EstadoDoPersonagem.MOVIMENTO_ESPECIAL)
        {
            gameObject.SetActive(false);
        }
        //se for um hit de longa distancia, se move at� o alvo
        if(longaDistancia)
        {
            if (_alvo != null)
            {
                //move o ataque com base na posi��o do alvo
                Vector3 posicaoAtual = transform.position;
                Vector3 posicaoAlvo = _alvo.position;

                //mant�m o mesmo y da posi��o atual
                posicaoAlvo.y = posicaoAtual.y;

                //move apenas em X e Z (altura fica fixa)
                transform.position = Vector3.MoveTowards(posicaoAtual, posicaoAlvo, _velocidade * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other) //quando colidir com um objeto
    {
        if(other.GetComponent<IAPersonagemBase>() != null) //se colidiu com um personagem
        {
            IAPersonagemBase alvo = other.GetComponent<IAPersonagemBase>(); //define o personagem colidido como alvo

            if(other != _personagemPai && alvo._comportamento != EstadoDoPersonagem.MORTO && alvo == _personagemPai._personagemAlvo)
            {
                //define para o personagem que este ataque colidiu com um personagem
                IAPersonagemBase alvoDoDAno = other.GetComponent<IAPersonagemBase>();
                if (CalcularPrecisao(_personagemPai.precisao, alvoDoDAno.esquiva))
                {
                    switch (_personagemPai.personagem.arma.armaDano)
                    {
                        case TipoDeDano.DANO_MELEE:
                            _personagemPai.CausarDano(alvoDoDAno, 0);
                            break;
                        case TipoDeDano.DANO_RANGED:
                            _personagemPai.CausarDano(alvoDoDAno, 0);
                            break;
                        case TipoDeDano.DANO_MAGICO:
                            _personagemPai.CausarDano(alvoDoDAno, 1);
                            break;
                    }

                    if (_personagemPai.efeitoPorAtaqueAtivado)
                    {
                        _personagemPai.efeitoPorAtaque();
                    }
                }
                else
                {
                    alvoDoDAno.Esquivar();

                    if (alvoDoDAno.efeitoPorEsquivaAtivado)
                    {
                        alvoDoDAno.efeitoPorEsquiva();
                    }
                }
                gameObject.SetActive(false);
            }
        }
        else
        {
            if(other.GetComponent<HitAtaquePersonagem>() == null)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void MoverAteAlvo(Transform alvo, float velocidade) //fun��o que recebe vari�veis necess�rias para que o hit se mova
    {
        if(alvo == null)
        {
            return;
        }

        _alvo = alvo;
        _velocidade = velocidade;
        longaDistancia = true;

        //ativa a trilha caso for um proj�til
        if (longaDistancia)
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            if(_trailRenderer != null)
            {
                _trailRenderer.enabled = true;
            }
        }
    }

    public void ResetarPosi��o() //reseta a posi��o do hit
    {
        transform.localPosition = _posicaoInicial;
    }

    private bool CalcularPrecisao(float precisao, float esquiva) //fun��o que calcula a precis�o do ataque
    {
        float chance = (100 - (precisao - esquiva));
        int rng = Random.Range(0, 100);

        return rng < chance;
    }
}
