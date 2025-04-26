using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControladorDoPersonagem { PERSONAGEM_DO_JOGADOR, PERSONAGEM_INIMIGO } //quem controla o personagem, se � controlado pelo jogador ou pela IA inimiga
public enum TipoDePersonagem { CURTA_DISTANCIA, LONGA_DISTANCIA } //caracter�sticas referente ao comportamento de ataque personagem, se � um ataque de curta ou longa dist�ncia
public enum EstadoDoPersonagem { IDLE, PERSEGUINDO, ATACANDO, MORTO } //estados de comportamento do personagem

public class IAPersonagemBase : MonoBehaviour
{
    //�rea referente �s defini��es do personagem
    [Header("Defini��es")]
    public ControladorDoPersonagem controlador;
    [SerializeField]
    private TipoDePersonagem _tipo;
    public float distanciaMaximaParaAtacar = 5f; //distancia maxima que um personagem delonga distancia pode ter para atacar

    //�rea referente ao hp (vida) do personagem
    [Header("HP")]
    [SerializeField]
    private float _hpMaximoEInicial = 100f; //valor inicial que o hp atual do player ter� ao iniciar a batalha, e valor m�ximo que ele pode ter
    //[HideInInspector]
    public float hpAtual = 100f; //valor atual do hp (vida) do personagem

    //�rea referente ao movimento do personagem
    [Header("Movimento")]
    [SerializeField]
    private float _velocidade = 2f; //valor do ataque b�sico do personagem

    //�rea referente ao ataque do personagem
    [Header("Ataque")]
    [SerializeField]
    private float _ataqueBasico = 10f; //valor do ataque b�sico do personagem
    [SerializeField]
    private float _cooldown = 1f; //valor do tempo de espera para cada ataque b�sico do personagem

    [SerializeField]
    private EstadoDoPersonagem _comportamento;
    private IAPersonagemBase _personagemAlvo; //alvo de ataques do personagem
    private Transform _alvoAtual;
    private SistemaDeBatalha _sistemaDeBatalha; //sistema de batalha

    //�rea de feedback visuais
    private Animator _animator;
    private bool _usarAnima��es; //vari�vel para verificar se deve usar as anima��es
    private bool _usarSliders; //vari�vel para verificar se deve usar os sliders

    public void IniciarBatalha() //fun��o chamada ao inicar a batalha e define os valores e comportamentos iniciais do personagem
    {
        _sistemaDeBatalha = GameObject.FindGameObjectWithTag("SistemaDeBatalha").GetComponent<SistemaDeBatalha>(); //encontra o sistema de batalha na cena
        hpAtual = _hpMaximoEInicial; //define o hp atual do personagem igual ao valor m�ximo e inicial
        SelecionarAlvo(); //chama a fun��o para o personagem encontrar seu alvo

        _usarAnima��es = _sistemaDeBatalha.usarAnima��es; //verifica se deve usar anima��es referente o sistema de batalha
        if (_usarAnima��es)
        {
            //encontra o animator no objeto
            _animator = GetComponent<Animator>();
            if(_animator == null)
            {
                Debug.Log("N�o h� animator");
            }
        }
        _usarSliders = _sistemaDeBatalha.usarSliders; //verifica se deve usar sliders referente o sistema de batalha
    }

    private void Update()
    {
        if(_comportamento == EstadoDoPersonagem.PERSEGUINDO)
        {
            Perseguir();
        }
    }

    #region Sele��oDeAlvo
    private void SelecionarAlvo() //fun��o que seleciona o alvo de ataques do personagem
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
        //cria um raycast para verificar se h� um inimigo a sua frente
        RaycastHit hit;
        Vector3 origem = transform.position + Vector3.up; //origem do raycast
        Vector3 direcao = transform.forward; //dire��o do raycast
        float distancia = 100f; //distancia do raycast

        if (Physics.Raycast(origem, direcao, out hit, distancia)) //checa se o raycast colidiu com algo
        {
            //verifica se o raycast colidiu com um personagem
            _personagemAlvo = hit.collider.GetComponent<IAPersonagemBase>();

            //verifica se o personagem visto � seu inimigo
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

            //verifica se o outro personagem � seu inimigo
            if (outro.controlador != this.controlador)
            {
                float distancia = Vector3.Distance(transform.position, outro.transform.position);

                //verifica se � a menor dist�ncia
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

    private void VerificarComportamento(string comportamento) //fun��o que verifica qual deve ser o comportamento do personagem
    {
        if(comportamento == "perseguir")
        {
            _comportamento = EstadoDoPersonagem.PERSEGUINDO;
        }
        else if (comportamento == "atacar")
        {
            _comportamento = EstadoDoPersonagem.ATACANDO;
        }
        else if(comportamento == "selecionarAlvo")
        {
            _comportamento = EstadoDoPersonagem.IDLE;
            SelecionarAlvo();
        }
    }

    private void Perseguir() //fun��o que faz o personagem perseguir seu alvo
    {
        //checa o tipo do personagem
        if (_tipo == TipoDePersonagem.CURTA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a dist�ncia do personagem e seu alvo

            if (distancia > 2f) //verifica se est� pr�ximo para atacar
            {
                //move o personagem
                Vector3 direcao = (_alvoAtual.position - transform.position).normalized;
                transform.position += direcao * _velocidade * Time.deltaTime;
                transform.forward = direcao; //rotaciona o personagem em dire��o ao seu alvo

                //caso deva usar as anima��es
                if (_sistemaDeBatalha.usarAnima��es && _animator != null)
                {
                    _animator.SetBool("Perseguindo", true); //aciona a anima��o de perseguir
                }
            }
            else
            {
                VerificarComportamento("atacar"); //ataca
                _animator.SetBool("Atacando", true); //aciona a anima��o de atacar
            }
        }
        else if (_tipo == TipoDePersonagem.LONGA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a dist�ncia do personagem e seu alvo

            if (distancia > distanciaMaximaParaAtacar) //verifica se deve se aproximar mais para atacar
            {
                //move o personagem
                Vector3 direcao = (_alvoAtual.position - transform.position).normalized;
                transform.position += direcao * _velocidade * Time.deltaTime;
                transform.forward = direcao; //rotaciona o personagem em dire��o ao seu alvo

                //caso deva usar as anima��es
                if (_sistemaDeBatalha.usarAnima��es && _animator != null)
                {
                    _animator.SetBool("Perseguindo", true); //aciona a anima��o de perseguir
                }
            }
            else
            {
                VerificarComportamento("atacar"); //ataca
                _animator.SetBool("Atacando", true); //aciona a anima��o de atacar
            }
        }
    }
}
