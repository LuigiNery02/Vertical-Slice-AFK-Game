using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControladorDoPersonagem { PERSONAGEM_DO_JOGADOR, PERSONAGEM_INIMIGO } //quem controla o personagem, se é controlado pelo jogador ou pela IA inimiga
public enum TipoDePersonagem { CURTA_DISTANCIA, LONGA_DISTANCIA } //características referente ao comportamento de ataque personagem, se é um ataque de curta ou longa distância
public enum EstadoDoPersonagem { IDLE, PERSEGUINDO, ATACANDO, MORTO } //estados de comportamento do personagem

public class IAPersonagemBase : MonoBehaviour
{
    //área referente às definições do personagem
    [Header("Definições")]
    public ControladorDoPersonagem controlador;
    [SerializeField]
    public TipoDePersonagem _tipo;
    public float distanciaMinimaParaAtacar = 5f; //distancia minima que um personagem de longa distancia deve ter para atacar
    public float velocidadeDoProjetil; //velocidade do projetil do ataque do personagem de longa distancia

    //área referente ao hp (vida) do personagem
    [Header("HP")]
    [SerializeField]
    private float _hpMaximoEInicial = 100f; //valor inicial que o hp atual do player terá ao iniciar a batalha, e valor máximo que ele pode ter
    //[HideInInspector]
    public float hpAtual = 100f; //valor atual do hp (vida) do personagem

    //área referente ao movimento do personagem
    [Header("Movimento")]
    [SerializeField]
    private float _velocidade = 2f; //valor do ataque básico do personagem

    //área referente ao ataque do personagem
    [Header("Ataque")]
    [SerializeField]
    private float _danoAtaqueBasico = 10f; //valor do dano do ataque básico do personagem
    [SerializeField]
    private float _cooldown = 1f; //valor do tempo de espera para cada ataque básico do personagem
    private float _cooldownAtual = 0f; //tempo atual para o personagem poder atacar novamente
    private bool _podeAtacar; //variável que verifica se o personagem pode atacar
    
    private EstadoDoPersonagem _comportamento;
    private IAPersonagemBase _personagemAlvo; //alvo de ataques do personagem
    private Transform _alvoAtual; //transform do personagem alvo
    private SistemaDeBatalha _sistemaDeBatalha; //sistema de batalha
    private HitAtaquePersonagem _hitAtaquePersonagem; //hit do personagem

    //Área de feedback visuais
    private Animator _animator; //animator do personagem
    private bool _usarAnimações; //variável para verificar se deve usar as animações
    private bool _usarSliders; //variável para verificar se deve usar os sliders

    public void IniciarBatalha() //função chamada ao inicar a batalha e define os valores e comportamentos iniciais do personagem
    {
        _sistemaDeBatalha = GameObject.FindGameObjectWithTag("SistemaDeBatalha").GetComponent<SistemaDeBatalha>(); //encontra o sistema de batalha na cena
        _hitAtaquePersonagem = transform.GetComponentInChildren<HitAtaquePersonagem>(true); //encontra o hit do personagem dentro de si
        hpAtual = _hpMaximoEInicial; //define o hp atual do personagem igual ao valor máximo e inicial
        SelecionarAlvo(); //chama a função para o personagem encontrar seu alvo

        _usarAnimações = _sistemaDeBatalha.usarAnimações; //verifica se deve usar animações referente o sistema de batalha
        if (_usarAnimações)
        {
            //encontra o animator no objeto
            _animator = GetComponent<Animator>();
            if(_animator == null)
            {
                Debug.Log("Não há animator");
            }
        }
        _usarSliders = _sistemaDeBatalha.usarSliders; //verifica se deve usar sliders referente o sistema de batalha
    }

    private void VerificarComportamento(string comportamento) //função que verifica qual deve ser o comportamento do personagem
    {
        if (comportamento == "perseguir")
        {
            _comportamento = EstadoDoPersonagem.PERSEGUINDO; //personagem persegue
        }
        else if (comportamento == "atacar") 
        {
            _comportamento = EstadoDoPersonagem.ATACANDO; //personagem ataca
        }
        else if (comportamento == "selecionarAlvo")
        {
            _comportamento = EstadoDoPersonagem.IDLE;
            SelecionarAlvo();
        }
    }

    private void Update()
    {
        //checa atualizadamente os estados de comportamento do personagem
        if(_comportamento == EstadoDoPersonagem.PERSEGUINDO)
        {
            Perseguir();
        }
        else if(_comportamento == EstadoDoPersonagem.ATACANDO)
        {
            Atacar();
        }
    }

    #region SeleçãoDeAlvo
    private void SelecionarAlvo() //função que seleciona o alvo de ataques do personagem
    {
        if(_sistemaDeBatalha != null) //caso tenha encontrado o sistema de batalha na cena anteriormente
        {
            //verifica o tipo de comportamento deve ter para selecionar seu alvo
            if(_sistemaDeBatalha.primeiroAlvo == PrimeiroAlvo.ALVO_VISTO) 
            {
                SelecionarAlvoVisto();
            }
            else if(_sistemaDeBatalha.primeiroAlvo == PrimeiroAlvo.ALVO_PROXIMO)
            {
                SelecionarAlvoProximo();
            }
        }
    }

    private void SelecionarAlvoVisto()
    {
        //cria um raycast para verificar se há um inimigo a sua frente
        RaycastHit hit;
        Vector3 origem = transform.position + Vector3.up; //origem do raycast
        Vector3 direcao = transform.forward; //direção do raycast
        float distancia = 100f; //distancia do raycast

        if (Physics.Raycast(origem, direcao, out hit, distancia)) //checa se o raycast colidiu com algo
        {
            //verifica se o raycast colidiu com um personagem
            _personagemAlvo = hit.collider.GetComponent<IAPersonagemBase>();

            //verifica se o personagem visto é seu inimigo
            if (_personagemAlvo != null && _personagemAlvo.controlador != controlador)
            {
                _alvoAtual = _personagemAlvo.transform;
                VerificarComportamento("perseguir"); //personagem deve perseguir
            }
        }
        else
        {
            Debug.Log(gameObject.name + "Detectou:" + "Nada atingido pelo raycast");
        }
    }

    private void SelecionarAlvoProximo()
    {
        IAPersonagemBase alvoProximo = null;
        float menorDistancia = Mathf.Infinity;

        //encontra todos os personagens na cena
        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (var outro in personagens)
        {
            //ignora a si mesmo
            if(outro == this)
            {
                continue;
            }

            //verifica se o outro personagem é seu inimigo
            if (outro.controlador != this.controlador)
            {
                float distancia = Vector3.Distance(transform.position, outro.transform.position);

                //verifica se é a menor distância
                if (distancia < menorDistancia)
                {
                    menorDistancia = distancia;
                    alvoProximo = outro;
                }
            }
        }

        if (alvoProximo != null)
        {
            //define seu alvo e verifica o tipo do personagem para definir seu comportamento a partir disso
            _personagemAlvo = alvoProximo;
            _alvoAtual = _personagemAlvo.transform;
            VerificarComportamento("perseguir"); //personagem deve perseguir
        }
        else
        {
            Debug.Log("Nenhum inimigo encontrado");
        }
    }
    #endregion

    #region Perseguir
    private void Perseguir() //função que faz o personagem perseguir seu alvo
    {
        //checa o tipo do personagem
        if (_tipo == TipoDePersonagem.CURTA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a distância do personagem e seu alvo

            if (distancia > 2f) //verifica se está próximo para atacar
            {
                //move o personagem
                Vector3 direcao = (_alvoAtual.position - transform.position).normalized;
                transform.position += direcao * _velocidade * Time.deltaTime;
                transform.forward = direcao; //rotaciona o personagem em direção ao seu alvo

                //caso deva usar as animações
                if (_sistemaDeBatalha.usarAnimações && _animator != null)
                {
                    _animator.ResetTrigger("Atacar"); //reseta a animação de atacar
                    _animator.SetTrigger("Perseguir"); //aciona a animação de perseguir
                }
            }
            else
            {
                VerificarComportamento("atacar"); //ataca
            }
        }
        else if (_tipo == TipoDePersonagem.LONGA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a distância do personagem e seu alvo

            if (distancia > distanciaMinimaParaAtacar) //verifica se deve se aproximar mais para atacar
            {
                //move o personagem
                Vector3 direcao = (_alvoAtual.position - transform.position).normalized;
                transform.position += direcao * _velocidade * Time.deltaTime;
                transform.forward = direcao; //rotaciona o personagem em direção ao seu alvo

                //caso deva usar as animações
                if (_sistemaDeBatalha.usarAnimações && _animator != null)
                {
                    _animator.ResetTrigger("Atacar"); //reseta a animação de atacar
                    _animator.SetTrigger("Perseguir"); //aciona a animação de perseguir
                }
            }
            else
            {
                VerificarComportamento("atacar"); //ataca
            }
        }
    }
    #endregion

    #region Atacar
    private void Atacar() //função que faz o personagem atacar seu alvo
    {
        if (_cooldownAtual > 0f)
        {
            _cooldownAtual -= Time.deltaTime;
        }

        _podeAtacar = false; //personagem não pode atacar

        //checa o tipo de personagem
        if(_tipo == TipoDePersonagem.CURTA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a distância do personagem e seu alvo

            if (distancia <= 2f) //verifica se está próximo para atacar
            {
                _podeAtacar = true; //personagem pode atacar
            }
            else
            {
                VerificarComportamento("perseguir"); //persegue
            }
        }
        else if(_tipo == TipoDePersonagem.LONGA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a distância do personagem e seu alvo

            if (distancia < distanciaMinimaParaAtacar) //verifica se deve se aproximar mais para atacar
            {
                _podeAtacar = true; //personagem pode atacar
            }
            else
            {
                VerificarComportamento("perseguir"); //persegue
            }
        }

        //chama a função de ataque caso o personagem possa atacar
        if (_podeAtacar && _cooldownAtual <= 0f)
        {
            Ataque();
        }
    }

    private void Ataque() //função do ataque em si
    {
        _cooldownAtual = _cooldown; //reinicia o cooldown

        //caso deva usar as animações
        if (_sistemaDeBatalha.usarAnimações && _animator != null)
        {
            _animator.ResetTrigger("Perseguir"); //reseta a animação de perseguir
            _animator.SetTrigger("Atacar"); //aciona a animação de atacar
        }
        else
        {
            AtivarHit();
        }
    }

    public void AtivarHit() //função para ativar o hit
    {
        //verifica se há um hit, e o ativa se sim
        if (_hitAtaquePersonagem != null)
        {
            _hitAtaquePersonagem.gameObject.SetActive(true);

            //se é um personagem de longa distancia, transtorma o hit em um ataque de longa distancia
            if(_tipo == TipoDePersonagem.LONGA_DISTANCIA)
            {
                _hitAtaquePersonagem.ResetarPosição(); //reseta a posição do hit
                _hitAtaquePersonagem.MoverAteAlvo(_alvoAtual, velocidadeDoProjetil); //faz com que ele se mova até o alvo
            }
        }
    }
    #endregion

    public void CausarDano(IAPersonagemBase personagem) //função de causar dano a um personagem
    {
        //causa dano ao personagem se ele for um personagem inimigo e é o alvo atual deste personagem
        if (personagem.controlador != this.controlador && personagem == _personagemAlvo)
        {
            personagem.SofrerDano(_danoAtaqueBasico);
        }
    }

    public void SofrerDano(float dano) //função para sofrer dano
    {
        hpAtual -= dano; //sofre o dano

        if(hpAtual <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
