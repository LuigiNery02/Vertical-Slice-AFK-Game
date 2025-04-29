using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EstadoDeBatalha { MANUAL, CONTINUA} //estados do sistema de batalha
public enum PrimeiroAlvo { ALVO_PROXIMO, ALVO_VISTO} //tipos de como os personagens v�o definir seus alvos na batalha

sealed class SistemaDeBatalha : MonoBehaviour
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
    private GameObject _telasDeResultaDeVitoria; //tela de resultado de vitoria
    [SerializeField]
    private GameObject _telasDeResultaDeDerrota; //tela de resultado de derrota
    [SerializeField]
    private GameObject _botaoConfiguracao; //botao de configura��es do jogo

    //�rea de SFX
    [Header("SFX")]
    [SerializeField]
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _botaoClip;
    [SerializeField]
    private AudioClip _vitoriaClip;

    //�rea referente os feedbacks visuais
    [Header("Feedbacks Visuais")]
    [HideInInspector]
    public bool usarAnima��es; //vari�vel para verificar se os personagens devem usar as anima��es
    [HideInInspector]
    public bool usarSfxs; //vari�vel para verificar se deve haver SFX
    [HideInInspector]
    public bool usarSliders; //vari�vel para verificar se os personagens devem ter sliders para representar suas vidas

    //�rea referente aos times
    private List<IAPersonagemBase> _personagensJogador = new List<IAPersonagemBase>(); //time do jogador
    private List<IAPersonagemBase> _personagensInimigos = new List<IAPersonagemBase>(); //time do inimigo
    private int _integrantesTimeJogador; //n�mero de integrantes do time do jogador
    private int _integrantesTimeInimigo; //n�mero de integrantes do time inimigo

    [HideInInspector]
    public bool batalhaIniciou; //vari�vel que define se a batalha foi iniciada 
    private bool _batalhaAlvoVisto; //vari�vel para verificar se inicialmente � uma batalha de primeiro alvo visto

    [HideInInspector]
    public bool fimDeBatalha; //vari�vel para verificar o fim da batalha
    public void IniciarBatalha() //fun��o que inicia a batalha
    {
        if(!batalhaIniciou) //checa se a batalha j� n�o foi iniciada
        {
            batalhaIniciou = true; //define a batalha como iniciada
            _botaoConfiguracao.SetActive(false); //desativa o bot�o de configura��es
            if(primeiroAlvo == PrimeiroAlvo.ALVO_VISTO)
            {
                _batalhaAlvoVisto = true;
            }
            EncontrarPersonagens(); //chama a fun��o de encontrar personagens
        }
    }

    public void DefinirBatalhaContinua() //define se o estado da batalha � continuo ou n�o
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

        //chama a fun��o de iniciar a batalha
        IniciarBatalha();
    }

    private void EncontrarPersonagens() //fun��o que encontra todos os personagens na cena
    {
        //Reseta os times
        _personagensJogador.Clear();
        _personagensInimigos.Clear();

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
            if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_DO_JOGADOR)
            {
                AtualizarTime(("adicionar"), ("jogador"), personagem);
            }
            else if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                AtualizarTime(("adicionar"), ("inimigo"), personagem);
            }

            personagem.IniciarBatalha(); //chama a fun��o "IniciarBatalha" de todos os personagens encontrados
        }

        primeiroAlvo = PrimeiroAlvo.ALVO_PROXIMO; //define a batalha como alvo pr�ximo para os pr�ximos alvos dos personagens
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
                _personagensJogador.Remove(personagem);
                _integrantesTimeJogador--;

                //chama o fim da batalha caso o n�mero de integrantes de um time chegar a 0
                if (_integrantesTimeJogador <= 0)
                {
                    StartCoroutine(FimDeBatalha("derrota"));
                }
            }
            else if (time == "inimigo")
            {
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

            IniciarBatalha();
        }
    }

    public void Recome�arBatalha() //fun��o para resetar a batalha
    {
        _integrantesTimeInimigo = 0;
        _integrantesTimeJogador = 0;
        _botaoConfiguracao.SetActive(true); //reativa o bot�o de configura��es
        if (_batalhaAlvoVisto)
        {
            primeiroAlvo = PrimeiroAlvo.ALVO_VISTO;
        }

        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase personagem in personagens)
        {
            personagem.ResetarEstado(); //chama a fun��o para resetar HP, anima��o, status, etc.
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
}
