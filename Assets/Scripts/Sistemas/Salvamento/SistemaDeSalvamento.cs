using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SistemaDeSalvamento : MonoBehaviour
{
    [Header("Debbuging")]
    [SerializeField]
    private bool _iniciarSalvamentoSeNulo = false; //variável que verifica se deve iniciar um novo save caso não haja

    [Header("Configurações de Arquivo")]
    [SerializeField]
    private string _nomeDoArquivo; //nome do arquivo do save
    [SerializeField]
    private bool _encriptar;  //variável para verificar se deve encriptar o arquivo do save

    private GameData gameData; // GameData onde oa variáveis serão salvas

    private List<Salvamento> _objetosDeSalvamento; //objetos que possuem dados para salvar

    private GerenciadorDeDados _gerenciador; //gerenciador dos dados do save de cada objeto

    private string _IDSelecionado = "save";
    public static SistemaDeSalvamento instancia { get; private set; }

    private void Awake()
    {
        //verifica se há outra instancia deste objeto
        if(instancia != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instancia = this;
        DontDestroyOnLoad(this.gameObject);

        this._gerenciador = new GerenciadorDeDados(Application.persistentDataPath, _nomeDoArquivo, _encriptar);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += QuandoCenaCarregar;
        SceneManager.sceneUnloaded += QuandoCenaDescarregar;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= QuandoCenaCarregar;
        SceneManager.sceneUnloaded -= QuandoCenaDescarregar;
    }

    public void QuandoCenaCarregar(Scene cena, LoadSceneMode modo)
    {
        this._objetosDeSalvamento = EncontrarObjetosDeSalvamento();
        CarregarJogo(); //carrega o jogo
    }

    public void QuandoCenaDescarregar(Scene cena)
    {
        SalvarJogo(); //salva o jogo
    }

    public void MudarPerfilID(string novoPerfilID) //função de mudar o ID do save
    {
        this._IDSelecionado = novoPerfilID;

        CarregarJogo();
    }

    public void DeletarDados(string ID) //função de deletar os dados
    {
        _gerenciador.Deletar(ID);

        CarregarJogo();
    }

    public void NovoJogo() //função de iniciar um novo jogo
    {
        this.gameData = new GameData();
    }

    public void CarregarJogo() //função de carregar o jogo
    {
        this.gameData = _gerenciador.Carregar(_IDSelecionado);

        if(this.gameData == null && _iniciarSalvamentoSeNulo)
        {
            NovoJogo();
        }

        if(this.gameData == null)
        {
            Debug.Log("Nenhum dado foi encontrado, Inicie um novo jogo");
            return;
        }

        foreach(Salvamento objetosDeSalvamento in _objetosDeSalvamento)
        {
            objetosDeSalvamento.CarregarSave(gameData);
        }
    }

    public void SalvarJogo() //função de salvar o jogo
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("Nenhum dado foi encontrado, um novo jogo precisa ser iniciado antes dos dados poderem ser salvos");
            return;
        }

        foreach (Salvamento objetosDeSalvamento in _objetosDeSalvamento)
        {
            objetosDeSalvamento.SalvarSave(gameData);
        }

        _gerenciador.Salvar(gameData, _IDSelecionado);
    }

    private List<Salvamento> EncontrarObjetosDeSalvamento()
    {
        IEnumerable<Salvamento> objetosDeSalvamento = FindObjectsOfType<MonoBehaviour>(true).OfType<Salvamento>();

        return new List<Salvamento>(objetosDeSalvamento);
    }

    public bool TemDadosDeSave()
    {
        return gameData != null;
    }

    public Dictionary<string, GameData> EncontrarTodosOsPerfisDeDadosDoSave() //função para encontrar todos os perfis de dados do save
    {
        return _gerenciador.CarregarTodosOsDados(); //carrega todos os dados
    }

    private void OnApplicationQuit()
    {
        //salva o jogo quando sair do game
        SalvarJogo();
    }
}
