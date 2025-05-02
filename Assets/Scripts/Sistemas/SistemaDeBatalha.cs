using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EstadoDeBatalha { MANUAL, CONTINUA} //estados do sistema de batalha
public enum PrimeiroAlvo { ALVO_PROXIMO, ALVO_VISTO} //tipos de como os personagens vão definir seus alvos na batalha

sealed class SistemaDeBatalha : MonoBehaviour
{
    //Área referente aos estados da batalha
    [Header("Estado de Batalha")]
    public EstadoDeBatalha estado;

    //Área referente aos ajustes da batalha
    [Header("Ajustes de Batalha")]
    public PrimeiroAlvo primeiroAlvo;

    //Área referente aos resultados de batalha
    [Header("Eventos")]
    [SerializeField]
    private UnityEvent _quandoVencer; //evento de vitória
    [SerializeField]
    private UnityEvent _quandoPerder; //evento de derrota

    //Área referente à UI
    [Header("UI")]
    [SerializeField]
    private Button _botaoBatalhaContinua;
    [SerializeField]
    private GameObject _telasDeResultaDeVitoria; //tela de resultado de vitoria
    [SerializeField]
    private GameObject _telasDeResultaDeDerrota; //tela de resultado de derrota
    [SerializeField]
    private GameObject _botaoConfiguracao; //botao de configurações do jogo
    [SerializeField]
    private Dropdown _dropdown; //dropdown de seleção de estado de batalha

    //Área de SFX
    [Header("SFX")]
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _botaoClip;
    [SerializeField]
    private AudioClip _vitoriaClip;

    //Área referente os feedbacks visuais
    [Header("Feedbacks Visuais")]
    public static bool usarAnimações = true; //variável para verificar se os personagens devem usar as animações
    public static bool usarSfxs = true; //variável para verificar se deve haver SFX
    public static bool usarSliders = true; //variável para verificar se os personagens devem ter sliders para representar suas vidas

    //Área referente aos times
    private List<IAPersonagemBase> _personagensJogador = new List<IAPersonagemBase>(); //time do jogador
    private List<IAPersonagemBase> _personagensInimigos = new List<IAPersonagemBase>(); //time do inimigo
    private int _integrantesTimeJogador; //número de integrantes do time do jogador
    private int _integrantesTimeInimigo; //número de integrantes do time inimigo

    [HideInInspector]
    public bool batalhaIniciou; //variável que define se a batalha foi iniciada 
    private bool _batalhaAlvoVisto; //variável para verificar se inicialmente é uma batalha de primeiro alvo visto
    private bool _podeComeçarBatalha = true; //variável para verificar se pode iniciar batalha
    [HideInInspector]
    public bool fimDeBatalha; //variável para verificar o fim da batalha
    private SistemaDeDrop _sisemaDeDrop; //sistema de drop

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(MudarEstadoDeBatalha);
        _sisemaDeDrop = FindObjectOfType<SistemaDeDrop>();
    }
    public void IniciarBatalha() //função que inicia a batalha
    {
        if(!batalhaIniciou && _podeComeçarBatalha) //checa se a batalha já não foi iniciada e se pode começar
        {
            batalhaIniciou = true; //define a batalha como iniciada
            _podeComeçarBatalha = false; //não pode começara batalha novamente
            _botaoConfiguracao.SetActive(false); //desativa o botão de configurações
            if(primeiroAlvo == PrimeiroAlvo.ALVO_VISTO)
            {
                _batalhaAlvoVisto = true;
            }
            EncontrarPersonagens(); //chama a função de encontrar personagens
        }
    }

    public void DefinirBatalhaContinua() //define se o estado da batalha é continuo ou não
    {
        //checa o estado da batalha, se for manual fica continua, e sor continua fica manual
        if(estado == EstadoDeBatalha.MANUAL)
        {
            estado = EstadoDeBatalha.CONTINUA;
        }
        else
        {
            estado = EstadoDeBatalha.MANUAL;
        }

        //chama a função de iniciar a batalha
        IniciarBatalha();
    }

    private void EncontrarPersonagens() //função que encontra todos os personagens na cena
    {
        //Reseta os times
        _personagensJogador.Clear();
        _personagensInimigos.Clear();

        //procura todos os personagens (objetos que possuem o script IAPersonagemBase) na cena
        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase personagem in personagens)
        {
            if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR) //verifica se é personagem do jogador
            {
                //salvar posição e rotação inicial dos personagens
                personagem.posicaoInicial = personagem.transform.position;
                personagem.rotacaoInicial = personagem.transform.rotation;
            }
            //define os times
            if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
            {
                AtualizarTime(("adicionar"), ("jogador"), personagem);
            }
            else if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                AtualizarTime(("adicionar"), ("inimigo"), personagem);
            }

            personagem.IniciarBatalha(); //chama a função "IniciarBatalha" de todos os personagens encontrados
        }

        primeiroAlvo = PrimeiroAlvo.ALVO_PROXIMO; //define a batalha como alvo próximo para os próximos alvos dos personagens
    }

    public void AtualizarTime(string atualizacao, string time, IAPersonagemBase personagem) //função que atualiza os times
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
                _personagensJogador.Remove(personagem);
                _integrantesTimeJogador--;

                //chama o fim da batalha caso o número de integrantes de um time chegar a 0
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

                //chama o fim da batalha caso o número de integrantes de um time chegar a 0
                if (_integrantesTimeInimigo <= 0)
                {
                    ChecarSFX("vitoria");
                    StartCoroutine(FimDeBatalha("vitoria"));
                }
            }
        }
    }

    IEnumerator FimDeBatalha(string resultado) //função que determina o resultado da batalha
    {
        batalhaIniciou = false;
        fimDeBatalha = true;

        //reseta todas as habilidades
        HabilidadesBase[] habilidades = FindObjectsOfType<HabilidadesBase>();

        foreach (HabilidadesBase habilidade in habilidades)
        {
            habilidade.StopAllCoroutines();
            habilidade.RemoverEfeitoExternamente();
            habilidade.podeAtivarEfeito = true;
        }

        yield return new WaitForSeconds(1.5f); //aguarda 1,5 segundo

        if(resultado == "vitoria")
        {
            _quandoVencer.Invoke(); //chama o evento de vitória
        }
        else if(resultado == "derrota")
        {
            estado = EstadoDeBatalha.MANUAL;
            _quandoPerder.Invoke(); //chama o evento de derrota
        }

        //recomeça a batalha se o estado de batalha for continua
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
            RecomeçarBatalha();

            yield return new WaitForSeconds(0.5f);

            _podeComeçarBatalha = true;
            IniciarBatalha();
        }
    }

    public void RecomeçarBatalha() //função para resetar a batalha
    {
        _integrantesTimeInimigo = 0;
        _integrantesTimeJogador = 0;
        if (estado == EstadoDeBatalha.CONTINUA)
        {
            _botaoConfiguracao.SetActive(false);
        }
        else
        {
            _botaoConfiguracao.SetActive(true); //reativa o botão de configurações
            _podeComeçarBatalha = true;
        }
        if (_batalhaAlvoVisto)
        {
            primeiroAlvo = PrimeiroAlvo.ALVO_VISTO;
        }

        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase personagem in personagens)
        {
            personagem.ResetarEstado(); //chama a função para resetar HP, animação, status, etc.
        }
    }

    public void VerificarBotao()
    {
        if(estado == EstadoDeBatalha.CONTINUA)
        {
            _botaoBatalhaContinua.image.color = Color.white;
        }
        else
        {
            _botaoBatalhaContinua.image.color = Color.gray;
        }
    }

    private void MudarEstadoDeBatalha(int indice) //função que muda o estado de batalha
    {
        string opcaoSelecionada = _dropdown.options[indice].text;

        if(opcaoSelecionada == "Alvo Próximo")
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

    public void SairDoJogo() //função para sair do jogo
    {
        Application.Quit();
    }

    public void ChecarSFX(string sfx) //função para checar quais sfx utilizar
    {
        if (usarSfxs)
        {
            if(sfx == "botao") //sfx do botão
            {
                _audio.clip = _botaoClip;
                _audio.Play();
            }
            else if(sfx == "vitoria") //sfx de vitória
            {
                _audio.clip = _vitoriaClip;
                _audio.Play();
            }
        }
    }

    public void DefinirFeedbackVisual(string feedback) //função para definir quais feedbacks visuais serão usados
    {
        if(feedback == "animação") //feedback visual de animação
        {
            usarAnimações = !usarAnimações;
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
}
