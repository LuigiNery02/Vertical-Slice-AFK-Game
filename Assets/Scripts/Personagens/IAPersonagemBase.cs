using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ControladorDoPersonagem { PERSONAGEM_DO_JOGADOR, PERSONAGEM_INIMIGO } //quem controla o personagem, se � controlado pelo jogador ou pela IA inimiga
public enum TipoDePersonagem { CURTA_DISTANCIA, LONGA_DISTANCIA } //caracter�sticas referente ao comportamento de ataque personagem, se � um ataque de curta ou longa dist�ncia
public enum EstadoDoPersonagem { IDLE, PERSEGUINDO, ATACANDO, MORTO } //estados de comportamento do personagem

public class IAPersonagemBase : MonoBehaviour
{
    //�rea referente �s defini��es do personagem
    [Header("Defini��es")]
    public ControladorDoPersonagem controlador;
    public TipoDePersonagem _tipo;
    [HideInInspector]
    public EstadoDoPersonagem _comportamento;
    public float distanciaMinimaParaAtacar = 5f; //distancia minima que um personagem de longa distancia deve ter para atacar
    public float velocidadeDoProjetil; //velocidade do projetil do ataque do personagem de longa distancia

    //�rea referente ao hp (vida) do personagem
    [Header("HP")]
    [SerializeField]
    private float _hpMaximoEInicial = 100f; //valor inicial que o hp atual do player ter� ao iniciar a batalha, e valor m�ximo que ele pode ter
    [HideInInspector]
    public float hpAtual = 100f; //valor atual do hp (vida) do personagem

    //�rea referente ao movimento do personagem
    [Header("Movimento")]
    [SerializeField]
    private float _velocidade = 2f; //valor do ataque b�sico do personagem

    //�rea referente ao ataque do personagem
    [Header("Ataque")]
    [SerializeField]
    private float _danoAtaqueBasico = 10f; //valor do dano do ataque b�sico do personagem
    [SerializeField]
    private float _cooldown = 1f; //valor do tempo de espera para cada ataque b�sico do personagem
    private float _cooldownAtual = 0f; //tempo atual para o personagem poder atacar novamente
    private bool _podeAtacar; //vari�vel que verifica se o personagem pode atacar

    //�rea referente aos sfx
    [Header("SFX")]
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _contatoSFX;
    [SerializeField]
    private AudioClip _projetilSFX;

    //[HideInInspector]
    public IAPersonagemBase _personagemAlvo; //alvo de ataques do personagem
    private Transform _alvoAtual; //transform do personagem alvo
    private SistemaDeBatalha _sistemaDeBatalha; //sistema de batalha
    private HitAtaquePersonagem _hitAtaquePersonagem; //hit do personagem
    private SkinnedMeshRenderer _malha; //malha do personagem
    [HideInInspector] 
    public Vector3 posicaoInicial; //posi��o inicial do personagem
    [HideInInspector]
    public Quaternion rotacaoInicial; //rota��o inicial do personagem


    //�rea de feedback visuais
    private Animator _animator; //animator do personagem
    private Slider _slider; //slider do hp do personagem
    private bool _usarAnima��es; //vari�vel para verificar se deve usar as anima��es
    private bool _usarSliders; //vari�vel para verificar se deve usar os sliders
    private bool _usarSFX; //vari�vel para verificar se deve usar os sfxs

    public void IniciarBatalha() //fun��o chamada ao inicar a batalha e define os valores e comportamentos iniciais do personagem
    {
        _sistemaDeBatalha = GameObject.FindGameObjectWithTag("SistemaDeBatalha").GetComponent<SistemaDeBatalha>(); //encontra o sistema de batalha na cena
        _hitAtaquePersonagem = transform.GetComponentInChildren<HitAtaquePersonagem>(true); //encontra o hit do personagem dentro de si
        hpAtual = _hpMaximoEInicial; //define o hp atual do personagem igual ao valor m�ximo e inicial

        //encontra a malha do personagem caso tenha
        if(gameObject.GetComponentInChildren<SkinnedMeshRenderer>() != null )
        {
            _malha = GetComponentInChildren<SkinnedMeshRenderer>();
        }

        //encontra o slider do personagem caso tenha
        if (gameObject.GetComponentInChildren<Slider>() != null)
        {
            _slider = GetComponentInChildren<Slider>();
        }

        FeedbacksVisuais(); //chama a fun��o para verificar quais feedbacks visuais ir� usar
        SelecionarAlvo(); //chama a fun��o para o personagem encontrar seu alvo
    }

    private void VerificarComportamento(string comportamento) //fun��o que verifica qual deve ser o comportamento do personagem
    {
        if (comportamento == "perseguir")
        {
            _comportamento = EstadoDoPersonagem.PERSEGUINDO; //personagem persegue
        }
        else if (comportamento == "atacar") 
        {
            _comportamento = EstadoDoPersonagem.ATACANDO; //personagem ataca

            //faz o personagem olhar para o novo alvo imediatamente
            if (_alvoAtual != null)
            {
                Vector3 direcao = (_alvoAtual.position - transform.position).normalized;
                direcao.y = 0; //impede que ele rotacione no eixo Y se houver diferen�a de altura
                if (direcao != Vector3.zero)
                {
                    transform.forward = direcao;
                }
            }
        }
        else if (comportamento == "selecionarAlvo")
        {
            _comportamento = EstadoDoPersonagem.IDLE;
            SelecionarAlvo();
        }
        else if(comportamento == "morrer") //personagem morre
        {
            _comportamento = EstadoDoPersonagem.MORTO;
            Morrer();
        }
    }

    public void ResetarEstado() //fun��es para resetar os estados do personagem
    {
        transform.position = posicaoInicial; //restaura a posi��o
        transform.rotation = rotacaoInicial; //restaura a rota��o
        hpAtual = _hpMaximoEInicial; //restaura hp
        _cooldownAtual = 0; //restaura cooldown
        _comportamento = EstadoDoPersonagem.IDLE;
        FeedbacksVisuais();
        _animator.Rebind();
    }

    private void Update()
    {
        //checa se o personagem n�o est� morto
        if(_comportamento != EstadoDoPersonagem.MORTO)
        {
            //checa atualizadamente os estados de comportamento do personagem
            if (_comportamento == EstadoDoPersonagem.PERSEGUINDO)
            {
                Perseguir();
            }
            else if (_comportamento == EstadoDoPersonagem.ATACANDO)
            {
                Atacar();
            }

            if(_personagemAlvo != null && _personagemAlvo._comportamento == EstadoDoPersonagem.MORTO)
            {
                VerificarComportamento("selecionarAlvo");
            }
        }   
    }

    #region Sele��oDeAlvo
    private void SelecionarAlvo() //fun��o que seleciona o alvo de ataques do personagem
    {
        if(_sistemaDeBatalha != null || !_sistemaDeBatalha.fimDeBatalha) //caso tenha encontrado o sistema de batalha na cena anteriormente e n�o seja fim de batalha pode selecionar um alvo
        {
            _cooldownAtual = 0; //resetar cooldown para novo alvo

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

            //verifica se o personagem visto � seu inimigo e se n�o est� morto
            if (_personagemAlvo != null && _personagemAlvo.controlador != controlador && _personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
            {
                _alvoAtual = _personagemAlvo.transform;
                VerificarComportamento("perseguir"); //personagem deve perseguir
            }
        }
        else
        {
            SelecionarAlvoProximo();
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
            //ignora a si mesmo e um personagem morto
            if(outro == this || outro._comportamento == EstadoDoPersonagem.MORTO)
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
    }
    #endregion

    #region Perseguir
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
                    _animator.ResetTrigger("Atacar"); //reseta a anima��o de atacar
                    _animator.SetTrigger("Perseguir"); //aciona a anima��o de perseguir
                }
            }
            else
            {
                VerificarComportamento("atacar"); //ataca
            }
        }
        else if (_tipo == TipoDePersonagem.LONGA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a dist�ncia do personagem e seu alvo

            if (distancia > distanciaMinimaParaAtacar) //verifica se deve se aproximar mais para atacar
            {
                //move o personagem
                Vector3 direcao = (_alvoAtual.position - transform.position).normalized;
                transform.position += direcao * _velocidade * Time.deltaTime;
                transform.forward = direcao; //rotaciona o personagem em dire��o ao seu alvo

                //caso deva usar as anima��es
                if (_sistemaDeBatalha.usarAnima��es && _animator != null)
                {
                    _animator.ResetTrigger("Atacar"); //reseta a anima��o de atacar
                    _animator.SetTrigger("Perseguir"); //aciona a anima��o de perseguir
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
    private void Atacar() //fun��o que faz o personagem atacar seu alvo
    {
        if (_cooldownAtual > 0f)
        {
            _cooldownAtual -= Time.deltaTime;
            _podeAtacar = false; //personagem n�o pode atacar
        }

        //checa o tipo de personagem
        if(_tipo == TipoDePersonagem.CURTA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a dist�ncia do personagem e seu alvo

            if (distancia <= 2f) //verifica se est� pr�ximo para atacar
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
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a dist�ncia do personagem e seu alvo

            if (distancia < distanciaMinimaParaAtacar) //verifica se deve se aproximar mais para atacar
            {
                _podeAtacar = true; //personagem pode atacar
            }
            else
            {
                VerificarComportamento("perseguir"); //persegue
            }
        }

        //chama a fun��o de ataque caso o personagem possa atacar
        if (_podeAtacar && _cooldownAtual <= 0f)
        {
            Ataque();
        }
    }

    private void Ataque() //fun��o do ataque em si
    {
        _cooldownAtual = _cooldown; //reinicia o cooldown

        //caso deva usar as anima��es
        if (_sistemaDeBatalha.usarAnima��es && _animator != null)
        {
            _animator.ResetTrigger("Perseguir"); //reseta a anima��o de perseguir
            _animator.SetTrigger("Atacar"); //aciona a anima��o de atacar
        }
        else
        {
            AtivarHit();
        }
    }

    public void AtivarHit() //fun��o para ativar o hit
    {
        //verifica se h� um hit, e o ativa se sim
        if (_hitAtaquePersonagem != null)
        {
            _hitAtaquePersonagem.gameObject.SetActive(true);
            _hitAtaquePersonagem.usarSFX = _usarSFX;

            //se � um personagem de longa distancia, transtorma o hit em um ataque de longa distancia
            if(_tipo == TipoDePersonagem.LONGA_DISTANCIA)
            {
                _hitAtaquePersonagem.ResetarPosi��o(); //reseta a posi��o do hit
                _hitAtaquePersonagem.MoverAteAlvo(_alvoAtual, velocidadeDoProjetil); //faz com que ele se mova at� o alvo

                //toca o sfx de proj�til se deve usar os sfxs
                if (_usarSFX)
                {
                    _audio.clip = _projetilSFX;
                    _audio.Play();
                }
            }
        }
    }
    #endregion

    #region Dano
    public void CausarDano(IAPersonagemBase personagem) //fun��o de causar dano a um personagem
    {
        //causa dano ao personagem se ele for um personagem inimigo e � o alvo atual deste personagem
        if (personagem.controlador != this.controlador && personagem == _personagemAlvo)
        {
            personagem.SofrerDano(_danoAtaqueBasico);

            //toca o sfx de contato se deve usar os sfxs
            if (_usarSFX)
            {
                _audio.clip = _contatoSFX;
                _audio.Play();
            }
        }
    }

    public void SofrerDano(float dano) //fun��o para sofrer dano
    {
        hpAtual -= dano; //sofre o dano

        if (_usarSliders)
        {
            //atualiza o slider
            _slider.value = hpAtual;
        }

        if(hpAtual <= 0)
        {
            hpAtual = 0;
            VerificarComportamento("morrer");
        }
    }
    #endregion

    #region Morte
    private void Morrer() //fun��o de morte do personagem
    {
        //se remove do time em que faz parte
        if(controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
        {
            _sistemaDeBatalha.AtualizarTime(("remover"), ("jogador"), this);
        }
        else if(controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
        {
            _sistemaDeBatalha.AtualizarTime(("remover"), ("inimigo"), this);
        }

        //caso deva usar as anima��es
        if (_sistemaDeBatalha.usarAnima��es && _animator != null)
        {
            _animator.ResetTrigger("Perseguir");
            _animator.ResetTrigger("Atacar");
            _animator.SetTrigger("Morrer");
        }
        else
        {
            //desativa a malha
            if(_malha != null)
            {
                _malha.gameObject.SetActive(false);
            }
        }
        _hitAtaquePersonagem.gameObject.SetActive(false); //desativa o hit
    }
    #endregion

    #region Feedbacks Visuais
    private void FeedbacksVisuais() //fun��o para verificar feedbacks visuais
    {
        _usarAnima��es = _sistemaDeBatalha.usarAnima��es; //verifica se deve usar anima��es referente o sistema de batalha
        _usarSliders = _sistemaDeBatalha.usarSliders; //verifica se deve usar sliders de hp referente ao sistema de batalha
        _usarSFX = _sistemaDeBatalha.usarSfxs; //verifica se deve usar sfxs referente ao sistema de batalha

        if (_usarAnima��es)
        {
            //encontra o animator no objeto
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.Log("N�o h� animator");
            }
        }

        if (_usarSliders)
        {
            if (_slider == null)
            {
                Debug.Log("N�o h� slider");
            }
            else
            {
                //referencia o hp ao slider
                _slider.maxValue = _hpMaximoEInicial;
                _slider.value = hpAtual;
            }
        }
        else
        {
            _slider.gameObject.SetActive(false);
        }

        if (_usarSFX)
        {
            if(_hitAtaquePersonagem != null)
            {
                _hitAtaquePersonagem.usarSFX = _usarSFX;
            }
        }
    }
    #endregion
}
