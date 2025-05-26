using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDeCriacaoDePersonagens : MonoBehaviour
{
    [Header("Personagens")]
    public List<PersonagemData> personagensCriados = new List<PersonagemData>(); //lista de todos os personagens criados
    public PersonagemData personagemEmCriacao; //representa o personagem atual que est� sendo criado

    [Header("Armas")]
    public List<ArmaBase> armasGuerreiro = new List<ArmaBase>(); //lista de armas da classe "Guerreiro"
    public List<ArmaBase> armasArqueiro = new List<ArmaBase>(); //lista de armas da classe "Arqueiro"
    public List<ArmaBase> armasMago = new List<ArmaBase>(); //lista de armas da classe "Mago"

    #region Visual
    [Header("Tela Personagem")]
    [SerializeField]
    private GameObject _telaPersonagem; //tela do personagem espec�fico criado
    [SerializeField]
    private GameObject _telaPersonagens; //tela dos personagens criados 
    [SerializeField]
    private GameObject _telaSelecaoClasse; //tela de sele��o de classes
    [SerializeField]
    private GameObject _telaPreferenciaHatributo; //tela de preferencias de atributo do personagem
    [SerializeField]
    private InputField _apelido; //texto do apelido do personagem
    [SerializeField]
    private Text _codigoIDTexto; //texto do c�digoID do personagem
    [SerializeField]
    private Image _personagemImagem; //imagem do personagem criado
    [SerializeField]
    private Text _classeTexto; //texto da classe do personagem
    [SerializeField]
    private Text _armaTexto; //texto do nome da arma do personagem
    [SerializeField]
    private Text _nivelTexto; //texto do n�vel do personagem
    [SerializeField]
    private Text _forcaTexto; //texto da for�a do personagem
    [SerializeField]
    private Text _agilidadeTexto; //texto da agilidade do personagem
    [SerializeField]
    private Text _destrezaTexto; //texto da destreza do personagem
    [SerializeField]
    private Text _constituicaoTexto; //texto da constituicao do personagem
    [SerializeField]
    private Text _inteligenciaTexto; //texto da inteligencia do personagem
    [SerializeField]
    private Text _sabedoriaTexto; //texto da sabedoria do personagem
    [SerializeField]
    private GerenciadorDeSlots _gerenciadorDeSlots; //gerenciador de slots

    [Header("Sprites")]
    [SerializeField]
    private Sprite[] _personagensSprites; //sprites dos personagens
    
    [Header("Tela Sele��o de Armas")]
    [SerializeField]
    private GameObject[] _telasArmas; //telas de sele��o de armas baseadas na classe selecionada
    #endregion

    [HideInInspector]
    public int _imagemClasseAtual; //vari�vel que determina a imagem do jogador a depender de sua classe
    private HashSet<string> _codigosGerados = new HashSet<string>(); //lista de c�digos gerados

    public void CriarPersonagem() //fun��o que inicia a cria��o do personagem
    {
        personagemEmCriacao = new PersonagemData();
        _telaPersonagens.SetActive(false); //desativa a tela de personagens
        _telaSelecaoClasse.SetActive(true); //ativa a tela de sele��o de classe
        ResetarTelaPersonagem();
    }
    public void DefinirClasse(int valorClasse) //fun��o para definir a classe do personagem
    {
        switch(valorClasse)
        {
            case 0:
                personagemEmCriacao.classe = Classe.Guerreiro;
                personagemEmCriacao.apelido = "Guerreiro";
                _imagemClasseAtual = valorClasse;
                _telasArmas[valorClasse].SetActive(true); //ativa a tela de sele��o de arma � depender da classe do personagem
                break;
            case 1:
                personagemEmCriacao.classe = Classe.Arqueiro;
                personagemEmCriacao.apelido = "Arqueiro";
                _imagemClasseAtual = valorClasse;
                _telasArmas[valorClasse].SetActive(true); //ativa a tela de sele��o de arma � depender da classe do personagem
                break;
            case 2:
                personagemEmCriacao.classe = Classe.Mago;
                personagemEmCriacao.apelido = "Mago";
                _imagemClasseAtual = valorClasse;
                _telasArmas[valorClasse].SetActive(true); //ativa a tela de sele��o de arma � depender da classe do personagem
                break;
        }
        _telaSelecaoClasse.SetActive(false); //desativa a tela de sele��o de classe
    }

    public void DefinirArma(int valorArma) //fun��o para definir a arma inicial do personagem
    {
        if(personagemEmCriacao.classe == Classe.Guerreiro)
        {
            personagemEmCriacao.arma = armasGuerreiro[valorArma];
        }
        else if(personagemEmCriacao.classe == Classe.Arqueiro)
        {
            personagemEmCriacao.arma = armasArqueiro[valorArma];
        }
        else if(personagemEmCriacao.classe == Classe.Mago)
        {
            personagemEmCriacao.arma = armasMago[valorArma];
        }

        for(int i = 0; i < _telasArmas.Length; i++)
        {
            _telasArmas[i].SetActive(false); //desativa todas as telas de sele��o de armas
        }

        _telaPreferenciaHatributo.SetActive(true); //ativa a tela de sele��o de atributos de prefer�ncia
    }

    public void DefinirPreferenciaDeAtributo(int valorAtributoPreferencia) //fun��o que define as prefer�ncias de atributo do personagem
    {
        int atributos = personagemEmCriacao.atributosDePreferencia.Count; //vari�vel que verifica os atributos selecionados pelo jogador

        //define as prefer�ncias de atributo do personagem
        switch (valorAtributoPreferencia)
        {
            case 0:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Forca))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Forca);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
            case 1:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Agilidade))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Agilidade);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
            case 2:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Destreza))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Destreza);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
            case 3:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Constituicao))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Constituicao);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
            case 4:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Inteligencia))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Inteligencia);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
            case 5:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Sabedoria))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Sabedoria);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
        }

        if (atributos >= 2)
        {
            _telaPreferenciaHatributo.SetActive(false); //desativa a tela de prefer�ncias de atributo
            PersonagemCriado();
            _telaPersonagem.SetActive(true); //ativa a tela do personagem
            AtualizarTelaPersonagem();
        }
    }

    public void DefinirApelido() //fun��o para definir o apelido do personagem
    {
        personagemEmCriacao.apelido = _apelido.text;
        _gerenciadorDeSlots.AtualizarSlots();
    }

    private string GerarCodigoID() //fun��o que gera o C�digoID do personagem
    {
        //define os princ�pios para gerar o c�digo
        string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numeros = "0123456789";
        System.Random sortear = new System.Random();
        string codigo;

        //gera o c�digo
        do
        {
            string parte1 = new string(new char[] {
            letras[sortear.Next(26)], letras[sortear.Next(26)], letras[sortear.Next(26)],
        });
            string parte2 = new string(new char[] {
            numeros[sortear.Next(10)], numeros[sortear.Next(10)], numeros[sortear.Next(10)]
        });
            string parte3 = new string(new char[] {
            letras[sortear.Next(26)], letras[sortear.Next(26)], letras[sortear.Next(26)],
        });

            string parte4 = new string(new char[] {
            numeros[sortear.Next(10)], numeros[sortear.Next(10)], numeros[sortear.Next(10)]
        });

            codigo = parte1 + "." + parte2 + "." + parte3 + "." + parte4;

        }while(_codigosGerados.Contains(codigo)); //ir� repetir o c�digo caso j� exista

        //adiciona o c�digo ao personagem
        _codigosGerados.Add(codigo);
        return codigo;
    }

    public void PersonagemCriado() //fun��o que finaliza a cria��o do personagem e define suas caracter�sticas b�sicas
    {
        personagemEmCriacao.DefinirPersonagem();

        switch (personagemEmCriacao.classe)
        {
            case Classe.Guerreiro:
                personagemEmCriacao.apelido = "Guerreiro";
                _imagemClasseAtual = 0;
                break;
            case Classe.Arqueiro:
                personagemEmCriacao.apelido = "Arqueiro";
                _imagemClasseAtual = 1;
                break;
            case Classe.Mago:
                personagemEmCriacao.apelido = "Mago";
                _imagemClasseAtual = 2;
                break;
        }
        //define o n�vel e atributos iniciais do personagem
        personagemEmCriacao.nivel = 1;
        personagemEmCriacao.forca = 1;
        personagemEmCriacao.agilidade = 1;
        personagemEmCriacao.destreza = 1;
        personagemEmCriacao.constituicao = 1;
        personagemEmCriacao.inteligencia = 1;
        personagemEmCriacao.sabedoria = 1;
        personagemEmCriacao.expProximoN�vel = 50;

        personagemEmCriacao.codigoID = GerarCodigoID(); //gera o c�digo do personagem
        personagensCriados.Add(personagemEmCriacao); //adiciona o personagem criado � lista
        _gerenciadorDeSlots.AdicionarSlot();
        _gerenciadorDeSlots.AtualizarSlots();
    }

    public void EditarPersonagem() //fun��o que possibilita a edi��o do personagem
    {
        _telaPersonagem.SetActive(true); //ativa a tela de personagem
        _telaPersonagens.SetActive(false); //desativa a tela de personagens
    }

    public void DeletarPersonagemCriado(int indice) //fun��o de deletar um personagem criado
    {
        if(indice >= 0 && indice < personagensCriados.Count)
        {
            personagensCriados.RemoveAt(indice);
            _gerenciadorDeSlots.AtualizarSlots();
        }
    }

    #region Atualiza��o Tela de Personagem
    public void AtualizarTelaPersonagem() //fun��o da tela do personagem 
    {
        //atualiza os visuais da tela de acordo com os dados do personagem
        _apelido.text = personagemEmCriacao.apelido;
        _codigoIDTexto.text = personagemEmCriacao.codigoID;
        _personagemImagem.sprite = _personagensSprites[_imagemClasseAtual];
        _classeTexto.text += (" " + personagemEmCriacao.classe.ToString());
        _armaTexto.text = personagemEmCriacao.arma.nome;
        _nivelTexto.text += (" " + personagemEmCriacao.nivel);
        _forcaTexto.text += (" " + personagemEmCriacao.forca);
        _agilidadeTexto.text += (" " + personagemEmCriacao.agilidade);
        _destrezaTexto.text += (" " + personagemEmCriacao.destreza);
        _constituicaoTexto.text += (" " + personagemEmCriacao.constituicao);
        _inteligenciaTexto.text += (" " + personagemEmCriacao.inteligencia);
        _sabedoriaTexto.text += (" " + personagemEmCriacao.sabedoria);

        //muda a cor do texto para identificar o atributo de prefer�ncia do personagem
        if(personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Forca))
        {
            _forcaTexto.color = Color.green;
        }
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Agilidade))
        {
            _agilidadeTexto.color = Color.green;
        }
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Destreza))
        {
            _destrezaTexto.color = Color.green;
        }
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Constituicao))
        {
            _constituicaoTexto.color = Color.green;
        }
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Inteligencia))
        {
            _inteligenciaTexto.color = Color.green;
        }
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Sabedoria))
        {
            _sabedoriaTexto.color = Color.green;
        }
    }

    public void ResetarTelaPersonagem() //fun��o que reseta os dados visuais da tela de personagem
    {
        //reseta todos os dados visuais
        _apelido.text = "";
        _codigoIDTexto.text = "";
        _personagemImagem.sprite = null;
        _classeTexto.text = ("Classe:");
        _armaTexto.text = ("Arma");
        _nivelTexto.text = ("N�vel:");
        _forcaTexto.text = ("For�a:");
        _agilidadeTexto.text = ("Agilidade:");
        _destrezaTexto.text = ("Destreza:");
        _constituicaoTexto.text = ("Constitui��o:");
        _inteligenciaTexto.text = ("Intelig�ncia:");
        _sabedoriaTexto.text = ("Sabedoria:");

        //reseta as cores dos textos
        _forcaTexto.color = Color.white;
        _agilidadeTexto.color = Color.white;
        _destrezaTexto.color = Color.white;
        _constituicaoTexto.color = Color.white;
        _inteligenciaTexto.color = Color.white;
        _sabedoriaTexto.color = Color.white;
    }
    #endregion

    #region Tempor�rio

    public void SubirNivelPersonagem() //fun��o tempor�ria que sobe o n�vel do personagem atual
    {
        personagemEmCriacao.EscolherAtributo();
        personagemEmCriacao.SubirDeNivel();
        ResetarTelaPersonagem();
        AtualizarTelaPersonagem();
        _gerenciadorDeSlots.AtualizarSlots();
    }

    #endregion
}
