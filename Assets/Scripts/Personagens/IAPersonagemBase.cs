using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ControladorDoPersonagem { PERSONAGEM_DO_JOGADOR, PERSONAGEM_INIMIGO } //quem controla o personagem, se � controlado pelo jogador ou pela IA inimiga
public enum TipoDeArma { CURTA_DISTANCIA, LONGA_DISTANCIA } //caracter�sticas referente ao comportamento de ataque da arma do personagem, se � um ataque de curta ou longa dist�ncia
public enum EstadoDoPersonagem { IDLE, PERSEGUINDO, ATACANDO, MORTO, MOVIMENTO_ESPECIAL} //estados de comportamento do personagem

public class IAPersonagemBase : MonoBehaviour
{
    //�rea referente aos dados e visual do personagem selecionado
    [Header("Personagem Selecionado")]
    //[HideInInspector]
    public PersonagemData personagem; //dados do personagem criado
    public GameObject[] personagemVisual; //visual do personagem
    public GameObject canvas; //canvas do personagem

    //�rea referente �s defini��es do personagem
    [Header("Defini��es")]
    public ControladorDoPersonagem controlador;
    [HideInInspector]
    public TipoDeArma _tipo;
    [HideInInspector]
    public EstadoDoPersonagem _comportamento;

    //�rea referente ao hp (vida) do personagem
    [Header("HP")]
    [SerializeField]
    private float _hpBaseGuerreiro = 1; //valor base do HP da classe guerreiro
    [SerializeField]
    private float _hpBaseLadino = 1; //valor base do HP da classe ladino
    [SerializeField]
    private float _hpBaseElementalista = 1; //valor base do HP da classe elementalista
    [SerializeField]
    private float _hpBaseSacerdote = 1; //valor base do HP da classe sacerdote
    [HideInInspector]
    public float _hpMaximoEInicial; //valor inicial que o hp atual do player ter� ao iniciar a batalha, e valor m�ximo que ele pode ter
    [HideInInspector]
    public float hpAtual; //valor atual do hp (vida) do personagem
    [HideInInspector]
    public float hpRegeneracao; //valor por segundo que o personagem recuperar� de hp

    //�rea referente ao sp (pontos de habilidade) do personagem
    [Header("SP")]
    [SerializeField]
    private float _spBaseGuerreiro; //valor base do SP da classe guerreiro
    [SerializeField]
    private float _spBaseLadino; //valor base do SP da classe ladino
    [SerializeField]
    private float _spBaseElementalista; //valor base do SP da classe elementalista
    [SerializeField]
    private float _spBaseSacerdote; //valor base do SP da classe sacerdote
    [HideInInspector]
    public float _spMaximoEInicial; //valor inicial que o SP atual do player ter� ao iniciar a batalha, e valor m�ximo que ele pode ter
    [HideInInspector]
    public float spAtual; //valor atual do SP (pontos de habilidade) do personagem
    [HideInInspector]
    public float spRegeneracao; //valor por segundo que o personagem recuperar� de sp

    private Coroutine regeneracaoCoroutine; //coroutine de regenera��o de hp e sp

    //�rea referente ao fator classe do personagem
    [Header("Fator Classe")]
    [SerializeField]
    private float _fatorClasseGuerreiro = 1; //fator da classe guerreiro
    [SerializeField]
    private float _fatorClasseLadino = 1; //fator da classe ladino
    [SerializeField]
    private float _fatorClasseElementalista = 1; //fator da classe elementalista
    [SerializeField]
    private float _fatorClasseSacerdote = 1; //fator da classe sacerdote

    //�rea referente ao ataque do personagem
    [Header("Ataque")]
    public float precisaoBase; //precis�o base do personagem
    [HideInInspector]
    public float precisao; //precis�o do personagem
    [HideInInspector]
    public float _dano; //valor do dano do ataque do personagem
    public float velocidadeDeAtaqueBase = 1f; //valor base do tempo de espera para cada ataque b�sico do personagem
    [HideInInspector]
    public float _velocidadeDeAtaque; //valor da velocidade de ataque do personagem
    private float _cooldownAtual = 0f; //tempo atual para o personagem poder atacar novamente
    public float esquivaBase; //valor base da esquiva do persongem
    [HideInInspector]
    public float esquiva; //valor da esquiva do personagem
    private bool _podeAtacar; //vari�vel que verifica se o personagem pode atacar
    public float chanceCritico; //valor em porcentagem das chances de cr�tico
    public float multiplicadorCritico = 2; //valor do multiplicador do cr�tico

    //�rea referente �s defini��es do personagem de longa distancia
    [Header("Defini��es Longa Dist�ncia")]
    public float distanciaMinimaParaAtacar = 5f; //distancia minima que um personagem de longa distancia deve ter para atacar

    //�rea referente ao movimento do personagem
    [Header("Movimento")]
    public float _velocidade = 1; //valor da velocidade de movimento do personagem

    //�rea referente � defesa do personagem
    [Header("Defesa")]
    public float defesa; //defesa do personagem
    public float defesaMagica; //defesa m�gica do personagem

    //�rea referente � habilidades
    [Header("Habilidades")]
    [HideInInspector]
    public HabilidadeBase habilidade1;
    [HideInInspector]
    public HabilidadeBase habilidade2;

    //�rea referente �s anima��es
    [Header("Anima��o")]
    public RuntimeAnimatorController[] controllerAnimatorArma; //controller animator referente � arma do personagem

    //�rea referente aos sfx
    [Header("SFX")]
    public AudioSource _audio;
    [SerializeField]
    private AudioClip _contatoSFX;
    [SerializeField]
    private AudioClip _projetilSFX;
    [SerializeField]
    public AudioClip _habilidadeSFX;

    [HideInInspector]
    public int id; //vari�vel para verificar o id do personagem
    [HideInInspector]
    public IAPersonagemBase _personagemAlvo; //alvo de ataques do personagem
    private Transform _alvoAtual; //transform do personagem alvo
    private SistemaDeBatalha _sistemaDeBatalha; //sistema de batalha
    private HitAtaquePersonagem _hitAtaquePersonagem; //hit do personagem
    private SkinnedMeshRenderer _malha; //malha do personagem
    [HideInInspector] 
    public Vector3 posicaoInicial; //posi��o inicial do personagem
    [HideInInspector]
    public Quaternion rotacaoInicial; //rota��o inicial do personagem
    [HideInInspector]
    public int movimentoEspecialAtual; //identifica��o do movimento especial do personagem
    [HideInInspector]
    public bool executandoMovimentoEspecial; //vari�vel para verificar se o personagem est� executando o movimento especial
    [HideInInspector]
    public bool imuneADanos; //vari�vel que verifica se o personagem � imune a danos
    //fun��o que � ativada quando h� um efeito por ataque
    public delegate void EfeitoPorAtaque();
    public EfeitoPorAtaque efeitoPorAtaque;
    [HideInInspector]
    public bool efeitoPorAtaqueAtivado; //verifica se efeitos por ataque de habilidades est�o ativados
    //fun��o que � ativada quando h� um efeito por esquiva
    public delegate void EfeitoPorEsquiva();
    public EfeitoPorAtaque efeitoPorEsquiva;
    [HideInInspector]
    public bool efeitoPorEsquivaAtivado; //verifica se efeitos por esquiva de habilidades est�o ativados
    //fun��o que � ativada quando h� um efeito por dano
    public delegate void EfeitoPorDano();
    public EfeitoPorDano efeitoPorDano;
    [HideInInspector]
    public bool efeitoPorDanoAtivado; //verifica se efeitos por dano de habilidades est�o ativados
    [HideInInspector]
    public bool sangramento; //efeito de sangramento
    [HideInInspector]
    public float danoSangramento; //dano do efeito de sangramento
    [HideInInspector]
    public bool queimadura; //efeito de queimadura
    [HideInInspector]
    public float danoQueimadura; //dano do efeito de sangramento
    [HideInInspector]
    public bool congelamento; //efeito de congelamento
    [HideInInspector]
    public bool envenenamento; //efeito de envenenamento
    public Text pontosDeHabilidadeTexto; //texto referente aos pontos de habilidade do personagem

    private int STR; //atributo for�a do personagem
    private int AGI; //atributo agilidade do personagem
    private int DEX; //atributo destreza do personagem
    private int INT; //atributo intelig�ncia do personagem
    private int VIT; //atributo constitui��o do personagem
    private int LUK; //atributo sabedoria/sorte do personagem
    private int LVL; //n�vel do personagem

    //�rea de feedback visuais
    private Animator _animator; //animator do personagem
    private Slider _slider; //slider do hp do personagem
    public Text textoHP; //texto mostrando a atualiza��o do hp do personagem
    private bool _usarAnima��es; //vari�vel para verificar se deve usar as anima��es
    private bool _usarSliders; //vari�vel para verificar se deve usar os sliders
    private bool _usarSFX; //vari�vel para verificar se deve usar os sfxs

    private void Start()
    {
        _sistemaDeBatalha = GameObject.FindGameObjectWithTag("SistemaDeBatalha").GetComponent<SistemaDeBatalha>(); //encontra o sistema de batalha na cena

        if (SistemaDeBatalha.usarSliders)
        {
            if (controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                _slider = GetComponentInChildren<Slider>();
            }
        }

        if (SistemaDeBatalha.usarAnima��es)
        {
            if (controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                _malha = GetComponentInChildren<SkinnedMeshRenderer>();
            }
        }
    }

    private void OnEnable()
    {
        _sistemaDeBatalha = GameObject.FindGameObjectWithTag("SistemaDeBatalha").GetComponent<SistemaDeBatalha>(); //encontra o sistema de batalha na cena

        if (SistemaDeBatalha.usarSliders)
        {
            if (controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                _slider = GetComponentInChildren<Slider>();
            }
        }

        if (SistemaDeBatalha.usarAnima��es)
        {
            if (controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                _malha = GetComponentInChildren<SkinnedMeshRenderer>();
            }
        }
    }

    public void ResetarDadosPersonagem() //fun��o que reseta os dados visuais do personagem selecionado
    {
        personagem.funcaoSubirNivel -= AtualizarNivel;

        //reseta os dados do personagem
        for (int i = 0; i < personagemVisual.Length; i++)
        {
            personagemVisual[i].gameObject.SetActive(false);
        }

        canvas.SetActive(false);
    }

    public void ReceberDadosPersonagem() //fun��o que atualiza os dados do personagem selecionado
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

        habilidade1 = personagem.habilidadeClasse;
        habilidade2 = personagem.habilidadeArma;

        if(habilidade1 != null)
        {
            habilidade1.personagem = this;
            habilidade1.Inicializar();
        }
        
        if(habilidade2 != null)
        {
            habilidade2.personagem = this;
            habilidade2.Inicializar();
        }

        //encontra o slider do personagem caso tenha
        if (transform.GetComponentInChildren<Slider>() != null)
        {
            _slider = transform.GetComponentInChildren<Slider>();
        }

        //encontra a malha do personagem caso tenha
        if (personagemVisual[id].GetComponentInChildren<SkinnedMeshRenderer>() != null)
        {
            _malha = personagemVisual[id].GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }

    public void AtualizarDadosPersonagem() //fun��o que atualiza os dados do personagem
    {
        STR = personagem.forca;
        AGI = personagem.agilidade;
        DEX = personagem.destreza;
        INT = personagem.inteligencia;
        VIT = personagem.constituicao;
        LUK = personagem.sabedoria;
        LVL = personagem.nivel;

        //b�nus a cada 10 pontos
        int bonusSTR = Mathf.FloorToInt(STR / 10f);
        int bonusAGI = Mathf.FloorToInt(AGI / 10f);
        int bonusDEX = Mathf.FloorToInt(DEX / 10f);
        int bonusVIT = Mathf.FloorToInt(VIT / 10f);
        int bonusINT = Mathf.FloorToInt(INT / 10f);
        int bonusLUK = Mathf.FloorToInt(LUK / 10f);

        //atualiza os dados do personagem
        switch (personagem.classe)
        {
            case Classe.Guerreiro:
                _hpMaximoEInicial = (((_hpBaseGuerreiro + VIT + STR + LVL) * 20) * (_fatorClasseGuerreiro * 4));
                _spMaximoEInicial = ((_spBaseGuerreiro + (INT * 10) + (DEX * 3)) * LVL * (_fatorClasseGuerreiro / 4));
                break;
            case Classe.Ladino:
                _hpMaximoEInicial = (((_hpBaseLadino + VIT + STR + LVL) * 20) * (_fatorClasseLadino * 3));
                _spMaximoEInicial = ((_spBaseLadino + (INT * 10) + (DEX * 3)) * LVL * (_fatorClasseLadino / 2));
                break;
            case Classe.Elementalista:
                _hpMaximoEInicial = (((_hpBaseElementalista + VIT + STR + LVL) * 20) * (_fatorClasseElementalista * 2));
                _spMaximoEInicial = ((_spBaseElementalista + (INT * 10) + (DEX * 3)) * LVL * (_fatorClasseElementalista / 2));
                break;
            case Classe.Sacerdote:
                _hpMaximoEInicial = (((_hpBaseSacerdote + VIT + STR + LVL) * 20) * (_fatorClasseSacerdote * 2));
                _spMaximoEInicial = ((_spBaseSacerdote + (INT * 10) + (DEX * 3)) * LVL * (_fatorClasseSacerdote / 1));
                break;
        }
        _hpMaximoEInicial += (_hpMaximoEInicial * 0.01f * bonusVIT); //efeito de b�nus do atributo constitui��o
        hpRegeneracao = (VIT * 0.5f);
        spRegeneracao = (INT * 0.5f);

        _tipo = personagem.arma.armaTipo;

        switch (personagem.arma.armaDano)
        {
            case TipoDeDano.DANO_MELEE:
                _dano = personagem.arma.dano + (STR * 3) + (DEX * 1);
                _dano += (bonusSTR * 6); //efeito de b�nus do atributo for�a
                break;
            case TipoDeDano.DANO_RANGED:
                _dano = personagem.arma.dano + (DEX * 3) + (STR * 1);
                _dano += (bonusDEX * 3); //efeito de b�nus do atributo destreza
                break;
            case TipoDeDano.DANO_MAGICO:
                _dano = personagem.arma.dano + (INT * 4) + (DEX * 1);
                _dano += (bonusINT * 4); //efeito de b�nus do atributo intelig�ncia
                break;
        }

        precisao = (precisaoBase + (DEX * 2) + (LUK * 0.5f));
        precisao += (bonusDEX * 10); //efeito de b�nus do atributo destreza

        float aspd = velocidadeDeAtaqueBase + (AGI * 0.3f) + (DEX * 0.1f);
        aspd += (bonusAGI * 0.3f); //efeito de b�nus do atributo agilidade
        _velocidadeDeAtaque = Mathf.Clamp(2f - (aspd * 0.02f), 0.2f, 2f);

        esquiva = (esquivaBase + (AGI * 2) + (LUK * 0.5f));
        esquiva += (bonusAGI * 10); //efeito de b�nus do atributo agilidade

        defesa = (VIT * 0.5f);
        defesa += defesa * (bonusVIT * 0.01f); //efeito de b�nus do atributo constitui��o
        defesaMagica = (INT * 0.5f);
        defesaMagica += defesaMagica * (bonusVIT * 0.01f); //efeito de b�nus do atributo constitui��o

        chanceCritico = (LUK * 0.5f);
        chanceCritico += (bonusLUK * 2); //efeito de b�nus do atributo sabedoria
    }
    public void IniciarBatalha() //fun��o chamada ao inicar a batalha e define os valores e comportamentos iniciais do personagem
    {
        if(habilidade1 != null)
        {
            habilidade1.podeAtivarEfeito = true;
        }

        if(habilidade2 != null)
        {
            habilidade2.podeAtivarEfeito = true;
        }

        _hitAtaquePersonagem = transform.GetComponentInChildren<HitAtaquePersonagem>(true); //encontra o hit do personagem dentro de si
        hpAtual = _hpMaximoEInicial; //define o hp atual do personagem igual ao valor m�ximo e inicial

        sangramento = false;
        queimadura = false;
        congelamento = false;

        FeedbacksVisuais(); //chama a fun��o para verificar quais feedbacks visuais ir� usar
        SelecionarAlvo(); //chama a fun��o para o personagem encontrar seu alvo

        //inicia a coroutine de regenera��o
        if(regeneracaoCoroutine == null)
        {
            regeneracaoCoroutine = StartCoroutine(RegeneracaoLoop());
        }
            
    }

    public void VerificarComportamento(string comportamento) //fun��o que verifica qual deve ser o comportamento do personagem
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
        else if(comportamento == "movimentoEspecial")
        {
            _comportamento = EstadoDoPersonagem.MOVIMENTO_ESPECIAL;
            MovimentoEspecial(movimentoEspecialAtual);
        }
        else if(comportamento == "paralisia")
        {
            _comportamento = EstadoDoPersonagem.IDLE;
            Paralisia();
        }
    }

    public void ResetarEstado() //fun��es para resetar os estados do personagem
    {
        if(controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR) //verifica se � personagem do jogador
        {
            transform.position = posicaoInicial; //restaura a posi��o
            transform.rotation = rotacaoInicial; //restaura a rota��o
        }
        hpAtual = _hpMaximoEInicial; //restaura hp
        _cooldownAtual = 0; //restaura cooldown
        _comportamento = EstadoDoPersonagem.IDLE;
        FeedbacksVisuais();
        if (_usarAnima��es)
        {
            _malha.gameObject.SetActive(true);
            _animator.Rebind();
        }
        else
        {
            _malha.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        //checa se o personagem n�o est� morto, se a batalha iniciou e se o personagem n�o est� em idle
        if(_comportamento != EstadoDoPersonagem.MORTO && _sistemaDeBatalha.batalhaIniciou && _comportamento != EstadoDoPersonagem.IDLE)
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
            else if (_comportamento == EstadoDoPersonagem.MOVIMENTO_ESPECIAL)
            {
                MovimentoEspecial(movimentoEspecialAtual);
            }

            if (_personagemAlvo != null && _personagemAlvo._comportamento == EstadoDoPersonagem.MORTO)
            {
                VerificarComportamento("selecionarAlvo");
            }
        }
    }

    private IEnumerator RegeneracaoLoop() //loop de regenera~��o de HP e SP por segundo
    {
        while (_comportamento != EstadoDoPersonagem.MORTO && _sistemaDeBatalha.batalhaIniciou)
        {
            ReceberHP(hpRegeneracao);
            ReceberSP(spRegeneracao);

            yield return new WaitForSeconds(1f);
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
        if (_tipo == TipoDeArma.CURTA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a dist�ncia do personagem e seu alvo
            if (distancia > 2f) //verifica se est� pr�ximo para atacar
            {
                //move o personagem
                Vector3 direcao = (_alvoAtual.position - transform.position).normalized;
                transform.position += direcao * _velocidade * Time.deltaTime;
                transform.forward = direcao; //rotaciona o personagem em dire��o ao seu alvo

                //caso deva usar as anima��es
                if (SistemaDeBatalha.usarAnima��es && _animator != null)
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
        else if (_tipo == TipoDeArma.LONGA_DISTANCIA)
        {
            float distancia = Vector3.Distance(transform.position, _alvoAtual.position); //define a dist�ncia do personagem e seu alvo

            if (distancia > distanciaMinimaParaAtacar) //verifica se deve se aproximar mais para atacar
            {
                //move o personagem
                Vector3 direcao = (_alvoAtual.position - transform.position).normalized;
                transform.position += direcao * _velocidade * Time.deltaTime;
                transform.forward = direcao; //rotaciona o personagem em dire��o ao seu alvo

                //caso deva usar as anima��es
                if (SistemaDeBatalha.usarAnima��es && _animator != null)
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
        if(_tipo == TipoDeArma.CURTA_DISTANCIA)
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
        else if(_tipo == TipoDeArma.LONGA_DISTANCIA)
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
        _cooldownAtual = _velocidadeDeAtaque; //reinicia o cooldown

        //caso deva usar as anima��es
        if (SistemaDeBatalha.usarAnima��es && _animator != null)
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
            if(_tipo == TipoDeArma.LONGA_DISTANCIA)
            {
                _hitAtaquePersonagem.ResetarPosi��o(); //reseta a posi��o do hit
                _hitAtaquePersonagem.MoverAteAlvo(_alvoAtual, personagem.arma.velocidadeDoProjetil); //faz com que ele se mova at� o alvo

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

    #region Movimento Especial
    private void MovimentoEspecial(int movimento) //fun��o do movimento especial do personagem
    {
        //retorna caso j� esteja executando o movimento especial
        if(executandoMovimentoEspecial)
        {
            return;
        }

        executandoMovimentoEspecial = true;

        if (movimento == 1)
        {
            if(SistemaDeBatalha.usarAnima��es && _animator != null)
            {
                _animator.ResetTrigger("Perseguir");
                _animator.ResetTrigger("Atacar");
                _animator.SetTrigger("Combo");
            }
            else
            {
                StartCoroutine(TempoMovimentoEspecial(movimento));
            }
        }
        else if(movimento == 2)
        {
            if (SistemaDeBatalha.usarAnima��es && _animator != null)
            {
                _animator.ResetTrigger("Perseguir");
                _animator.ResetTrigger("Atacar");
                _animator.SetTrigger("Defender");
            }
        }
    }

    public void FinalizarMovimentoEspecial() //fun��o que finaliza o movimento especial externamente
    {
        executandoMovimentoEspecial = false;

        if(movimentoEspecialAtual == 1)
        {
            if(habilidade1 != null)
            {
                habilidade1.RemoverEfeito();
            }
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

    IEnumerator TempoMovimentoEspecial(int movimento) //fun��o de movimento especial que utiliza tempo
    {
        if(movimento == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                _hitAtaquePersonagem.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                _hitAtaquePersonagem.gameObject.SetActive(false);
            }
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
    public void CausarDano(IAPersonagemBase personagem, int tipoDano) //fun��o de causar dano a um personagem
    {
        float dano = 0;

        //verifica as chances de um dano cr�tico
        bool critico = false;
        if (Random.Range(0f, 100f) < chanceCritico)
        {
            critico = true;
        }

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

        dano = Mathf.Max(0, dano); //garante que o dano nunca seja negativo

        if (critico)
        {
            dano *= multiplicadorCritico; //causa dano cr�tico
        }

        //causa dano ao personagem se ele for um personagem inimigo e � o alvo atual deste personagem
        if (personagem.controlador != this.controlador && personagem == _personagemAlvo)
        {
            personagem.SofrerDano(dano);

            if(controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
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

    public void Esquivar() //fun��o de esquiva do personagem
    {
        textoHP.gameObject.SetActive(true);
        textoHP.text = ("Esquivou");
        StartCoroutine(DesativarTextoHP(this));
        StartCoroutine(TempoDeEsquiva());
    }

    IEnumerator TempoDeEsquiva()
    {
        _malha.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        _malha.gameObject.SetActive(true);
    }

    public void DesativarTextoHPPersonagem(IAPersonagemBase personagem)
    {
        StartCoroutine(DesativarTextoHP(personagem));
    }

    private IEnumerator DesativarTextoHP(IAPersonagemBase personagem) //coroutine que desativa o texto do ho do personagem
    {
        yield return new WaitForSeconds(1);
        personagem.textoHP.gameObject.SetActive(false);
    }

    public void SofrerDano(float dano) //fun��o para sofrer dano
    {
        if (!imuneADanos)
        {
            hpAtual -= dano; //sofre o dano

            if (_usarSliders && _slider != null)
            {
                //atualiza o slider
                _slider.value = hpAtual;
                textoHP.gameObject.SetActive(true);
                textoHP.text = ("-" + dano);
                StartCoroutine(DesativarTextoHP(this));
            }

            if (efeitoPorDanoAtivado)
            {
                efeitoPorDano();
            }

            if (hpAtual <= 0)
            {
                hpAtual = 0;
                VerificarComportamento("morrer");
            }
        }
    }

    public void ReceberHP(float cura) //fun��o para receber hp
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
            textoHP.gameObject.SetActive(true);
            textoHP.text = ("+" + cura);
            StartCoroutine(DesativarTextoHP(this));
        }
    }
    #endregion

    #region SP
    public void ReceberSP(float sp)
    {
        spAtual += sp;
        if(spAtual >= _spMaximoEInicial)
        {
            spAtual = _spMaximoEInicial;
        }
    }
    #endregion

    #region Morte
    private void Morrer() //fun��o de morte do personagem
    {
        //para a coroutine de regenera��o
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

        //caso deva usar as anima��es
        if (SistemaDeBatalha.usarAnima��es && _animator != null)
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

    #region N�vel
    public void AtualizarNivel() //fun��o que atualiza os atributos de batalha do personagem quando sobe de n�vel
    {
        AtualizarDadosPersonagem();

        if (habilidade1 != null)
        {
            habilidade1.personagem = this;
            habilidade1.Inicializar();
        }

        if (habilidade2 != null)
        {
            habilidade2.personagem = this;
            habilidade2.Inicializar();
        }

        if (_usarSliders && _slider != null)
        {
            //atualiza o slider
            _slider.value = hpAtual;
        }
    }
    #endregion

    #region Habilidades

    public void EsperarEfeitoHabilidade(HabilidadeBase habilidade, float tempo)
    {
        if(habilidade.tempoDeEfeito != 0)
        {
            StartCoroutine(TempoEfeitoHabilidade(habilidade, tempo));
        }
    }
    IEnumerator TempoEfeitoHabilidade(HabilidadeBase habilidade, float tempo) //coroutine que espera um tempo para remover o efeito da habilidade
    {
        yield return new WaitForSeconds(tempo);
        habilidade.RemoverEfeito();
    }
    public void EsperarRecargaHabilidade(HabilidadeBase habilidade, float tempo)
    {
        StartCoroutine(TempoRecargaHabilidade(habilidade, tempo));
    }
    IEnumerator TempoRecargaHabilidade(HabilidadeBase habilidade, float tempo) //coroutine que espera um tempo para recarregar o efeito da habilidade
    {
        yield return new WaitForSeconds(tempo);
        habilidade.podeAtivarEfeito = true;
        Debug.Log("Pode Ativar Efeito");
    }
    #endregion

    #region Efeitos
    public void Paralisia() //fun��o de paralisia do personagem
    {
        _alvoAtual = null;
        _personagemAlvo = null;
        if (_usarAnima��es)
        {
            _animator.Rebind();
        }
    }

    public void Sangramento() //fun��o de sangramento do personagem
    {
        if (sangramento)
        {
            StartCoroutine(DanoSangramento());
        }
    }

    IEnumerator DanoSangramento()
    {
        if(_comportamento != EstadoDoPersonagem.MORTO)
        {
            SofrerDano(danoSangramento);
        }
        yield return new WaitForSeconds(1);
        if (sangramento)
        {
            Sangramento();
        }
    }

    public void Queimadura() //fun��o de queimadura do personagem
    {
        if (queimadura)
        {
            StartCoroutine(DanoQueimadura());
        }
    }

    IEnumerator DanoQueimadura()
    {
        if (_comportamento != EstadoDoPersonagem.MORTO)
        {
            SofrerDano(danoQueimadura);
        }
        yield return new WaitForSeconds(1);
        if (queimadura)
        {
            Queimadura();
        }
    }
    #endregion

    #region Feedbacks Visuais
    private void FeedbacksVisuais() //fun��o para verificar feedbacks visuais
    {
        _usarAnima��es = SistemaDeBatalha.usarAnima��es; //verifica se deve usar anima��es referente o sistema de batalha
        _usarSliders = SistemaDeBatalha.usarSliders; //verifica se deve usar sliders de hp referente ao sistema de batalha
        _usarSFX = SistemaDeBatalha.usarSfxs; //verifica se deve usar sfxs referente ao sistema de batalha

        if (_usarAnima��es)
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
                        Debug.Log("N�o h� animator");
                    }
                }
            }
            else if(controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                _animator = GetComponent<Animator>();
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
    #endregion
}
