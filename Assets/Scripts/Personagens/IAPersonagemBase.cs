using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ControladorDoPersonagem { PERSONAGEM_DO_JOGADOR, PERSONAGEM_INIMIGO } //quem controla o personagem, se é controlado pelo jogador ou pela IA inimiga
public enum TipoDeArma { CURTA_DISTANCIA, LONGA_DISTANCIA } //características referente ao comportamento de ataque da arma do personagem, se é um ataque de curta ou longa distância
public enum EstadoDoPersonagem { IDLE, PERSEGUINDO, ATACANDO, MORTO, MOVIMENTO_ESPECIAL, STUNADO, CONJURANDO_HABILIDADE} //estados de comportamento do personagem

public enum EfeitoMarcadorDeAlvo { NENHUM, EXPOSTO, SANGRAMENTO, ATORDOADO, CORTACURA, MARCADO_PARA_EXECUCAO}

public class IAPersonagemBase : MonoBehaviour
{
    //área referente aos dados e visual do personagem selecionado
    [Header("Personagem Selecionado")]
    public PersonagemData personagem; //dados do personagem criado
    public GameObject[] personagemVisual; //visual do personagem
    public GameObject canvas; //canvas do personagem

    //área referente às definições do personagem
    [Header("Definições")]
    public ControladorDoPersonagem controlador;
    [HideInInspector]
    public TipoDeArma _tipo;
    [HideInInspector]
    public EstadoDoPersonagem _comportamento;

    //área referente ao hp (vida) do personagem
    [Header("HP")]
    [HideInInspector]
    public float _hpMaximoEInicial; //valor inicial que o hp atual do player terá ao iniciar a batalha, e valor máximo que ele pode ter
    [HideInInspector]
    public float hpAtual; //valor atual do hp (vida) do personagem
    [HideInInspector]
    public float hpRegeneracao; //valor por segundo que o personagem recuperará de hp

    //área referente ao sp (pontos de habilidade) do personagem
    [Header("SP")]
    [HideInInspector]
    public float _spMaximoEInicial; //valor inicial que o SP atual do player terá ao iniciar a batalha, e valor máximo que ele pode ter
    [HideInInspector]
    public float spAtual; //valor atual do SP (pontos de habilidade) do personagem
    [HideInInspector]
    public float spRegeneracao; //valor por segundo que o personagem recuperará de sp

    private Coroutine regeneracaoCoroutine; //coroutine de regeneração de hp e sp

    //área referente ao ataque do personagem
    [Header("Ataque")]
    [HideInInspector]
    public float precisao; //precisão do personagem
    [HideInInspector]
    public float _dano; //valor do dano do ataque do personagem
    [HideInInspector]
    public float _velocidadeDeAtaque; //valor da velocidade de ataque do personagem
    private float _cooldownAtual = 0f; //tempo atual para o personagem poder atacar novamente
    [HideInInspector]
    public float esquiva; //valor da esquiva do personagem
    private bool _podeAtacar; //variável que verifica se o personagem pode atacar
    public float chanceCritico;
    public float multiplicadorCritico;

    //área referente às definições do personagem de longa distancia
    [Header("Definições Longa Distância")]
    public float distanciaMinimaParaAtacar = 5f; //distancia minima que um personagem de longa distancia deve ter para atacar

    //área referente ao movimento do personagem
    [Header("Movimento")]
    public float _velocidade = 1; //valor da velocidade de movimento do personagem

    //área referente à defesa do personagem
    [Header("Defesa")]
    public float defesa; //defesa do personagem
    public float defesaMagica; //defesa mágica do personagem

    //área referente ao escudo do personagem
    [Header("Escudo")]
    public bool escudoAtivado; //define se o escudo está ativado ou não
    public float valorEscudo; //valor em % do escudo
    public GameObject escudoVfx;

    //área referente à habilidades
    [Header("Habilidades")]
    public int willPower;
    public int marcadoresDeAlvo;
    public EfeitoMarcadorDeAlvo efeitoMarcadorDeAlvo;
    //[HideInInspector]
    public HabilidadeAtiva habilidadeAtivaClasse; //habilidade ativa de classe do personagem
    //[HideInInspector]
    public HabilidadeAtiva habilidadeAtivaArma; //habilidade ativa de arma do personagem
    //[HideInInspector]
    public HabilidadePassiva habilidadePassivaClasse; //habilidade passiva de classe do personagem
    //[HideInInspector]
    public HabilidadePassiva habilidadePassivaArma; //habilidade passiva de arma do personagem
    public bool podeAtivarEfeitoHabilidadeAtivaClasse = false; //variável que determina se pode ativar ou não o efeito da habilidade ativa de classe
    public bool podeAtivarEfeitoHabilidadeAtivaArma = false; //variável que determina se pode ativar ou não o efeito da habilidade ativa de arma
    public bool podeAtivarEfeitoHabilidadePassivaClasse = false; //variável que determina se pode ativar ou não o efeito da habilidade passiva de classe
    public bool podeAtivarEfeitoHabilidadePassivaArma = false; //variável que determina se pode ativar ou não o efeito da habilidade passiva de arma
    public Dictionary<HabilidadePassiva, DadosHabilidadePassiva> dadosDasHabilidadesPassivas = new();
    public GameObject vfxHabilidadeAtivaClasse;
    public GameObject vfxHabilidadeAtivaArma;
    public GameObject vfxHabilidadePassivaClasse;
    public GameObject vfxHabilidadePassivaArma;
    [HideInInspector]
    public bool executandoHabilidadeAtiva;

    //Área referente às animações
    [Header("Animação")]
    public RuntimeAnimatorController[] controllerAnimatorArma; //controller animator referente à arma do personagem

    //Área referente aos sfx
    [Header("SFX")]
    public AudioSource _audio; //áudiosource do personagem
    [SerializeField]
    private AudioClip _contatoSFX; //áudio sfx do contato(hit)
    [SerializeField]
    private AudioClip _projetilSFX; //áudio sfx do projétil
    [SerializeField]
    public AudioClip _habilidadeSFX; //áudio sfx da habilidade

    [HideInInspector]
    public int id; //variável para verificar o id do personagem
    [HideInInspector]
    public IAPersonagemBase _personagemAlvo; //alvo de ataques do personagem
    [HideInInspector]
    public Transform _alvoAtual; //transform do personagem alvo
    private SistemaDeBatalha _sistemaDeBatalha; //sistema de batalha
    private GerenciadorDeObjectPool _gerenciadorDePool; //gerenciador de object pool
    private HitAtaquePersonagem _hitAtaquePersonagem; //hit do personagem
    [HideInInspector] 
    public Vector3 posicaoInicial; //posição inicial do personagem
    [HideInInspector]
    public Quaternion rotacaoInicial; //rotação inicial do personagem
    [HideInInspector]
    public bool executandoMovimentoEspecial; //variável para verificar se o personagem está executando o movimento especial
    [HideInInspector]
    public string movimentoEspecial;

    //função que é ativada quando há um efeito por ataque
    public delegate void EfeitoPorAtaque(bool acerto);
    public EfeitoPorAtaque efeitoPorAtaque;
    [HideInInspector]
    public bool efeitoPorAtaqueAtivado; //verifica se efeitos por ataque de habilidades estão ativados
    [HideInInspector]
    public Dictionary<string, EfeitoPorAtaque> efeitosPorAtaque = new();
    [HideInInspector]
    public event EfeitoPorAtaque OnAtaqueComEfeito;

    public delegate void EfeitoPorAtaqueRecebido(bool acerto);
    public EfeitoPorAtaqueRecebido efeitoPorAtaqueRecebido;
    [HideInInspector]
    public bool efeitoPorAtaqueRecebidoAtivado; //verifica se efeitos por ataque recebidos de habilidades estão ativados
    [HideInInspector]
    public Dictionary<string, EfeitoPorAtaqueRecebido> efeitosPorAtaqueRecebidos = new();
    [HideInInspector]
    public event EfeitoPorAtaqueRecebido OnAtaqueRecebidoComEfeito;

    public delegate void EfeitoPorDanoCausado();
    public EfeitoPorDanoCausado efeitoPorDanoCausado;
    [HideInInspector]
    public bool efeitoPorDanoCausadoAtivado; //verifica se efeitos por danos causados de habilidades estão ativados
    [HideInInspector]
    public Dictionary<string, EfeitoPorDanoCausado> efeitosPorDanosCausados = new();
    [HideInInspector]
    public event EfeitoPorDanoCausado OnDanoCausadoComEfeito;

    public event Action<int> aoGastarWillPower;
    public event Action<int> aoReceberWillPower;

    [HideInInspector]
    public bool stunado;
    [HideInInspector]
    public float tempoDeStun;
    [HideInInspector]
    public bool ataqueDiminuido;
    [HideInInspector]
    public bool imuneAMagias;
    [HideInInspector]
    public bool imuneAStun;
    [HideInInspector]
    public bool imuneAKnockback;
    [HideInInspector]
    public bool sangramento;
    [SerializeField]
    private GameObject _vfxSangramento;
    [HideInInspector]
    public bool medo;
    [HideInInspector]
    public float reducaoDanoMedo;

    [HideInInspector]
    public bool recebeuDebuffPunhoDisciplina;

    [HideInInspector]
    public bool conjurandoHabilidade;
    [HideInInspector]
    public float tempoDeCast;
    [HideInInspector]
    public int habilidadeSendoConjurada;

    private bool atualizandoDados = false;

    //Área de feedback visuais
    private Animator _animator; //animator do personagem
    private Slider _slider; //slider do hp do personagem
    public Text textoHP; //texto mostrando a atualização do hp do personagem
    public Text textoWillPower; //texto mostrando a atualização do willpower do personagem
    public Text pontosDeHabilidadeTexto; //texto referente aos pontos de habilidade do personagem
    private bool _usarAnimações; //variável para verificar se deve usar as animações
    private bool _usarSliders; //variável para verificar se deve usar os sliders
    private bool _usarSFX; //variável para verificar se deve usar os sfxs

    private void Start()
    {
        _sistemaDeBatalha = GameObject.FindGameObjectWithTag("SistemaDeBatalha").GetComponent<SistemaDeBatalha>(); //encontra o sistema de batalha na cena
        _gerenciadorDePool = FindFirstObjectByType<GerenciadorDeObjectPool>(); //encontra o gerenciador de pool na cena

        //verifica se deve usar os sliders, e os encontra se for personagem inimigo
        if (SistemaDeBatalha.usarSliders)
        {
            if (controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                _slider = GetComponentInChildren<Slider>();
            }
        }
    }

    private void OnEnable()
    {
        _sistemaDeBatalha = GameObject.FindGameObjectWithTag("SistemaDeBatalha").GetComponent<SistemaDeBatalha>(); //encontra o sistema de batalha na cena

        //verifica se deve usar os sliders, e os encontra se for personagem inimigo
        if (SistemaDeBatalha.usarSliders)
        {
            if (controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                _slider = GetComponentInChildren<Slider>();
            }
        }

        //recebe os dados de batalha do personagem caso for um inimigo
        if (controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
        {
            AtualizarDadosPersonagem();
        }
    }

    public void ResetarDadosPersonagem() //função que reseta os dados visuais do personagem selecionado
    {
        //reseta os dados do personagem
        personagem.funcaoSubirNivel -= AtualizarNivel;

        for (int i = 0; i < personagemVisual.Length; i++)
        {
            personagemVisual[i].gameObject.SetActive(false);
        }

        canvas.SetActive(false);
    }

    public void ReceberDadosPersonagem() //função que recebe e define os dados do personagem selecionado
    {
        personagem.funcaoSubirNivel += AtualizarNivel;

        AtualizarDadosPersonagem();

        switch (personagem.classe)
        {
            case Classe.Guerreiro:
                id = 0;
                break;
            case Classe.Ladino:
                id = 1;
                break;
            case Classe.Elementalista:
                id = 2;
                break;
            case Classe.Sacerdote:
                id = 3;
                break;
        }

        personagemVisual[id].SetActive(true);
        AtivarArmaPersonagem armaPersonagem = personagemVisual[id].GetComponent<AtivarArmaPersonagem>();
        if(armaPersonagem != null)
        {
            armaPersonagem.AtivarArma(personagem.arma.id);
        }
        Animator animatorPersonagem = personagemVisual[id].GetComponent<Animator>();
        if(animatorPersonagem != null)
        {
            animatorPersonagem.runtimeAnimatorController = controllerAnimatorArma[personagem.arma.id];
        }
        canvas.SetActive(true);

        //encontra o slider do personagem caso tenha
        if (transform.GetComponentInChildren<Slider>() != null)
        {
            _slider = transform.GetComponentInChildren<Slider>();
        }
    }

    public void AtualizarDadosPersonagem() //função que atualiza os dados de batalha do personagem
    {
        if (atualizandoDados)
        {
            return;
        }

        atualizandoDados = true;

        if (habilidadeAtivaClasse == null)
        {
            habilidadeAtivaClasse = personagem.habilidadeAtivaClasse;
        }

        if (habilidadeAtivaArma == null)
        {
            habilidadeAtivaArma = personagem.habilidadeAtivaArma;
        }

        if (habilidadePassivaClasse == null)
        {
            habilidadePassivaClasse = personagem.habilidadePassivaClasse;
        }

        if (habilidadePassivaArma == null)
        {
            habilidadePassivaArma = personagem.habilidadePassivaArma;
        }

        _tipo = personagem.arma.armaTipo;
        _dano = personagem.dano;
        _velocidadeDeAtaque = personagem.velocidadeDeAtaque;
        precisao = personagem.precisao;
        chanceCritico = personagem.chanceCritico;
        multiplicadorCritico = personagem.multiplicadorCritico;
        distanciaMinimaParaAtacar = personagem.ranged;

        defesa = personagem.defesa;
        defesaMagica = personagem.defesaMagica;
        esquiva = personagem.esquiva;

        _velocidade = personagem.velocidadeDeMovimento;

        _hpMaximoEInicial = personagem.hp;
        hpRegeneracao = personagem.hpRegeneracao;
        _spMaximoEInicial = personagem.sp;
        spRegeneracao = personagem.spRegeneracao;

        if (habilidadePassivaClasse != null && podeAtivarEfeitoHabilidadePassivaClasse)
        {
            habilidadePassivaClasse.RemoverEfeito(this);
            habilidadePassivaClasse.AtivarEfeito(this);
        }

        if (habilidadePassivaArma != null && podeAtivarEfeitoHabilidadePassivaArma)
        {
            habilidadePassivaArma.RemoverEfeito(this);
            habilidadePassivaArma.AtivarEfeito(this);
        }

        atualizandoDados = false;
    }
    public void IniciarBatalha() //função chamada ao inicar a batalha e define os valores e comportamentos iniciais do personagem
    {
        //verifica se há habilidades equipadas, e habilita elas se sim

        if (habilidadeAtivaClasse != null)
        {
            podeAtivarEfeitoHabilidadeAtivaClasse = true;
        }

        if(habilidadeAtivaArma != null)
        {
            podeAtivarEfeitoHabilidadeAtivaArma = true;
        }

        if (habilidadePassivaClasse != null)
        {
            podeAtivarEfeitoHabilidadePassivaClasse = true;
            habilidadePassivaClasse.AtivarEfeito(this);
        }

        if (habilidadePassivaArma != null)
        {
            podeAtivarEfeitoHabilidadePassivaArma = true;
            habilidadePassivaArma.AtivarEfeito(this);
        }

        if(controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
        {
            if(habilidadeAtivaClasse != null || habilidadeAtivaArma!= null)
            {
                StartCoroutine(EsperarAtivacaoHabilidadesInimigo());
            }
        }

        //encontra o hit dentro de si caso esteja com uma arma melee equipada e à ativa
        if (personagem.arma.armaDano == TipoDeDano.DANO_MELEE)
        {
            _hitAtaquePersonagem = GetComponentInChildren<HitAtaquePersonagem>(true);
        }

        hpAtual = _hpMaximoEInicial; //define o hp atual do personagem igual ao valor máximo e inicial
        spAtual = _spMaximoEInicial; //define o sp atual do personagem igual ao valor máximo e inicial

        AtualizarFeedbackSP();

        if(personagem.statusEspecial == StatusEspecial.Willpower && textoWillPower != null)
        {
            textoWillPower.text = "WillPower: " + willPower;
        }

        FeedbacksVisuais(); //chama a função para verificar quais feedbacks visuais irá usar
        SelecionarAlvo(); //chama a função para o personagem encontrar seu alvo

        //inicia a coroutine de regeneração de hp e sp
        if(regeneracaoCoroutine == null)
        {
            regeneracaoCoroutine = StartCoroutine(RegeneracaoLoop());
        }
            
    }

    public void VerificarComportamento(string comportamento) //função que verifica qual deve ser o comportamento do personagem
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
                direcao.y = 0; //impede que ele rotacione no eixo Y se houver diferença de altura
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
        else if(comportamento == "movimentoEspecial")
        {
            _comportamento = EstadoDoPersonagem.MOVIMENTO_ESPECIAL;
            MovimentoEspecial();
        }
        else if (comportamento == "stun")
        {
            _comportamento = EstadoDoPersonagem.STUNADO;
            Stun();
        }
        else if (comportamento == "conjurarHabilidade1" && !conjurandoHabilidade)
        {
            _comportamento = EstadoDoPersonagem.CONJURANDO_HABILIDADE;
            habilidadeSendoConjurada = 1;
            ConjurarHabilidade();
        }
        else if (comportamento == "conjurarHabilidade2" && !conjurandoHabilidade)
        {
            _comportamento = EstadoDoPersonagem.CONJURANDO_HABILIDADE;
            habilidadeSendoConjurada = 2;
            ConjurarHabilidade();
        }
    }

    public void ResetarEstado() //funções para resetar os estados do personagem
    {
        if(controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR) //verifica se é personagem do jogador
        {
            transform.position = posicaoInicial; //restaura a posição
            transform.rotation = rotacaoInicial; //restaura a rotação
        }
        hpAtual = _hpMaximoEInicial; //restaura hp
        _cooldownAtual = 0; //restaura cooldown
        _comportamento = EstadoDoPersonagem.IDLE;
        FeedbacksVisuais();
        if (_usarAnimações)
        {
            _animator.Rebind();
        }

        willPower = 0;
        marcadoresDeAlvo = 0;
        efeitoMarcadorDeAlvo = EfeitoMarcadorDeAlvo.NENHUM;
        stunado = false;
        conjurandoHabilidade = false;
        imuneAMagias = false;
        imuneAStun = false;
        imuneAKnockback = false;
        recebeuDebuffPunhoDisciplina = false;
        sangramento = false;
        medo = false;
    }

    private void Update()
    {
        //checa se o personagem não está morto, se a batalha iniciou e se o personagem não está em idle
        if(_comportamento != EstadoDoPersonagem.MORTO && _sistemaDeBatalha.batalhaIniciou && _comportamento != EstadoDoPersonagem.IDLE && _comportamento != EstadoDoPersonagem.STUNADO)
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
            //else if (_comportamento == EstadoDoPersonagem.MOVIMENTO_ESPECIAL)
            //{
            //    MovimentoEspecial(movimentoEspecialAtual);
            //}


            if (_personagemAlvo != null)
            {
                transform.LookAt(_personagemAlvo.transform); //faz o personagem sempre olhar para seu alvo

                //se seu alvo morreu, procura outro alvo
                if (_personagemAlvo._comportamento == EstadoDoPersonagem.MORTO)
                {
                    VerificarComportamento("selecionarAlvo");
                }
            }
        }
    }

    private IEnumerator RegeneracaoLoop() //loop de regeneração de HP e SP por segundo
    {
        //regenera hp e sp por segundo enquanto não está morto e enquanto a batalha está acontecendo
        while (_comportamento != EstadoDoPersonagem.MORTO && _sistemaDeBatalha.batalhaIniciou)
        {
            ReceberHP(hpRegeneracao);
            ReceberSP(spRegeneracao);

            yield return new WaitForSeconds(1f);
        }
    }

    #region SeleçãoDeAlvo
    private void SelecionarAlvo() //função que seleciona o alvo de ataques do personagem
    {
        if(_sistemaDeBatalha != null || !_sistemaDeBatalha.fimDeBatalha) //caso tenha encontrado o sistema de batalha na cena anteriormente e não seja fim de batalha pode selecionar um alvo
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
        //cria um raycast para verificar se há um inimigo a sua frente
        RaycastHit hit;
        Vector3 origem = transform.position + Vector3.up; //origem do raycast
        Vector3 direcao = transform.forward; //direção do raycast
        float distancia = 100f; //distancia do raycast

        if (Physics.Raycast(origem, direcao, out hit, distancia)) //checa se o raycast colidiu com algo
        {
            //verifica se o raycast colidiu com um personagem
            _personagemAlvo = hit.collider.GetComponent<IAPersonagemBase>();

            //verifica se o personagem visto é seu inimigo e se não está morto
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
    }
    #endregion

    #region Perseguir
    private void Perseguir() //função que faz o personagem perseguir seu alvo
    {
        //checa o tipo do personagem
        if (_tipo == TipoDeArma.CURTA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a distância do personagem e seu alvo
            if (distancia > 2f) //verifica se está próximo para atacar
            {
                //move o personagem
                Vector3 direcao = (_alvoAtual.position - transform.position).normalized;
                transform.position += direcao * _velocidade * Time.deltaTime;
                transform.forward = direcao; //rotaciona o personagem em direção ao seu alvo

                //caso deva usar as animações
                if (SistemaDeBatalha.usarAnimações && _animator != null)
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
        else if (_tipo == TipoDeArma.LONGA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a distância do personagem e seu alvo

            if (distancia > distanciaMinimaParaAtacar) //verifica se deve se aproximar mais para atacar
            {
                //move o personagem
                Vector3 direcao = (_alvoAtual.position - transform.position).normalized;
                transform.position += direcao * _velocidade * Time.deltaTime;
                transform.forward = direcao; //rotaciona o personagem em direção ao seu alvo

                //caso deva usar as animações
                if (SistemaDeBatalha.usarAnimações && _animator != null)
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
            _podeAtacar = false; //personagem não pode atacar
        }

        //checa o tipo de personagem
        if(_tipo == TipoDeArma.CURTA_DISTANCIA)
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
        else if(_tipo == TipoDeArma.LONGA_DISTANCIA)
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

    public void Ataque() //função do ataque em si
    {
        _cooldownAtual = _velocidadeDeAtaque; //reinicia o cooldown

        //caso deva usar as animações
        if (SistemaDeBatalha.usarAnimações && _animator != null)
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
            _hitAtaquePersonagem.usarSFX = _usarSFX;
            
        }
        else
        {
            //se é um personagem com arma de longa distancia, transtorma o hit em um ataque de longa distancia
            if (_tipo == TipoDeArma.LONGA_DISTANCIA)
            {
                string chaveDoPool = personagem.arma.nome; //define o pool que quer buscar pelo nome da arma
                GameObject projetil = _gerenciadorDePool.ObterPool(chaveDoPool);

                if (projetil != null)
                {
                    if (controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR) //se é um personagem do jogador...
                    {
                        if (_tipo == TipoDeArma.LONGA_DISTANCIA) //se é um personagem com uma arma de longa distancia equipada...
                        {
                            //define a posição do projétil para a posição da arma do personagem
                            AtivarArmaPersonagem arma = GetComponentInChildren<AtivarArmaPersonagem>();
                            projetil.transform.position = arma.armas[arma.armaAtual].transform.position;
                        }
                    }
                    else //do contrário...
                    {
                        //posiciona o projétil em uma posição especíifica do personagem
                        Vector3 posicao = transform.position;
                        posicao.y += 1.5f;
                        projetil.transform.position = posicao;
                    }

                    //define o projétil
                    var prefab = _gerenciadorDePool.ObterPrefab(chaveDoPool);
                    if(prefab != null)
                    {
                        projetil.transform.rotation = prefab.transform.rotation;
                    }
                    HitAtaquePersonagem hit = projetil.GetComponent<HitAtaquePersonagem>();
                    if (hit != null)
                    {
                        hit._personagemPai = this;
                        hit.usarSFX = _usarSFX;
                        hit.MoverAteAlvo(_alvoAtual, personagem.arma.velocidadeDoProjetil);
                        hit.poolKey = chaveDoPool;
                        hit.gerenciadorDePool = _gerenciadorDePool;
                    }
                }
            }
        }
    }
    #endregion

    #region Movimento Especial
    private void MovimentoEspecial() //função do movimento especial do personagem
    {
        //retorna caso já esteja executando o movimento especial
        if (executandoMovimentoEspecial)
        {
            return;
        }

        executandoMovimentoEspecial = true;

        if (_animator != null)
        {
            _animator.ResetTrigger("Perseguir");
            _animator.ResetTrigger("Atacar");
            _animator.SetTrigger(movimentoEspecial);
        }
    }

    public void FinalizarMovimentoEspecial() //função que finaliza o movimento especial externamente
    {
        executandoMovimentoEspecial = false;

        if (_animator != null && _usarAnimações)
        {
            _animator.SetTrigger("Idle");
        }
        
        if (_personagemAlvo != null && _personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            VerificarComportamento("perseguir");
        }
        else
        {
            VerificarComportamento("selecionarAlvo");
        }
    }
    #endregion

    #region HP
    public void CausarDano(IAPersonagemBase personagem, int tipoDano) //função de causar dano a um personagem
    {
        float dano = 0;

        //verifica as chances de um dano crítico
        bool critico = false;
        if (UnityEngine.Random.Range(0f, 100f) < chanceCritico)
        {
            critico = true;
        }

        //verifica o tipo de dano (físico ou mágico)
        switch (tipoDano)
        {
            case 0:
                float defesa = Mathf.Clamp(personagem.defesa, 0f, 75f);
                dano = _dano * (1f - defesa / 100f);
                break;
            case 1:
                float defesaMagica = Mathf.Clamp(personagem.defesaMagica, 0f, 75f);
                dano = _dano * (1f - defesaMagica / 100f);
                break;
        }

        if (medo)
        {
            dano -= (dano * reducaoDanoMedo);
        }

        dano = Mathf.Max(0, dano); //garante que o dano nunca seja negativo

        if (critico) //se as chances deram verdadeiras para um dano crítico
        {
            dano *= multiplicadorCritico; //causa dano crítico
        }

        //causa dano ao personagem se ele for um personagem inimigo e é o alvo atual deste personagem
        if (personagem.controlador != this.controlador && personagem == _personagemAlvo)
        {
            personagem.SofrerDano(dano, critico);

            if (efeitoPorDanoCausadoAtivado)
            {
                ExecutarEfeitosDeDanoCausado();
            }

            if (controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
            {
                this.personagem.GanharEXP(dano); //caso seja personagem do jogador ganha pontos de exp igual ao dano causado do inimigo
            }

            //toca o sfx de contato se deve usar os sfxs
            if (_usarSFX)
            {
                _audio.clip = _contatoSFX;
                _audio.Play();
            }
        }
    }

    public void Esquivar() //função de esquiva do personagem
    {
        textoHP.gameObject.SetActive(true);
        textoHP.text = ("Esquivou");
        StartCoroutine(DesativarTextoHP(this));
    }

    public void DesativarTextoHPPersonagem(IAPersonagemBase personagem) //função para chamar a coroutine "DesativarTextoHP"
    {
        StartCoroutine(DesativarTextoHP(personagem));
    }

    private IEnumerator DesativarTextoHP(IAPersonagemBase personagem) //coroutine que desativa o texto do ho do personagem
    {
        yield return new WaitForSeconds(0.75f); //tempo de espera
        personagem.textoHP.gameObject.SetActive(false); //desativa o texto
    }

    public void SofrerDano(float dano, bool critico) //função para sofrer dano
    {
        if(efeitoMarcadorDeAlvo == EfeitoMarcadorDeAlvo.EXPOSTO)
        {
            dano += (dano * 0.2f);
        }
        else if(efeitoMarcadorDeAlvo == EfeitoMarcadorDeAlvo.MARCADO_PARA_EXECUCAO)
        {
            dano = (dano * 3);
        }

        if (escudoAtivado)
        {
            valorEscudo -= dano;
            if(valorEscudo <= 0)
            {
                valorEscudo = 0;
                escudoAtivado = false;
                escudoVfx.SetActive(false);
            }
        }
        else
        {
            hpAtual -= dano; //sofre o dano
        } 

        if (_usarSliders && _slider != null)
        {
            //atualiza o slider e o texto de hp
            _slider.value = hpAtual;
            if (critico && !escudoAtivado)
            {
                textoHP.gameObject.SetActive(true);
                textoHP.text = ("-" + dano + " (Crítico)");
            }
            else
            {
                if (!escudoAtivado)
                {
                    textoHP.gameObject.SetActive(true);
                    textoHP.text = ("-" + dano);
                }
            }
            StartCoroutine(DesativarTextoHP(this));
        }

        if (hpAtual <= 0)
        {
            hpAtual = 0;
            medo = false;
            VerificarComportamento("morrer");
        }
        else
        {
            if(efeitoMarcadorDeAlvo == EfeitoMarcadorDeAlvo.ATORDOADO)
            {
                if (!stunado)
                {
                    tempoDeStun = 0.25f;
                    Stun();
                }
            }
        }
    }

    public void ReceberHP(float cura) //função para receber hp
    {
        if(efeitoMarcadorDeAlvo != EfeitoMarcadorDeAlvo.CORTACURA)
        {
            hpAtual += cura; //recebe hp

            if (hpAtual >= _hpMaximoEInicial)
            {
                hpAtual = _hpMaximoEInicial;
            }

            if (_usarSliders)
            {
                //atualiza o slider
                _slider.value = hpAtual;

                if (hpAtual < _hpMaximoEInicial)
                {
                    textoHP.gameObject.SetActive(true);
                    textoHP.text = ("+" + cura);
                    StartCoroutine(DesativarTextoHP(this));
                }
            }
        }
    }
    #endregion

    #region SP
    public void ReceberSP(float sp) //função que recebe sp
    {
        spAtual += sp; //recebe o sp
        if(spAtual >= _spMaximoEInicial)
        {
            spAtual = _spMaximoEInicial;
        }
        AtualizarFeedbackSP();
    }

    public void GastarSP(float sp) //função que recebe sp
    {
        spAtual -= sp; //recebe o sp
        if (spAtual <= 0)
        {
            spAtual = 0;
        }
        AtualizarFeedbackSP();
    }

    private void AtualizarFeedbackSP()
    {
        if (pontosDeHabilidadeTexto != null)
        {
            pontosDeHabilidadeTexto.text = (spAtual + " / " + _spMaximoEInicial);
        }
    }
    #endregion

    #region Morte
    private void Morrer() //função de morte do personagem
    {
        //para a coroutine de regeneração
        if (regeneracaoCoroutine != null)
        {
            StopCoroutine(regeneracaoCoroutine);
            regeneracaoCoroutine = null;
        }

        //se remove do time em que faz parte
        if (controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
        {
            _sistemaDeBatalha.AtualizarTime(("remover"), ("jogador"), this);
        }
        else if(controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
        {
            _sistemaDeBatalha.AtualizarTime(("remover"), ("inimigo"), this);
        }

        //caso deva usar as animações
        if (SistemaDeBatalha.usarAnimações && _animator != null)
        {
            _animator.ResetTrigger("Perseguir");
            _animator.ResetTrigger("Atacar");
            _animator.ResetTrigger("Stun");
            _animator.SetTrigger("Morrer");
        }

        //desativa o hit caso seja melee, ou o manda para o pool caso seja longa distância
        if (_gerenciadorDePool != null && _tipo == TipoDeArma.LONGA_DISTANCIA)
        {
            if (_hitAtaquePersonagem != null)
            {
                _gerenciadorDePool.DevolverPool(personagem.arma.nome, _hitAtaquePersonagem.gameObject);
            }
        }
        else
        {
            if(_hitAtaquePersonagem != null)
            {
                _hitAtaquePersonagem.gameObject.SetActive(false);
            }
        }  
    }
    #endregion

    #region Nível
    public void AtualizarNivel() //função que atualiza os atributos de batalha do personagem quando sobe de nível
    {
        //atualiza o personagem

        //AtualizarDadosPersonagem();

        if (_usarSliders && _slider != null)
        {
            //atualiza o slider
            _slider.value = hpAtual;
        }
    }
    #endregion

    #region Habilidades

    public void ConjurarHabilidade()
    {
        conjurandoHabilidade = true;
        _animator.SetTrigger("Cast");
        StartCoroutine(EsperarCast());
    }

    public void CancelarHabilidade()
    {
        StopCoroutine(EsperarCast());
        conjurandoHabilidade = false;
        habilidadeSendoConjurada = 0;
        _comportamento = EstadoDoPersonagem.IDLE;
        _animator.ResetTrigger("Cast");
        _animator.SetTrigger("Idle");
        VerificarComportamento("selecionarAlvo");
    }

    IEnumerator EsperarCast()
    {
        yield return new WaitForSeconds(tempoDeCast);
        if (_comportamento != EstadoDoPersonagem.MORTO && _sistemaDeBatalha.batalhaIniciou && _comportamento != EstadoDoPersonagem.IDLE)
        {
            conjurandoHabilidade = false;
            if(habilidadeSendoConjurada == 1)
            {
                //ativa o efeito da habilidade 1
            }
            else if(habilidadeSendoConjurada == 2)
            {
                //ativa o efeito da habilidade 2
            }
            //ativa a habilidade
            _comportamento = EstadoDoPersonagem.IDLE;
            _animator.ResetTrigger("Cast");
            _animator.SetTrigger("Idle");
            VerificarComportamento("selecionarAlvo");
            habilidadeSendoConjurada = 0;
        }
    }

    public void EsperarEfeitoHabilidade(int habilidade, float tempo)
    {
        StartCoroutine(TempoEfeitoHabilidade(habilidade, tempo));
    }
    IEnumerator TempoEfeitoHabilidade(int habilidade, float tempo) //coroutine que espera um tempo para remover o efeito da habilidade
    {
        yield return new WaitForSeconds(tempo);
        if(habilidade == 1)
        {
            habilidadeAtivaClasse.RemoverEfeito(this);
        }
        else if(habilidade == 2)
        {
            habilidadeAtivaArma.RemoverEfeito(this);
        }
    }
    public void EsperarRecargaHabilidade(int habilidade, float tempo)
    {
        StartCoroutine(TempoRecargaHabilidade(habilidade, tempo));
    }
    IEnumerator TempoRecargaHabilidade(int habilidade, float tempo) //coroutine que espera um tempo para recarregar o efeito da habilidade
    {
        yield return new WaitForSeconds(tempo);
        if (habilidade == 1)
        {
            podeAtivarEfeitoHabilidadeAtivaClasse = true;
        }
        else if (habilidade == 2)
        {
            podeAtivarEfeitoHabilidadeAtivaArma = true;
        }
    }

    public void GerenciarVFXHabilidade(int habilidade, bool ativar)
    {
        if(habilidade == 1)
        {
            if(vfxHabilidadeAtivaClasse != null)
            {
                vfxHabilidadeAtivaClasse.SetActive(ativar);
            }
        }
        else if(habilidade == 2)
        {
            if (vfxHabilidadeAtivaArma != null)
            {
                vfxHabilidadeAtivaArma.SetActive(ativar);
            }
        }
        else if (habilidade == 3)
        {
            if (vfxHabilidadePassivaClasse != null)
            {
                vfxHabilidadePassivaClasse.SetActive(ativar);
            }
        }
        else if (habilidade == 4)
        {
            if (vfxHabilidadePassivaArma != null)
            {
                vfxHabilidadePassivaArma.SetActive(ativar);
            }
        }
    }

    public void AtualizarWillPower(int valor, bool valorPositivo)
    {
        if (valorPositivo)
        {
            willPower += valor;
        }
        else
        {
            willPower -= valor;
        }

        if (willPower >= 10)
        {
            willPower = 10;
        }
        else if (willPower <= 0)
        {
            willPower = 0;
        }

        if (valorPositivo)
        {
            aoReceberWillPower?.Invoke(valor);
        }
        else
        {
            aoGastarWillPower?.Invoke(valor);
        }

        if (textoWillPower != null)
        {
            textoWillPower.text = "WillPower: " + willPower;
        }
    }

    public void AtualizarMarcadoresDeAlvo(int valor, bool valorPositivo)
    {
        if (valorPositivo)
        {
            marcadoresDeAlvo += valor;
        }
        else
        {
            marcadoresDeAlvo -= valor;
        }

        if (marcadoresDeAlvo >= 5)
        {
            marcadoresDeAlvo = 5;
        }
        else if (marcadoresDeAlvo <= 0)
        {
            marcadoresDeAlvo = 0;
        }

        switch (marcadoresDeAlvo)
        {
            case 0:
                efeitoMarcadorDeAlvo = EfeitoMarcadorDeAlvo.NENHUM; 
                break;
            case 1:
                efeitoMarcadorDeAlvo = EfeitoMarcadorDeAlvo.EXPOSTO;
                break;
            case 2:
                efeitoMarcadorDeAlvo = EfeitoMarcadorDeAlvo.SANGRAMENTO;
                if (!sangramento)
                {
                    Sangramento(1, 1, 1);
                }
                break;
            case 3:
                efeitoMarcadorDeAlvo = EfeitoMarcadorDeAlvo.ATORDOADO;
                break;
            case 4:
                efeitoMarcadorDeAlvo = EfeitoMarcadorDeAlvo.CORTACURA;
                break;
            case 5:
                efeitoMarcadorDeAlvo = EfeitoMarcadorDeAlvo.MARCADO_PARA_EXECUCAO;
                break;
            default:
                efeitoMarcadorDeAlvo = EfeitoMarcadorDeAlvo.NENHUM;
                break;
        }
    }

    private void AtivacaoHabildiadesInimigo()
    {
        int habilidade = UnityEngine.Random.Range(0, 2);

        if(habilidade == 0 && habilidadeAtivaClasse != null && podeAtivarEfeitoHabilidadeAtivaClasse && _comportamento != EstadoDoPersonagem.MORTO)
        {
            habilidadeAtivaClasse.AtivarEfeito(this);
            StartCoroutine(EsperarAtivacaoHabilidadesInimigo());
        }
        else if (habilidade == 1 && habilidadeAtivaArma != null && podeAtivarEfeitoHabilidadeAtivaClasse && _comportamento != EstadoDoPersonagem.MORTO)
        {
            habilidadeAtivaArma.AtivarEfeito(this);
            StartCoroutine(EsperarAtivacaoHabilidadesInimigo());
        }
        else
        {
            if(_comportamento != EstadoDoPersonagem.MORTO)
            {
                StartCoroutine(EsperarAtivacaoHabilidadesInimigo());
            }
        }
    }

    IEnumerator EsperarAtivacaoHabilidadesInimigo()
    {
        int tempo = UnityEngine.Random.Range(5, 16);
        yield return new WaitForSeconds(tempo);
        if(_comportamento != EstadoDoPersonagem.MORTO)
        {
            AtivacaoHabildiadesInimigo();
        }
    }

    #endregion

    #region Efeitos

    public void AtivarEfeitoPorAtaque(string chave, EfeitoPorAtaque efeito)
    {
        if (efeitosPorAtaque.ContainsKey(chave))
        {
            OnAtaqueComEfeito -= efeitosPorAtaque[chave];
        }

        efeitosPorAtaque[chave] = efeito;
        OnAtaqueComEfeito += efeito;
    }

    public void RemoverEfeitoPorAtaque(string chave)
    {
        if (efeitosPorAtaque.TryGetValue(chave, out var efeito))
        {
            OnAtaqueComEfeito -= efeito;
            efeitosPorAtaque.Remove(chave);
        }
    }

    public void ExecutarEfeitosDeAtaque(bool acerto)
    {
        OnAtaqueComEfeito?.Invoke(acerto);
    }

    public void AtivarEfeitoPorAtaqueRecebido(string chave, EfeitoPorAtaqueRecebido efeito)
    {
        if (efeitosPorAtaqueRecebidos.ContainsKey(chave))
        {
            OnAtaqueRecebidoComEfeito -= efeitosPorAtaqueRecebidos[chave];
        }

        efeitosPorAtaqueRecebidos[chave] = efeito;
        OnAtaqueRecebidoComEfeito += efeito;
    }

    public void RemoverEfeitoPorAtaqueRecebido(string chave)
    {
        if (efeitosPorAtaqueRecebidos.TryGetValue(chave, out var efeito))
        {
            OnAtaqueRecebidoComEfeito -= efeito;
            efeitosPorAtaqueRecebidos.Remove(chave);
        }
    }

    public void ExecutarEfeitosDeAtaqueRecebidos(bool acerto)
    {
        OnAtaqueRecebidoComEfeito?.Invoke(acerto);
    }

    public void AtivarEfeitoPorDanoCausado(string chave, EfeitoPorDanoCausado efeito)
    {
        if (efeitosPorDanosCausados.ContainsKey(chave))
        {
            OnDanoCausadoComEfeito -= efeitosPorDanosCausados[chave];
        }

        efeitosPorDanosCausados[chave] = efeito;
        OnDanoCausadoComEfeito += efeito;
    }

    public void RemoverEfeitoPorDanoCausado(string chave)
    {
        if (efeitosPorDanosCausados.TryGetValue(chave, out var efeito))
        {
            OnDanoCausadoComEfeito -= efeito;
            efeitosPorDanosCausados.Remove(chave);
        }
    }

    public void ExecutarEfeitosDeDanoCausado()
    {
        OnDanoCausadoComEfeito?.Invoke();
    }

    public void Stun()
    {
        if (!imuneAStun)
        {
            stunado = true;
            _animator.SetTrigger("Stun");
            StartCoroutine(EsperarTempoStun());
        }
    }

    IEnumerator EsperarTempoStun()
    {
        yield return new WaitForSeconds(tempoDeStun);
        if (_comportamento != EstadoDoPersonagem.MORTO && _sistemaDeBatalha.batalhaIniciou && _comportamento != EstadoDoPersonagem.IDLE)
        {
            stunado = false;
            _comportamento = EstadoDoPersonagem.IDLE;
            _animator.ResetTrigger("Stun");
            _animator.SetTrigger("Idle");
            VerificarComportamento("selecionarAlvo");
        }
    }

    public void Sangramento(float dano, float intervalo, float duracao)
    {
        sangramento = true;
        _vfxSangramento.SetActive(true);
        StartCoroutine(EsperarTempoSangramento(dano, intervalo, duracao));
    }

    public IEnumerator EsperarTempoSangramento(float danoPorTick, float intervalo, float duracaoTotal)
    {
        float tempoDecorrido = 0f;

        while (tempoDecorrido < duracaoTotal)
        {
            if (_comportamento == EstadoDoPersonagem.MORTO)
            {
                _vfxSangramento.SetActive(false);
                yield break;
            }

            SofrerDano(danoPorTick, false);

            yield return new WaitForSeconds(intervalo);
            tempoDecorrido += intervalo;
        }

        sangramento = false;
        _vfxSangramento.SetActive(false);

        if(efeitoMarcadorDeAlvo == EfeitoMarcadorDeAlvo.SANGRAMENTO)
        {
            Sangramento(1, 1, 1);
        }
    }

    public bool VerificarEfeitoNegativo()
    {
        if (ataqueDiminuido || sangramento || medo)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region Feedbacks Visuais
    private void FeedbacksVisuais() //função para verificar feedbacks visuais
    {
        _usarAnimações = SistemaDeBatalha.usarAnimações; //verifica se deve usar animações referente o sistema de batalha
        _usarSliders = SistemaDeBatalha.usarSliders; //verifica se deve usar sliders de hp referente ao sistema de batalha
        _usarSFX = SistemaDeBatalha.usarSfxs; //verifica se deve usar sfxs referente ao sistema de batalha

        if (_usarAnimações)
        {
            //encontra o animator no objeto
            if (controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
            {
                if (personagemVisual.Length != 0)
                {
                    if (personagemVisual[id].GetComponent<Animator>() != null)
                    {
                        _animator = personagemVisual[id].GetComponent<Animator>();
                    }
                    else
                    {
                        Debug.Log("Não há animator");
                    }
                }
            }
            else if(controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                _animator = GetComponent<Animator>();
            }
            
        }

        //encontra o slider do hp no objeto
        if (_usarSliders)
        {
            if (_slider == null)
            {
                Debug.Log("Não há slider");
            }
            else
            {
                //referencia o hp ao slider
                _slider.gameObject.SetActive(true);
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

    public void DefinirAnimacao(string animacao)
    {
        if(_animator != null && _usarAnimações)
        {
            _animator.SetTrigger(animacao);
        }
    }
    #endregion
}
