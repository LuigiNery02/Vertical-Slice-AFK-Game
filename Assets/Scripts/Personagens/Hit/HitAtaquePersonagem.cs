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
    private TrailRenderer _trailRenderer; //trilha da movimentação do hit

    [HideInInspector]
    public bool longaDistancia; //variável para verificar se este ataque é de longa distancia
    [HideInInspector]
    public bool usarSFX; //variável para verificar se deve usar SFX

    private void OnEnable() //quando for ativado
    {
        _personagemPai = transform.GetComponentInParent<IAPersonagemBase>(); //encontra o personagem que criou este ataque
    }

    private void OnDisable() //quando for desativado
    {
        //desativa a trilha caso for um projétil
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
        //se desativa caso o personagem que criou este ataque está morto
        if(_personagemPai._comportamento != EstadoDoPersonagem.ATACANDO && _personagemPai._comportamento != EstadoDoPersonagem.MOVIMENTO_ESPECIAL)
        {
            gameObject.SetActive(false);
        }
        //se for um hit de longa distancia, se move até o alvo
        if(longaDistancia)
        {
            if (_alvo != null)
            {
                //move o ataque com base na posição do alvo
                Vector3 posicaoAtual = transform.position;
                Vector3 posicaoAlvo = _alvo.position;

                //mantém o mesmo y da posição atual
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

            //verifica se o persomagem que criou este ataque está lançando um ataque especial
            if(other != _personagemPai && _personagemPai._comportamento == EstadoDoPersonagem.MOVIMENTO_ESPECIAL)
            {
                IAPersonagemBase alvoDoDAno = other.GetComponent<IAPersonagemBase>();
                if(CalcularPrecisao(_personagemPai.personagem.precisao, alvoDoDAno.personagem.esquiva))
                {
                    switch (_personagemPai.personagem.classe)
                    {
                        case Classe.Guerreiro:
                            _personagemPai.CausarDano(alvoDoDAno, 0);
                            break;
                        case Classe.Arqueiro:
                            _personagemPai.CausarDano(alvoDoDAno, 1);
                            break;
                        case Classe.Mago:
                            _personagemPai.CausarDano(alvoDoDAno, 2);
                            break;
                    }
                }
                else
                {
                    alvoDoDAno.Esquivar();
                }
           
                gameObject.SetActive(false);
            }
            //checa se o alvo não é o personagem que criou este ataque, se o alvo não está morto e se o alvo é o atual alvo do personagem que criou este ataque
            else if(other != _personagemPai && alvo._comportamento != EstadoDoPersonagem.MORTO && alvo == _personagemPai._personagemAlvo)
            {
                //define para o personagem que este ataque colidiu com um personagem
                IAPersonagemBase alvoDoDAno = other.GetComponent<IAPersonagemBase>();
                if (CalcularPrecisao(_personagemPai.personagem.precisao, alvoDoDAno.personagem.esquiva))
                {
                    switch (_personagemPai.personagem.classe)
                    {
                        case Classe.Guerreiro:
                            _personagemPai.CausarDano(alvoDoDAno, 0);
                            break;
                        case Classe.Arqueiro:
                            _personagemPai.CausarDano(alvoDoDAno, 1);
                            break;
                        case Classe.Mago:
                            _personagemPai.CausarDano(alvoDoDAno, 2);
                            break;
                    }
                }
                else
                {
                    alvoDoDAno.Esquivar();
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

    public void MoverAteAlvo(Transform alvo, float velocidade) //função que recebe variáveis necessárias para que o hit se mova
    {
        if(alvo == null)
        {
            return;
        }

        _alvo = alvo;
        _velocidade = velocidade;
        longaDistancia = true;

        //ativa a trilha caso for um projétil
        if (longaDistancia)
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            if(_trailRenderer != null)
            {
                _trailRenderer.enabled = true;
            }
        }
    }

    public void ResetarPosição() //reseta a posição do hit
    {
        transform.localPosition = _posicaoInicial;
    }

    private bool CalcularPrecisao(int precisao, int esquiva) //função que calcula a precisão do ataque
    {
        int chance = 75 + (precisao - esquiva);
        chance = Mathf.Clamp(chance, 5, 95);
        int rng = Random.Range(0, 100);

        return rng < chance;
    }
}
