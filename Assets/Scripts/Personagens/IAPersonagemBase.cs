using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ControladorDoPersonagem { PERSONAGEM_DO_JOGADOR, PERSONAGEM_INIMIGO } //quem controla o personagem, se é controlado pelo jogador ou pela IA inimiga
public enum TipoDePersonagem { CURTA_DISTANCIA, LONGA_DISTANCIA } //características referente ao comportamento de ataque personagem, se é um ataque de curta ou longa distância
public enum EstadoDoPersonagem { IDLE, PERSEGUINDO, ATACANDO, MORTO, MOVIMENTO_ESPECIAL } //estados de comportamento do personagem

public class IAPersonagemBase : MonoBehaviour
{
    [Header("Personagem Selecionado")]
    public PersonagemData personagem; //dados do personagem criado
    public GameObject[] personagemVisual; //visual do personagem
    public GameObject canvas; //canvas do personagem

    //área referente às definições do personagem
    [Header("Definições")]
    public ControladorDoPersonagem controlador;
    public TipoDePersonagem _tipo;
    [HideInInspector]
    public EstadoDoPersonagem _comportamento;

    //área referente às definições do personagem de longa distancia
    [Header("Definições Longa Distancia")]
    [SerializeField]
    private float distanciaMinimaParaAtacar = 5f; //distancia minima que um personagem de longa distancia deve ter para atacar
    [SerializeField]
    private float velocidadeDoProjetil; //velocidade do projetil do ataque do personagem de longa distancia

    //área referente ao id do personagem
    [Header("ID")]
    public int id; //variável para verificar o id do personagem

    //área referente aos equipamentos do personagem
    [Header("Equipamentos")]
    public int numeroDeEquipamentos; //número de equipamentos do personagem (apenas visualmente demonstrativo)
    public Sprite[] spriteEquipamentos; //sprites dos equipamentos do personagem 
    

    //área referente ao hp (vida) do personagem
    [Header("HP")]
    public float _hpMaximoEInicial = 100f; //valor inicial que o hp atual do player terá ao iniciar a batalha, e valor máximo que ele pode ter
    //[HideInInspector]
    public float hpAtual = 100f; //valor atual do hp (vida) do personagem

    //área referente ao movimento do personagem
    [Header("Movimento")]
    public float _velocidade = 2f; //valor do ataque básico do personagem

    //área referente ao ataque do personagem
    [Header("Ataque")]
    public float _danoAtaqueBasico = 10f; //valor do dano do ataque básico do personagem
    public float _cooldown = 1f; //valor do tempo de espera para cada ataque básico do personagem
    private float _cooldownAtual = 0f; //tempo atual para o personagem poder atacar novamente
    private bool _podeAtacar; //variável que verifica se o personagem pode atacar

    //área referente à habilidades
    [Header("Habilidades")]
    public HabilidadesBase habilidade1;
    public HabilidadesBase habilidade2;

    //Área referente aos sfx
    [Header("SFX")]
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _contatoSFX;
    [SerializeField]
    private AudioClip _projetilSFX;

    [HideInInspector]
    public IAPersonagemBase _personagemAlvo; //alvo de ataques do personagem
    private Transform _alvoAtual; //transform do personagem alvo
    private SistemaDeBatalha _sistemaDeBatalha; //sistema de batalha
    private HitAtaquePersonagem _hitAtaquePersonagem; //hit do personagem
    private SkinnedMeshRenderer _malha; //malha do personagem
    [HideInInspector] 
    public Vector3 posicaoInicial; //posição inicial do personagem
    [HideInInspector]
    public Quaternion rotacaoInicial; //rotação inicial do personagem
    [HideInInspector]
    public int movimentoEspecialAtual; //identificação do movimento especial do personagem
    [HideInInspector]
    public bool executandoMovimentoEspecial; //variável para verificar se o personagem está executando o movimento especial
    [HideInInspector]
    public bool imuneADanos; //variável que verifica se o personagem é imune a danos
    [HideInInspector]
    public int reducaoDeDano = 0; //valor da redução de dano do personagem


    //Área de feedback visuais
    private Animator _animator; //animator do personagem
    private Slider _slider; //slider do hp do personagem
    private bool _usarAnimações; //variável para verificar se deve usar as animações
    private bool _usarSliders; //variável para verificar se deve usar os sliders
    private bool _usarSFX; //variável para verificar se deve usar os sfxs

    private void Start()
    {
        _sistemaDeBatalha = GameObject.FindGameObjectWithTag("SistemaDeBatalha").GetComponent<SistemaDeBatalha>(); //encontra o sistema de batalha na cena

        //encontra o slider do personagem caso tenha
        if (gameObject.GetComponentInChildren<Slider>() != null)
        {
            _slider = GetComponentInChildren<Slider>();
        }

        //encontra a malha do personagem caso tenha
        if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>() != null)
        {
            _malha = GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }

    private void OnEnable()
    {
        _sistemaDeBatalha = GameObject.FindGameObjectWithTag("SistemaDeBatalha").GetComponent<SistemaDeBatalha>(); //encontra o sistema de batalha na cena

        //encontra o slider do personagem caso tenha
        if (gameObject.GetComponentInChildren<Slider>() != null)
        {
            _slider = GetComponentInChildren<Slider>();
        }

        //encontra a malha do personagem caso tenha
        if (gameObject.GetComponentInChildren<SkinnedMeshRenderer>() != null)
        {
            _malha = GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }

    public void ResetarDadosPersonagem() //função que reseta os dados do personagem selecionado
    {
        //reseta os dados do personagem
        for (int i = 0; i < personagemVisual.Length; i++)
        {
            personagemVisual[i].gameObject.SetActive(false);
        }

        canvas.SetActive(false);
    }

    public void ReceberDadosPersonagem() //função que atualiza os dados do personagem selecionado
    {
        //atualiza os dados do personagem
        switch (personagem.classe)
        {
            case Classe.Guerreiro:
                id = 0;
                break;
            case Classe.Arqueiro:
                id = 1;
                break;
            case Classe.Mago:
                id = 2;
                break;
        }
        personagemVisual[id].SetActive(true);
        canvas.SetActive(true);
    }

    public void IniciarBatalha() //função chamada ao inicar a batalha e define os valores e comportamentos iniciais do personagem
    {
        _hitAtaquePersonagem = transform.GetComponentInChildren<HitAtaquePersonagem>(true); //encontra o hit do personagem dentro de si
        hpAtual = _hpMaximoEInicial; //define o hp atual do personagem igual ao valor máximo e inicial

        FeedbacksVisuais(); //chama a função para verificar quais feedbacks visuais irá usar
        SelecionarAlvo(); //chama a função para o personagem encontrar seu alvo
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
            MovimentoEspecial(movimentoEspecialAtual);
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
        //checa se o personagem não está morto, se a batalha iniciou e se o personagem não está em idle
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

            //se é um personagem de longa distancia, transtorma o hit em um ataque de longa distancia
            if(_tipo == TipoDePersonagem.LONGA_DISTANCIA)
            {
                _hitAtaquePersonagem.ResetarPosição(); //reseta a posição do hit
                _hitAtaquePersonagem.MoverAteAlvo(_alvoAtual, velocidadeDoProjetil); //faz com que ele se mova até o alvo

                //toca o sfx de projétil se deve usar os sfxs
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
    private void MovimentoEspecial(int movimento) //função do movimento especial do personagem
    {
        //retorna caso já esteja executando o movimento especial
        if(executandoMovimentoEspecial)
        {
            return;
        }

        executandoMovimentoEspecial = true;

        if (movimento == 1)
        {
            if(SistemaDeBatalha.usarAnimações && _animator != null)
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
            if (SistemaDeBatalha.usarAnimações && _animator != null)
            {
                _animator.ResetTrigger("Perseguir");
                _animator.ResetTrigger("Atacar");
                _animator.SetTrigger("Defender");
            }
        }
    }

    public void FinalizarMovimentoEspecial() //função que finaliza o movimento especial externamente
    {
        if(movimentoEspecialAtual == 1)
        {
            Habilidade2 habilidade2 = GetComponent<Habilidade2>();
            habilidade2.RemoverEfeito();
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

    IEnumerator TempoMovimentoEspecial(int movimento) //função de movimento especial que utiliza tempo
    {
        if(movimento == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                _hitAtaquePersonagem.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                _hitAtaquePersonagem.gameObject.SetActive(false);
            }

            Habilidade2 habilidade2 = GetComponent<Habilidade2>();
            habilidade2.RemoverEfeito();
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
    public void CausarDano(IAPersonagemBase personagem) //função de causar dano a um personagem
    {
        //causa dano ao personagem se ele for um personagem inimigo e é o alvo atual deste personagem
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

    public void SofrerDano(float dano) //função para sofrer dano
    {
        if (!imuneADanos)
        {
            hpAtual -= Mathf.RoundToInt(dano * (1f - (reducaoDeDano / 100f))); //sofre o dano

            if (_usarSliders)
            {
                //atualiza o slider
                _slider.value = hpAtual;
            }

            if (hpAtual <= 0)
            {
                hpAtual = 0;
                VerificarComportamento("morrer");
            }
        }
    }

    public void ReceberHP(float cura) //função para receber hp
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
        }
    }
    #endregion

    #region Morte
    private void Morrer() //função de morte do personagem
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

        //caso deva usar as animações
        if (SistemaDeBatalha.usarAnimações && _animator != null)
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
    private void FeedbacksVisuais() //função para verificar feedbacks visuais
    {
        _usarAnimações = SistemaDeBatalha.usarAnimações; //verifica se deve usar animações referente o sistema de batalha
        _usarSliders = SistemaDeBatalha.usarSliders; //verifica se deve usar sliders de hp referente ao sistema de batalha
        _usarSFX = SistemaDeBatalha.usarSfxs; //verifica se deve usar sfxs referente ao sistema de batalha

        if (_usarAnimações)
        {
            //encontra o animator no objeto
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.Log("Não há animator");
            }
        }

        if (_usarSliders)
        {
            _slider.gameObject.SetActive(true);
            if (_slider == null)
            {
                Debug.Log("Não há slider");
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
