using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EstadoDeBatalha { MANUAL, CONTINUA} //estados do sistema de batalha
public enum PrimeiroAlvo { ALVO_PROXIMO, ALVO_VISTO} //tipos de como os personagens v�o definir seus alvos na batalha

sealed class SistemaDeBatalha : MonoBehaviour, Salvamento
{
    //�rea referente aos estados da batalha
    [Header("Estado de Batalha")]
    public EstadoDeBatalha estado;

    //�rea referente aos ajustes da batalha
    [Header("Ajustes de Batalha")]
    public PrimeiroAlvo primeiroAlvo;

    //�rea referente aos resultados de batalha
    [Header("Eventos")]
    [SerializeField]
    private UnityEvent _quandoVencer; //evento de vit�ria
    [SerializeField]
    private UnityEvent _quandoPerder; //evento de derrota

    //�rea referente � UI
    [Header("UI")]
    [SerializeField]
    private Button _botaoBatalhaContinua;
    [SerializeField]
    private GameObject _telasDeResultaDeVitoria; //tela de resultado de vitoria
    [SerializeField]
    private GameObject _telasDeResultaDeDerrota; //tela de resultado de derrota
    [SerializeField]
    private GameObject _botaoConfiguracao; //botao de configura��es do jogo
    [SerializeField]
    private Dropdown _dropdown; //dropdown de sele��o de estado de batalha
    [SerializeField]
    private GameObject _telaDuracaoBatalha; //tela de dura��o da batalha
    [SerializeField]
    private GameObject _telaRecompensasDrop; //tela de recompensas do drop
    [SerializeField]
    private Text _textoRecompensasDrop; //texto de recompensas de drop;

    //�rea de SFX
    [Header("SFX")]
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _botaoClip;
    [SerializeField]
    private AudioClip _vitoriaClip;

    //�rea referente os feedbacks visuais
    public static bool usarAnima��es = true; //vari�vel para verificar se os personagens devem usar as anima��es
    public static bool usarSfxs = true; //vari�vel para verificar se deve haver SFX
    public static bool usarSliders = true; //vari�vel para verificar se os personagens devem ter sliders para representar suas vidas

    //�rea referente aos times
    [SerializeField]
    private List<IAPersonagemBase> _personagensJogador = new List<IAPersonagemBase>(); //time do jogador
    private List<IAPersonagemBase> _personagensInimigos = new List<IAPersonagemBase>(); //time do inimigo
    private int _integrantesTimeJogador; //n�mero de integrantes do time do jogador
    private int _integrantesTimeInimigo; //n�mero de integrantes do time inimigo
    private List<string> _codigosIDPersonagensBatalhaContinua = new List<string>(); //c�digos ID dos personagens da �ltima batalha continua

    //�rea referente ao save
    private DateTime _tempo; //tempo do sistema de batalha
    private float _duracaoBatalhaContinua; //dura��o em segundos da batalha continua
    private float _tempoAtualBatalhaContinua; //tempo atual da batalha cont�nua
    private bool _acontecendoBatalhaContinua; //vari�vel para verificar se a batalha continua est� acontecendo

    //�rea referente � simula��o
    private int _batalhasRestantesSimuladas = 0; //valor de batalhas simuladas
    private int _dropsRestantes = 0;
    private float _tempoPorBatalha = 30f; //tempo m�dio para uma batalha terminar em segundos
    private bool _simular; //vari�vel que verifica se deve simular a batalha
    private int _dropsGanhos; //valor dos drops que o jogador ganhou enquanto estava afk


    [HideInInspector]
    public bool batalhaIniciou; //vari�vel que define se a batalha foi iniciada 
    private bool _batalhaAlvoVisto; //vari�vel para verificar se inicialmente � uma batalha de primeiro alvo visto
    private bool _podeCome�arBatalha = true; //vari�vel para verificar se pode iniciar batalha
    [HideInInspector]
    public bool fimDeBatalha; //vari�vel para verificar o fim da batalha
    private SistemaDeDrop _sisemaDeDrop; //sistema de drop
    private GerenciadorDePersonagens _gerenciadorDePersonagens; //gerenciador de personagens

    private void Awake()
    {
        _gerenciadorDePersonagens = FindObjectOfType<GerenciadorDePersonagens>();
    }

    public void CarregarSave(GameData data) //fun��o que carrega os dados do save
    {
        _tempo = DateTime.Parse(data.tempo);
        _duracaoBatalhaContinua = data.duracaoBatalhaContinua;
        _acontecendoBatalhaContinua = data.acontecendoBatalhaContinua;
        _batalhasRestantesSimuladas = data.batalhasRestantes;
        _dropsRestantes = data.dropsRestantes;

        if (_acontecendoBatalhaContinua)
        {
            TimeSpan tempoPassado = DateTime.Now - _tempo;

            _tempoAtualBatalhaContinua = data.tempoAtualBatalhaContinua - (float)tempoPassado.TotalSeconds;
            if (_tempoAtualBatalhaContinua < 0f)
            {
                _tempoAtualBatalhaContinua = 0f;
            }

            _codigosIDPersonagensBatalhaContinua.Clear();
            for (int i = 0; i < data.codigoPersonagensBatalhaContinua.Count; i++)
            {
                _codigosIDPersonagensBatalhaContinua.Add(data.codigoPersonagensBatalhaContinua[i]);
            }

            IAPersonagemBase[] personagensIA = FindObjectsOfType<IAPersonagemBase>();
            List<IAPersonagemBase> personagensJogador = new List<IAPersonagemBase>();
            foreach (var personagem in personagensIA)
            {
                if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
                {
                    personagensJogador.Add(personagem);
                }
            }

            int max = Mathf.Min(_codigosIDPersonagensBatalhaContinua.Count, personagensJogador.Count);
            for (int i = 0; i < max; i++)
            {
                string codigo = _codigosIDPersonagensBatalhaContinua[i];
                PersonagemData personagem = GerenciadorDeInventario.instancia.personagensCriados.Find(p => p.codigoID == codigo);

                if (personagem != null)
                {
                    personagensJogador[i].personagem = personagem;

                    if (!_personagensJogador.Contains(personagensJogador[i]))
                    {
                        AtualizarTime("adicionar", "jogador", personagensJogador[i]);
                    }
                }
            }

            _gerenciadorDePersonagens.RestaurarSlotsSelecionados();
        }
        else
        {
            _tempoAtualBatalhaContinua = 0f;
        }
    }


    public void SalvarSave(GameData data) //fun��o de salvar os dados do save
    {
        if (_acontecendoBatalhaContinua)
        {
            if (_tempoAtualBatalhaContinua < 0f)
            {
                _tempoAtualBatalhaContinua = 0f;
            }
        }

        data.tempo = DateTime.Now.ToString();
        data.tempoAtualBatalhaContinua = _tempoAtualBatalhaContinua;
        data.duracaoBatalhaContinua = _duracaoBatalhaContinua;
        data.acontecendoBatalhaContinua = _acontecendoBatalhaContinua;
        data.batalhasRestantes = _batalhasRestantesSimuladas;
        data.dropsRestantes = _dropsRestantes;

        if (_acontecendoBatalhaContinua)
        {
            _codigosIDPersonagensBatalhaContinua.Clear();

            for (int i = 0; i < _personagensJogador.Count; i++)
            {
                _codigosIDPersonagensBatalhaContinua.Add(_personagensJogador[i].personagem.codigoID);
            }

            data.codigoPersonagensBatalhaContinua = new List<string>(_codigosIDPersonagensBatalhaContinua);
        }

    }

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(MudarEstadoDeBatalha);
        _sisemaDeDrop = FindObjectOfType<SistemaDeDrop>();

        //verifica se ao iniciar a batalha continua estava como verdadeira ao sair do jogo
        if (_acontecendoBatalhaContinua)
        {
            if (_tempoAtualBatalhaContinua <= 0)
            {
                _telaRecompensasDrop.SetActive(true);
                _dropsGanhos = _dropsRestantes;
                _tempoAtualBatalhaContinua = 0f;
                _batalhasRestantesSimuladas = 0;
            }
            else
            {
                float tempoPassado = _duracaoBatalhaContinua - _tempoAtualBatalhaContinua;
                int batalhasQueJaAconteceram = Mathf.FloorToInt((tempoPassado / 2) / _tempoPorBatalha);
                _batalhasRestantesSimuladas -= batalhasQueJaAconteceram;

                if(_batalhasRestantesSimuladas < 0)
                {
                    _batalhasRestantesSimuladas = 0;
                }

                //gera drops para batalhas que j� aconteceram
                int dropsGanhos = 0;
                for (int i = 0; i < batalhasQueJaAconteceram; i++)
                {
                    dropsGanhos++;
                }

                _dropsRestantes -= dropsGanhos;
                _dropsGanhos = dropsGanhos;

                VerificarBotao();
                IniciarBatalhaContinua(_tempoAtualBatalhaContinua);
            }
            Simula��oDeRecompensas();
        }
        else
        {
            _simular = true;
        }
    }

    private void Update()
    {
        if (_acontecendoBatalhaContinua)
        {
            _tempoAtualBatalhaContinua -= Time.deltaTime;

            if (_tempoAtualBatalhaContinua <= 0f)
            {
                _tempoAtualBatalhaContinua = 0f;
                _acontecendoBatalhaContinua = false;
                _simular = true;
                estado = EstadoDeBatalha.MANUAL;
                VerificarBotao();
            }
        }
    }
    public void IniciarBatalha() //fun��o que inicia a batalha
    {
        if(!batalhaIniciou && _podeCome�arBatalha) //checa se a batalha j� n�o foi iniciada e se pode come�ar
        {
            batalhaIniciou = true; //define a batalha como iniciada
            _podeCome�arBatalha = false; //n�o pode come�ara batalha novamente
            _botaoConfiguracao.SetActive(false); //desativa o bot�o de configura��es
            if(primeiroAlvo == PrimeiroAlvo.ALVO_VISTO)
            {
                _batalhaAlvoVisto = true;
            }
            EncontrarPersonagens(); //chama a fun��o de encontrar personagens
            if(estado == EstadoDeBatalha.CONTINUA)
            {
                _acontecendoBatalhaContinua = true;
            }
        }
    }

    public void IniciarBatalhaContinua(float duracao) //fun��o que inicia a batalha continua
    {
        _duracaoBatalhaContinua = duracao;
        _tempoAtualBatalhaContinua = duracao;

        //define a batalha como continua
        estado = EstadoDeBatalha.CONTINUA;

        //chama a fun��o de iniciar a batalha
        IniciarBatalha();
    }

    public void DefinirBatalhaContinua() //define se o estado da batalha � continuo ou n�o
    {
        //checa o estado da batalha, se for manual fica continua, e sor continua fica manual
        if(estado == EstadoDeBatalha.MANUAL)
        {
            estado = EstadoDeBatalha.CONTINUA;
            _acontecendoBatalhaContinua = true;
            if (!batalhaIniciou)
            {
                _telaDuracaoBatalha.SetActive(true); //ativa a tela de dura��o de batalha
            }
        }
        else
        {
            _acontecendoBatalhaContinua = false;
            estado = EstadoDeBatalha.MANUAL;
        }
    }

    private void EncontrarPersonagens() //fun��o que encontra todos os personagens na cena
    {
        //Reseta os times
        if (!_acontecendoBatalhaContinua)
        {
            _personagensJogador.Clear();
            _personagensInimigos.Clear();
        }

        //procura todos os personagens (objetos que possuem o script IAPersonagemBase) na cena
        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase personagem in personagens)
        {
            if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR) //verifica se � personagem do jogador
            {
                //salvar posi��o e rota��o inicial dos personagens
                personagem.posicaoInicial = personagem.transform.position;
                personagem.rotacaoInicial = personagem.transform.rotation;
            }

            //define os times
            if (!_acontecendoBatalhaContinua)
            {
                if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
                {
                    AtualizarTime(("adicionar"), ("jogador"), personagem);
                }
            }
            else
            {
                if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
                {
                    personagem.ReceberDadosPersonagem();
                }
            }
            
            if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                AtualizarTime(("adicionar"), ("inimigo"), personagem);
            }

            personagem.IniciarBatalha(); //chama a fun��o "IniciarBatalha" de todos os personagens encontrados
        }

        if(estado == EstadoDeBatalha.CONTINUA)
        {
            SimularBatalha();
        }

        primeiroAlvo = PrimeiroAlvo.ALVO_PROXIMO; //define a batalha como alvo pr�ximo para os pr�ximos alvos dos personagens

        if(SistemaDeSalvamento.instancia != null)
        {
            SistemaDeSalvamento.instancia.SalvarJogo();
        }
    }

    public void AtualizarTime(string atualizacao, string time, IAPersonagemBase personagem) //fun��o que atualiza os times
    {
        if(atualizacao == ("adicionar")) //adiciona personagem
        {
            if(time == "jogador")
            {
                _personagensJogador.Add(personagem);
                _integrantesTimeJogador++;
            }
            else if(time == "inimigo")
            {
                _personagensInimigos.Add(personagem);
                _integrantesTimeInimigo++;
            }
        }
        else if(atualizacao == ("remover")) //remove personagem
        {
            if (time == "jogador")
            {
                //_personagensJogador.Remove(personagem);
                _integrantesTimeJogador--;

                //chama o fim da batalha caso o n�mero de integrantes de um time chegar a 0
                if (_integrantesTimeJogador <= 0)
                {
                    StartCoroutine(FimDeBatalha("derrota"));
                }
            }
            else if (time == "inimigo")
            {
                _sisemaDeDrop.Dropar(personagem.transform);
                _personagensInimigos.Remove(personagem);
                _integrantesTimeInimigo--;

                //chama o fim da batalha caso o n�mero de integrantes de um time chegar a 0
                if (_integrantesTimeInimigo <= 0)
                {
                    ChecarSFX("vitoria");
                    StartCoroutine(FimDeBatalha("vitoria"));
                }
            }
        }
    }

    IEnumerator FimDeBatalha(string resultado) //fun��o que determina o resultado da batalha
    {
        batalhaIniciou = false;
        fimDeBatalha = true;

        RemoverSimula��o(1);

        //reseta todas as habilidades
        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase personagem in personagens)
        {
            if(personagem.habilidade1 != null)
            {
                personagem.habilidade1.StopAllCoroutines();
                personagem.habilidade1.RemoverEfeitoExternamente();
                personagem.habilidade1.podeAtivarEfeito = true;
            }

            if (personagem.habilidade2 != null)
            {
                personagem.habilidade2.StopAllCoroutines();
                personagem.habilidade2.RemoverEfeitoExternamente();
                personagem.habilidade2.podeAtivarEfeito = true;
            }
        }

        yield return new WaitForSeconds(1.5f); //aguarda 1,5 segundo

        if(resultado == "vitoria")
        {
            _quandoVencer.Invoke(); //chama o evento de vit�ria
        }
        else if(resultado == "derrota")
        {
            estado = EstadoDeBatalha.MANUAL;
            _quandoPerder.Invoke(); //chama o evento de derrota
        }

        //salva o jogo
        if(SistemaDeSalvamento.instancia != null)
        {
            SistemaDeSalvamento.instancia.SalvarJogo();
        }

        //recome�a a batalha se o estado de batalha for continua
        if (estado == EstadoDeBatalha.CONTINUA)
        {
            yield return new WaitForSeconds(1f);
            if(resultado == "vitoria")
            {
                _telasDeResultaDeVitoria.SetActive(false);
            }
            else if(resultado == "derrota")
            {
                _telasDeResultaDeDerrota.SetActive(false);
            }
            Recome�arBatalha();

            yield return new WaitForSeconds(0.5f);

            _podeCome�arBatalha = true;
            IniciarBatalha();
        }
    }

    public void Recome�arBatalha() //fun��o para resetar a batalha
    {
        _integrantesTimeInimigo = 0;
        _integrantesTimeJogador = 0;
        if (estado == EstadoDeBatalha.CONTINUA)
        {
            _botaoConfiguracao.SetActive(false);
        }
        else
        {
            _botaoConfiguracao.SetActive(true); //reativa o bot�o de configura��es
            _podeCome�arBatalha = true;
        }
        if (_batalhaAlvoVisto)
        {
            primeiroAlvo = PrimeiroAlvo.ALVO_VISTO;
        }

        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase personagem in personagens)
        {
            personagem.ResetarEstado(); //chama a fun��o para resetar HP, anima��o, status, etc.
            if(personagem.controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
            {
                personagem.AtualizarDadosBatalha();
            }
        }
    }
    private void MudarEstadoDeBatalha(int indice) //fun��o que muda o estado de batalha
    {
        string opcaoSelecionada = _dropdown.options[indice].text;

        if(opcaoSelecionada == "Alvo Pr�ximo")
        {
            _batalhaAlvoVisto = false;
            primeiroAlvo = PrimeiroAlvo.ALVO_PROXIMO;
        }
        else if(opcaoSelecionada == "Alvo Visto")
        {
            _batalhaAlvoVisto = true;
            primeiroAlvo = PrimeiroAlvo.ALVO_VISTO;
        }
    }

    #region Simula��o de Batalha
    private void SimularBatalha() //fun��o que simula a batalha
    {
        if (_simular)
        {
            _simular = false;

            int vitoriasConfirmadas = 0;
            int dropsRecebidos = 0;
            float numeroDeBatalhas = _duracaoBatalhaContinua / _tempoPorBatalha;
            _batalhasRestantesSimuladas = ((int)numeroDeBatalhas);

            List<IAPersonagemBase> todosInimigosDisponiveis = new List<IAPersonagemBase>(_personagensInimigos);

            for (int i = 0; i < numeroDeBatalhas; i++)
            {
                List<IAPersonagemBase> inimigosNaBatalha = new List<IAPersonagemBase>();

                if (i == 0)
                {
                    //primeira batalha: 1 de cada tipo
                    inimigosNaBatalha.Add(todosInimigosDisponiveis[0]);
                    inimigosNaBatalha.Add(todosInimigosDisponiveis[1]);
                    inimigosNaBatalha.Add(todosInimigosDisponiveis[2]);
                }
                else
                {
                    //outras batalhas: sorteio
                    int quantidadeInimigos = 3; // supondo sempre 3 inimigos

                    for (int j = 0; j < quantidadeInimigos; j++)
                    {
                        int indiceSorteado = UnityEngine.Random.Range(0, todosInimigosDisponiveis.Count);
                        inimigosNaBatalha.Add(todosInimigosDisponiveis[indiceSorteado]);
                    }
                }

                //c�lculo do DPS do jogador
                float dpsJogadorTotal = 0f;
                foreach (IAPersonagemBase personagem in _personagensJogador)
                {
                    dpsJogadorTotal += personagem._danoAtaqueBasico / personagem._cooldown;
                    dpsJogadorTotal += personagem.danoAtaqueDistancia / personagem._cooldown;
                    dpsJogadorTotal += personagem.danoAtaqueMagico / personagem._cooldown;
                }

                //c�lculo do DPS dos inimigos sorteados
                float dpsInimigoTotal = 0f;
                foreach (IAPersonagemBase inimigo in inimigosNaBatalha)
                {
                    dpsInimigoTotal += inimigo._danoAtaqueBasico / inimigo._cooldown;
                    dpsInimigoTotal += inimigo.danoAtaqueDistancia / inimigo._cooldown;
                    dpsInimigoTotal += inimigo.danoAtaqueMagico / inimigo._cooldown;
                }

                //calcula chance de vit�ria
                float chanceDeVitoria = (dpsJogadorTotal + 2f) / (dpsJogadorTotal + dpsInimigoTotal);
                Debug.Log(chanceDeVitoria);

                float rolagem = UnityEngine.Random.value - (0.2f); // valor entre 0 e 1

                if (rolagem <= chanceDeVitoria)
                {
                    vitoriasConfirmadas++;
                }
            }

            for (int i = 0; i < (vitoriasConfirmadas * 3); i++)
            {
                int probabilidadeDeDrop = UnityEngine.Random.Range(0, 4);

                if (probabilidadeDeDrop > 0)
                {
                    dropsRecebidos++;
                }
            }

            _dropsRestantes = dropsRecebidos;
        }
    }

    public void RemoverSimula��o(int remocao) //fun��o que remove cada simula��o de batalha
    {
        if(remocao == 1)
        {
            if (_batalhasRestantesSimuladas > 0)
            {
                _batalhasRestantesSimuladas--;
            }
        }
        else if(remocao == 2)
        {
            if (_dropsRestantes > 0)
            {
                _dropsRestantes--;
            }
        } 
    }

    private void Simula��oDeRecompensas() //fun��o que mostra a tela de recompensas do jogador ao voltar ao game enquanto o jogo estava em afk
    {
        _telaRecompensasDrop.SetActive(true);
        _textoRecompensasDrop.text = (_dropsGanhos * 5).ToString();
        _sisemaDeDrop.Receberdrops((_dropsGanhos * 5));
    }
    #endregion

    #region feedbacks Visuais

    public void VerificarBotao()
    {
        if (estado == EstadoDeBatalha.CONTINUA)
        {
            _botaoBatalhaContinua.image.color = Color.white;
        }
        else
        {
            _botaoBatalhaContinua.image.color = Color.gray;
        }
    }
    public void ChecarSFX(string sfx) //fun��o para checar quais sfx utilizar
    {
        if (usarSfxs)
        {
            if(sfx == "botao") //sfx do bot�o
            {
                _audio.clip = _botaoClip;
                _audio.Play();
            }
            else if(sfx == "vitoria") //sfx de vit�ria
            {
                _audio.clip = _vitoriaClip;
                _audio.Play();
            }
        }
    }

    public void DefinirFeedbackVisual(string feedback) //fun��o para definir quais feedbacks visuais ser�o usados
    {
        if(feedback == "anima��o") //feedback visual de anima��o
        {
            usarAnima��es = !usarAnima��es;
        }
        else if (feedback == "sfx") //feedback visual de sfx
        {
            usarSfxs = !usarSfxs;
        }
        else if (feedback == "slider") //feedback visual de slider do hp do personagem
        {
            usarSliders = !usarSliders;
        }
    }
    #endregion
}
