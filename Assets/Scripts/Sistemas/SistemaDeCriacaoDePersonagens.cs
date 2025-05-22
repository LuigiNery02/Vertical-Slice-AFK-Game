using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDeCriacaoDePersonagens : MonoBehaviour
{
    [Header("Personagens")]
    public List<PersonagemData> personagensCriados = new List<PersonagemData>(); //lista de todos os personagens criados
    public PersonagemData personagemEmCriacao; //representa o personagem atual que está sendo criado

    #region Visual
    [Header("Tela Personagem")]
    [SerializeField]
    private GameObject _telaPersonagens; //tela do personagem criado
    [SerializeField]
    private GameObject _telaPreferenciaHatributo; //tela de preferencias de atributo do personagem
    [SerializeField]
    private InputField _apelido; //texto do apelido do personagem
    [SerializeField]
    private Text _codigoIDTexto; //texto do códigoID do personagem
    [SerializeField]
    private Image _personagemImagem; //imagem do personagem criado
    [SerializeField]
    private Text _nivelTexto; //texto do nível do personagem
    [SerializeField]
    private Text _forcaTexto; //texto da força do personagem
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

    [Header("Sprites")]
    [SerializeField]
    private Sprite[] _personagensSprites; //sprites dos personagens
    
    [Header("Tela Seleção de Armas")]
    [SerializeField]
    private GameObject[] _telasArmas; //telas de seleção de armas baseadas na classe selecionada
    #endregion

    private int _imagemClasseAtual; //variável que determina a imagem do jogador a depender de sua classe
    private HashSet<string> _codigosGerados = new HashSet<string>(); //lista de códigos gerados

    public void CriarPersonagem() //função que inicia a criação do personagem
    {
        personagemEmCriacao = new PersonagemData();
        ResetarTelaPersonagem();
    }
    public void DefinirClasse(int valorClasse) //função para definir a classe do personagem
    {
        switch(valorClasse)
        {
            case 0:
                personagemEmCriacao.classe = Classe.Guerreiro;
                _telasArmas[valorClasse].SetActive(true);
                break;
            case 1:
                personagemEmCriacao.classe = Classe.Arqueiro;
                personagemEmCriacao.apelido = "Arqueiro";
                _imagemClasseAtual = valorClasse;
                _telasArmas[valorClasse].SetActive(true);
                break;
            case 2:
                personagemEmCriacao.classe = Classe.Mago;
                personagemEmCriacao.apelido = "Mago";
                _imagemClasseAtual = valorClasse;
                _telasArmas[valorClasse].SetActive(true);
                break;
        }
    }

    public void DefinirArma(int valorArma) //função para definir a arma do personagem
    {
        switch(valorArma)
        {
            case 0:
                Debug.Log("arma1");
                break;
            case 1:
                Debug.Log("arma2");
                break;
            case 2:
                Debug.Log("arma3");
                break;
        }

        for(int i = 0; i < _telasArmas.Length; i++)
        {
            _telasArmas[i].SetActive(false);
        }
    }

    public void DefinirPreferenciaDeAtributo(int valorAtributoPreferencia) //função que define as preferências de atributo do personagem
    {
        int atributos = personagemEmCriacao.atributosDePreferencia.Count;

        //define as preferências de atributo do personagem
        switch (valorAtributoPreferencia)
        {
            case 0:
                personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Forca);
                atributos = personagemEmCriacao.atributosDePreferencia.Count;
                break;
            case 1:
                personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Agilidade);
                atributos = personagemEmCriacao.atributosDePreferencia.Count;
                break;
            case 2:
                personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Destreza);
                atributos = personagemEmCriacao.atributosDePreferencia.Count;
                break;
            case 3:
                personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Constituicao);
                atributos = personagemEmCriacao.atributosDePreferencia.Count;
                break;
            case 4:
                personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Inteligencia);
                atributos = personagemEmCriacao.atributosDePreferencia.Count;
                break;
            case 5:
                personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Sabedoria);
                atributos = personagemEmCriacao.atributosDePreferencia.Count;
                break;
        }

        if (atributos >= 2)
        {
            _telaPreferenciaHatributo.SetActive(false); //desativa a tela de preferências de atributo
            PersonagemCriado();
            _telaPersonagens.SetActive(true); //ativa a tela do personagem
            AtualizarTelaPersonagem();
        }
    }

    public void DefinirApelido() //função para definir o apelido do personagem
    {
        personagemEmCriacao.apelido = _apelido.text;
    }

    private string GerarCodigoID() //função que gera o CódigoID do personagem
    {
        //define os princípios para gerar o código
        string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numeros = "0123456789";
        System.Random sortear = new System.Random();
        string codigo;

        //gera o código
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

        }while(_codigosGerados.Contains(codigo)); //irá repetir o código caso já exista

        //adiciona o código ao personagem
        _codigosGerados.Add(codigo);
        return codigo;
    }

    public void PersonagemCriado() //função que finaliza a criação do personagem e define suas características básicas
    {
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
        //define o nível e atributos iniciais do personagem
        personagemEmCriacao.nivel = 1;
        personagemEmCriacao.forca = 1;
        personagemEmCriacao.agilidade = 1;
        personagemEmCriacao.destreza = 1;
        personagemEmCriacao.constituicao = 1;
        personagemEmCriacao.inteligencia = 1;
        personagemEmCriacao.sabedoria = 1;
        personagemEmCriacao.codigoID = GerarCodigoID(); //gera o código do personagem
        personagensCriados.Add(personagemEmCriacao); //adiciona o personagem criado à lista
    }

    #region Atualização Tela de Personagem
    public void AtualizarTelaPersonagem() //função da tela do personagem 
    {
        //atualiza os visuais da tela de acordo com os dados do personagem
        _apelido.text = personagemEmCriacao.apelido;
        _codigoIDTexto.text = personagemEmCriacao.codigoID;
        _personagemImagem.sprite = _personagensSprites[_imagemClasseAtual];
        _nivelTexto.text += (" " + personagemEmCriacao.nivel);
        _forcaTexto.text += (" " + personagemEmCriacao.forca);
        _agilidadeTexto.text += (" " + personagemEmCriacao.agilidade);
        _destrezaTexto.text += (" " + personagemEmCriacao.destreza);
        _constituicaoTexto.text += (" " + personagemEmCriacao.constituicao);
        _inteligenciaTexto.text += (" " + personagemEmCriacao.inteligencia);
        _sabedoriaTexto.text += (" " + personagemEmCriacao.sabedoria);

        //muda a cor do texto para identificar o atributo de preferência do personagem
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

    private void ResetarTelaPersonagem() //função que reseta os dados visuais da tela de personagem
    {
        //reseta todos os dados visuais
        _apelido.text = "";
        _codigoIDTexto.text = "";
        _personagemImagem.sprite = null;
        _nivelTexto.text = ("Nível:");
        _forcaTexto.text = ("Força:");
        _agilidadeTexto.text = ("Agilidade:");
        _destrezaTexto.text = ("Destreza:");
        _constituicaoTexto.text = ("Constituição:");
        _inteligenciaTexto.text = ("Inteligência:");
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
}
