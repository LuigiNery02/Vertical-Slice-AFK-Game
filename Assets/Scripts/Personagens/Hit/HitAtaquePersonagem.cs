using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class HitAtaquePersonagem : MonoBehaviour
{
    [HideInInspector]
    public IAPersonagemBase _personagemPai; //personagem que criou este ataque
    private Transform _alvo; //alvo do ataque
    private float _velocidade; //velocidade do ataque
    private TrailRenderer _trailRenderer; //trilha da movimentação do hit

    [HideInInspector] 
    public string poolKey; //chave do pool a ser buscado ou 
    [HideInInspector] 
    public GerenciadorDeObjectPool gerenciadorDePool; //gerenciador do pool

    public bool longaDistancia; //variável para verificar se este ataque é de longa distancia
    [HideInInspector]
    public bool usarSFX; //variável para verificar se deve usar SFX

    private void OnEnable() //quando for ativado
    {
        if (!longaDistancia)
        {
            _personagemPai = transform.GetComponentInParent<IAPersonagemBase>(); //encontra o personagem que criou este ataque
        }
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
        //auto desativa caso o personagem que criou este ataque está morto ou é um porjétil
        if(_personagemPai != null && _personagemPai._comportamento != EstadoDoPersonagem.ATACANDO && _personagemPai._comportamento != EstadoDoPersonagem.MOVIMENTO_ESPECIAL && longaDistancia)
        {
            //desativa ou devolve para o pool

            if (gerenciadorDePool != null)
            {
                gerenciadorDePool.DevolverPool(poolKey, gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            } 
        }
        else if(_personagemPai == null)
        {
            if (gerenciadorDePool != null)
            {
                gerenciadorDePool.DevolverPool(poolKey, gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
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

                transform.LookAt(posicaoAlvo);

                //crrige a rotação X para 90 graus
                Vector3 rotacaoCorrigida = transform.eulerAngles;
                rotacaoCorrigida.x = 90f;
                transform.eulerAngles = rotacaoCorrigida;

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
                IAPersonagemBase alvoDoDano = other.GetComponent<IAPersonagemBase>();
                if (CalcularPrecisao(_personagemPai.precisao, alvoDoDano.esquiva)) //calcula a precisão do hit
                {
                    if (_personagemPai.efeitoPorAtaqueAtivado)
                    {
                        _personagemPai.ExecutarEfeitosDeAtaque(true);
                    }

                    if (alvoDoDano.efeitoPorAtaqueRecebidoAtivado)
                    {
                        alvoDoDano.ExecutarEfeitosDeAtaqueRecebidos(true);
                    }

                    //verifica o tipo de dano que causará (físico ou mágico)
                    switch (_personagemPai.personagem.arma.armaDano)
                    {
                        case TipoDeDano.DANO_MELEE:
                            _personagemPai.CausarDano(alvoDoDano, 0);
                            break;
                        case TipoDeDano.DANO_RANGED:
                            _personagemPai.CausarDano(alvoDoDano, 0);
                            break;
                        case TipoDeDano.DANO_MAGICO:
                            _personagemPai.CausarDano(alvoDoDano, 1);
                            break;
                    }

                    if(_personagemPai.personagem.statusEspecial == StatusEspecial.Willpower)
                    {
                        _personagemPai.AtualizarWillPower(1, true);
                    }

                    if(alvoDoDano.personagem.statusEspecial == StatusEspecial.Willpower)
                    {
                        alvoDoDano.AtualizarWillPower(1, true);
                    }
                }
                else
                {
                    if (_personagemPai.efeitoPorAtaqueAtivado)
                    {
                        _personagemPai.ExecutarEfeitosDeAtaque(false);
                    }

                    if (alvoDoDano.efeitoPorAtaqueRecebidoAtivado)
                    {
                        alvoDoDano.ExecutarEfeitosDeAtaqueRecebidos(false);
                    }

                    alvoDoDano.Esquivar(); //faz o alvo esquivar

                    //if (alvoDoDAno.efeitoPorEsquivaAtivado)
                    //{
                    //    alvoDoDAno.efeitoPorEsquiva();
                    //}
                }

                //verifica se volta para o pool ou se auto desativa

                if (gerenciadorDePool != null && longaDistancia)
                {
                    gerenciadorDePool.DevolverPool(poolKey, gameObject);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (_personagemPai.efeitoPorAtaqueAtivado)
            {
                _personagemPai.ExecutarEfeitosDeAtaque(false);
            }

            if (other.GetComponent<HitAtaquePersonagem>() == null) //verifica se não colidiu com outro hit
            {
                //verifica se volta para o pool ou se auto desativa

                if (gerenciadorDePool != null && longaDistancia)
                {
                    gerenciadorDePool.DevolverPool(poolKey, gameObject);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void MoverAteAlvo(Transform alvo, float velocidade) //função que recebe variáveis necessárias para que o hit se mova
    {
        if(alvo == null)
        {
            return; //não se move caso o alvo não esteja mais acessível ou nulo
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

    private bool CalcularPrecisao(float precisao, float esquiva) //função que calcula a precisão do ataque
    {
        float chance = (100 - (precisao - esquiva));
        int rng = Random.Range(0, 100);

        return rng < chance;
    }
}
