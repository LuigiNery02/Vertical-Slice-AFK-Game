using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAtaquePersonagem : MonoBehaviour
{
    [HideInInspector]
    public IAPersonagemBase _personagemPai; //personagem que criou este ataque
    private Transform _alvo; //alvo do ataque
    private float _velocidade; //velocidade do ataque
    private TrailRenderer _trailRenderer; //trilha da movimenta��o do hit

    [HideInInspector] 
    public string poolKey; //chave do pool a ser buscado ou 
    [HideInInspector] 
    public GerenciadorDeObjectPool gerenciadorDePool; //gerenciador do pool

    public bool longaDistancia; //vari�vel para verificar se este ataque � de longa distancia
    [HideInInspector]
    public bool rebater;
    [HideInInspector]
    public int numeroDeRebates;
    private int rebatesAtuais;
    [HideInInspector]
    public bool atravessar;
    private Vector3 direcaoContinuaAposAtravessar;

    [HideInInspector]
    public System.Action<IAPersonagemBase> efeitoExtraAoAtingirAlvo;

    [HideInInspector]
    public bool usarSFX; //vari�vel para verificar se deve usar SFX

    private void OnEnable() //quando for ativado
    {
        if (!longaDistancia)
        {
            _personagemPai = transform.GetComponentInParent<IAPersonagemBase>(); //encontra o personagem que criou este ataque
        }
    }

    private void OnDisable() //quando for desativado
    {
        if (rebater)
        {
            rebater = false;
            numeroDeRebates = 0;
            rebatesAtuais = 0;
            if (_personagemPai != null)
            {
                _personagemPai.rebatesRestantesFlechaEstatica = rebatesAtuais;
            }
        }
        
        //desativa a trilha caso for um proj�til
        if (longaDistancia)
        {
            if (_trailRenderer != null)
            {
                _trailRenderer.enabled = false;
            }

            if(_personagemPai != null)
            {
                _personagemPai._hitAtaquePersonagem = null;
            }
        }

        efeitoExtraAoAtingirAlvo = null;
    }

    private void Update()
    {
        //auto desativa caso o personagem que criou este ataque est� morto ou � um porj�til
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

                transform.LookAt(posicaoAlvo);

                //crrige a rota��o X para 90 graus
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

            if(other != _personagemPai && alvo._comportamento != EstadoDoPersonagem.MORTO && alvo.controlador != _personagemPai.controlador)
            {
                //define para o personagem que este ataque colidiu com um personagem
                IAPersonagemBase alvoDoDano = other.GetComponent<IAPersonagemBase>();
                if (CalcularPrecisao(_personagemPai.precisao, alvoDoDano.esquiva)) //calcula a precis�o do hit
                {
                    if (_personagemPai.efeitoPorAtaqueAtivado)
                    {
                        _personagemPai.ExecutarEfeitosDeAtaque(true);
                    }

                    if (alvoDoDano.efeitoPorAtaqueRecebidoAtivado)
                    {
                        alvoDoDano.ExecutarEfeitosDeAtaqueRecebidos(true);
                    }

                    //verifica o tipo de dano que causar� (f�sico ou m�gico)
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

                    if (efeitoExtraAoAtingirAlvo != null)
                    {
                        efeitoExtraAoAtingirAlvo.Invoke(alvoDoDano);
                    }

                    if (_personagemPai.personagem.statusEspecial == StatusEspecial.Willpower)
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

                if (rebater)
                {
                    if(rebatesAtuais < numeroDeRebates)
                    {
                        rebatesAtuais++;
                        _personagemPai.rebatesRestantesFlechaEstatica = rebatesAtuais;
                        IAPersonagemBase proximoAlvo = EncontrarOutroAlvo(alvoDoDano);
                        if (proximoAlvo != null)
                        {
                            MoverAteAlvo(proximoAlvo.transform, _personagemPai.personagem.arma.velocidadeDoProjetil);
                        }
                        else
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
                    else
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
                else if (atravessar)
                {
                    Collider colisor = GetComponent<Collider>();
                    if (colisor != null)
                    {
                        colisor.enabled = false;
                    }

                    _trailRenderer = GetComponent<TrailRenderer>();
                    if (_trailRenderer != null)
                    {
                        _trailRenderer.enabled = true;
                    }

                    StartCoroutine(Atravessar());
                }
                else
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
        else
        {
            if (_personagemPai.efeitoPorAtaqueAtivado)
            {
                _personagemPai.ExecutarEfeitosDeAtaque(false);
            }

            if (other.GetComponent<HitAtaquePersonagem>() == null) //verifica se n�o colidiu com outro hit
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

    public void MoverAteAlvo(Transform alvo, float velocidade) //fun��o que recebe vari�veis necess�rias para que o hit se mova
    {
        if(alvo == null)
        {
            return; //n�o se move caso o alvo n�o esteja mais acess�vel ou nulo
        }

        _alvo = alvo;
        _velocidade = velocidade;
        longaDistancia = true;

        Vector3 direcao = (_alvo.position - transform.position);
        direcao.y = 0f;
        direcaoContinuaAposAtravessar = direcao.normalized;

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

    private bool CalcularPrecisao(float precisao, float esquiva) //fun��o que calcula a precis�o do ataque
    {
        float chance = (100 - (precisao - esquiva));
        int rng = Random.Range(0, 100);

        return rng < chance;
    }

    private IAPersonagemBase EncontrarOutroAlvo(IAPersonagemBase excluido)
    {
        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        float menorDistancia = float.MaxValue;
        IAPersonagemBase alvoMaisProximo = null;

        foreach (var possivelAlvo in personagens)
        {
            if (possivelAlvo == excluido || possivelAlvo == _personagemPai || possivelAlvo._comportamento == EstadoDoPersonagem.MORTO || possivelAlvo.controlador == _personagemPai.controlador)
            {
                continue;
            }

            float dist = Vector3.Distance(possivelAlvo.transform.position, excluido.transform.position);
            if (dist < menorDistancia && dist <= 20f)
            {
                menorDistancia = dist;
                alvoMaisProximo = possivelAlvo;
            }
        }

        return alvoMaisProximo;
    }

    IEnumerator Atravessar()
    {
        float tempo = 0f;
        float duracao = 2f; 
        float velocidade = _velocidade;

        Vector3 direcao = direcaoContinuaAposAtravessar;

        yield return new WaitForSeconds(0.05f);
        Collider colisor = GetComponent<Collider>();
        if (colisor != null)
        {
            colisor.enabled = true;
        }

        while (tempo < duracao)
        {
            transform.position += direcao * velocidade * Time.deltaTime;
            tempo += Time.deltaTime;
            yield return null;
        }

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
