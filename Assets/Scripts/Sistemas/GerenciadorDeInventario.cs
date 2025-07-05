using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorDeInventario : MonoBehaviour, Salvamento
{
    public static GerenciadorDeInventario instancia { get; private set; }

    [Header("Mensagem")]
    public GameObject mensagemObjeto; //objeto da mensagem
    public Text textoMensagem; //texto da mensagem
    public float duracao; //duração da mensagem na tela

    [Header("Lista de Habilidades de Classe")]
    public List<HabilidadeBase> habilidadesClasseGuerreiro = new List<HabilidadeBase>(); //lista de habilidades da classe guerreiro
    public List<HabilidadeBase> habilidadesClasseLadino = new List<HabilidadeBase>(); //lista de habilidades da classe ladino
    public List<HabilidadeBase> habilidadesClasseElementalista = new List<HabilidadeBase>(); //lista de habilidades da classe elementalista
    public List<HabilidadeBase> habilidadesClasseSacerdote = new List<HabilidadeBase>(); //lista de habilidades da classe sacerdote

    [Header("Lista de Habilidades de Arma")]
    public List<HabilidadeBase> habilidadesEspada = new List<HabilidadeBase>(); //lista de habilidades da espada
    public List<HabilidadeBase> habilidadesArco = new List<HabilidadeBase>(); //lista de habilidades do arco
    public List<HabilidadeBase> habilidadesLivro = new List<HabilidadeBase>(); //lista de habilidades do livro

    [Header("Lista Equipamentos")]
    public List<EquipamentoBase> equipamentosCabecaAcessorio = new List<EquipamentoBase>(); //lista de equipamentos da cabeça acessório
    public List<EquipamentoBase> equipamentosCabecaTopo = new List<EquipamentoBase>(); //lista de equipamentos da cabeça topo
    public List<EquipamentoBase> equipamentosCabecaMedio = new List<EquipamentoBase>(); //lista de equipamentos da cabeça médio
    public List<EquipamentoBase> equipamentosCabecaBaixo = new List<EquipamentoBase>(); //lista de equipamentos da cabeça baixo
    public List<EquipamentoBase> equipamentosArmadura = new List<EquipamentoBase>(); //lista de equipamentos da armadura
    public List<EquipamentoBase> equipamentosBracadeira = new List<EquipamentoBase>(); //lista de equipamentos da braçadeira
    public List<EquipamentoBase> equipamentosMaoEsquerda = new List<EquipamentoBase>(); //lista de equipamentos da mão esquerda
    public List<EquipamentoBase> equipamentosMaoDireita = new List<EquipamentoBase>(); //lista de equipamentos da mão direita
    public List<EquipamentoBase> equipamentosBota = new List<EquipamentoBase>(); //lista de equipamentos da bota
    public List<EquipamentoBase> equipamentosAcessorio1 = new List<EquipamentoBase>(); //lista de equipamentos do acessório 1
    public List<EquipamentoBase> equipamentosAcessorio2 = new List<EquipamentoBase>(); //lista de equipamentos do acessório 2
    public List<EquipamentoBase> equipamentosBuffConsumivel = new List<EquipamentoBase>(); //lista de equipamentos do buff de consumível 

    [HideInInspector]
    public bool equipouEquipamento; //variável que checa se qualquer equipamento já foi equipado a qualquer personagem

    //[HideInInspector]
    public List<PersonagemData> personagensCriados = new List<PersonagemData>();

    private SistemaDeCriacaoDePersonagens _sistemaDeCriacaoDePersonagens;
    private Dictionary<string, EquipamentoBase> _equipamentosPorID = new Dictionary<string, EquipamentoBase>();
    private Transform canvasAtual; //canvas atual da cena

    #region Salvamento
    public void CarregarSave(GameData data)
    {
        equipouEquipamento = data.gerenciadorInventarioEquipado;

        if (equipouEquipamento)
        {
            if(data.gerenciadorInventarioCabecaAcessorioID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosCabecaAcessorio);
                equipamentosCabecaAcessorio = ConverterIDs(data.gerenciadorInventarioCabecaAcessorioID);
            }

            if (data.gerenciadorInventarioCabecaTopoID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosCabecaTopo);
                equipamentosCabecaTopo = ConverterIDs(data.gerenciadorInventarioCabecaTopoID);
            }

            if (data.gerenciadorInventarioCabecaMedioID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosCabecaMedio);
                equipamentosCabecaMedio = ConverterIDs(data.gerenciadorInventarioCabecaMedioID);
            }

            if (data.gerenciadorInventarioCabecaBaixoID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosCabecaBaixo);
                equipamentosCabecaBaixo = ConverterIDs(data.gerenciadorInventarioCabecaBaixoID);
            }

            if (data.gerenciadorInventarioArmaduraID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosArmadura);
                equipamentosArmadura = ConverterIDs(data.gerenciadorInventarioArmaduraID);
            }

            if (data.gerenciadorInventarioBracadeiraID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosBracadeira);
                equipamentosBracadeira = ConverterIDs(data.gerenciadorInventarioBracadeiraID);
            }

            if (data.gerenciadorInventarioMaoEsquerdaID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosMaoEsquerda);
                equipamentosMaoEsquerda = ConverterIDs(data.gerenciadorInventarioMaoEsquerdaID);
            }

            if (data.gerenciadorInventarioMaoDireitaID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosMaoDireita);
                equipamentosMaoDireita = ConverterIDs(data.gerenciadorInventarioMaoDireitaID);
            }

            if (data.gerenciadorInventarioBotaID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosBota);
                equipamentosBota = ConverterIDs(data.gerenciadorInventarioBotaID);
            }

            if (data.gerenciadorInventarioAcessorio1ID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosAcessorio1);
                equipamentosAcessorio1 = ConverterIDs(data.gerenciadorInventarioAcessorio1ID);
            }

            if (data.gerenciadorInventarioAcessorio2ID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosAcessorio2);
                equipamentosAcessorio2 = ConverterIDs(data.gerenciadorInventarioAcessorio2ID);
            }

            if (data.gerenciadorInventarioBuffConsumivelID.Count == 0)
            {
                AdicionarListaAoDicionario(equipamentosBuffConsumivel);
                equipamentosBuffConsumivel = ConverterIDs(data.gerenciadorInventarioBuffConsumivelID);
            }
        }
    }

    public void SalvarSave(GameData data)
    {
        data.gerenciadorInventarioCabecaAcessorioID = ExtrairIDs(equipamentosCabecaAcessorio);
        data.gerenciadorInventarioCabecaTopoID = ExtrairIDs(equipamentosCabecaTopo);
        data.gerenciadorInventarioCabecaMedioID = ExtrairIDs(equipamentosCabecaMedio);
        data.gerenciadorInventarioCabecaBaixoID = ExtrairIDs(equipamentosCabecaBaixo);
        data.gerenciadorInventarioArmaduraID = ExtrairIDs(equipamentosArmadura);
        data.gerenciadorInventarioBracadeiraID = ExtrairIDs(equipamentosBracadeira);
        data.gerenciadorInventarioMaoEsquerdaID = ExtrairIDs(equipamentosMaoEsquerda);
        data.gerenciadorInventarioMaoDireitaID = ExtrairIDs(equipamentosMaoDireita);
        data.gerenciadorInventarioBotaID = ExtrairIDs(equipamentosBota);
        data.gerenciadorInventarioAcessorio1ID = ExtrairIDs(equipamentosAcessorio1);
        data.gerenciadorInventarioAcessorio2ID = ExtrairIDs(equipamentosAcessorio2);
        data.gerenciadorInventarioBuffConsumivelID = ExtrairIDs(equipamentosBuffConsumivel);

        data.gerenciadorInventarioEquipado = equipouEquipamento;

        data.personagens = personagensCriados;
    }

    private List<string> ExtrairIDs(List<EquipamentoBase> lista) //extrai os IDs
    {
        List<string> ids = new List<string>();

        if(lista == null)
        {
            return ids;
        }
        foreach (var equipamento in lista)
        {
            ids.Add(equipamento.id);
        }
        return ids;
    }

    private List<EquipamentoBase> ConverterIDs(List<string> lista) //converte os IDs
    {
        List<EquipamentoBase> equipamentos = new List<EquipamentoBase>();
        foreach (var id in lista)
        {
            equipamentos.Add(equipamentos.Find(idEquipamento => idEquipamento.id == id));
        }
        return equipamentos;
    }
    #endregion

    private void Awake()
    {
        if(instancia != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instancia = this;
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += CarregandoCena;
    }

    private void Start()
    {
        _sistemaDeCriacaoDePersonagens = FindFirstObjectByType<SistemaDeCriacaoDePersonagens>();
        if(_sistemaDeCriacaoDePersonagens != null)
        {
            foreach (var personagem in _sistemaDeCriacaoDePersonagens.personagensCriados)
            {
                foreach (string id in personagem.idsEquipamentosEquipados)
                {
                    if (_equipamentosPorID.TryGetValue(id, out EquipamentoBase equipamento))
                    {
                        _sistemaDeCriacaoDePersonagens.DefinirEquipamento(equipamento);
                    }
                }
            }

            ReceberPersonagensCriados();
        }
    }

    public void ReceberPersonagensCriados()
    {
        personagensCriados = _sistemaDeCriacaoDePersonagens.personagensCriados;
    }

    public void CarregandoCena(Scene cena, LoadSceneMode modo) //função chamada quando carregar outra cena
    {
        StopAllCoroutines();
        mensagemObjeto.SetActive(false);
        mensagemObjeto.transform.SetParent(this.transform);
        canvasAtual = GameObject.Find("Canvas").transform; //encontra o canvas na cena
    }

    public void SortearHabilidade(TipoDeHabilidade tipo, Classe classe, int nivel ,PersonagemData personagem) //função que sorteia uma habilidade para o personagem
    {
        if(tipo == TipoDeHabilidade.Classe)
        {
            //define o sorteio da habilidade baseado na classe do personagem
            List<HabilidadeBase> listaClasseOriginal = null;

            switch (classe)
            {
                case Classe.Guerreiro:
                    listaClasseOriginal = habilidadesClasseGuerreiro;
                    break;
                case Classe.Ladino:
                    listaClasseOriginal = habilidadesClasseLadino;
                    break;
                case Classe.Elementalista:
                    listaClasseOriginal = habilidadesClasseElementalista;
                    break;
                case Classe.Sacerdote:
                    listaClasseOriginal = habilidadesClasseSacerdote;
                    break;
            }

            loop:
            HabilidadeBase habilidadeSorteada = listaClasseOriginal[Random.Range(0, listaClasseOriginal.Count)]; //sorteia uma habilidade
            HabilidadeBase habilidadeExistente = personagem.listaDeHabilidadesDeClasse.Find(id => id.idHabilidade == habilidadeSorteada.idHabilidade); //verifica so o personagem já possui a habilidade

            if(habilidadeExistente != null) //caso o personagem já a possua a habilidade sorteada
            {
                goto loop;
            }
            else //do contrário
            {
                //adiciona a habilidade sorteada ao inventário do personagem
                habilidadeSorteada.nivel = nivel;
                personagem.listaDeHabilidadesDeClasse.Add(habilidadeSorteada);
                personagem.habilidadesDeClasseSalvas.Add(new DadosHabilidade(habilidadeSorteada.idHabilidade, habilidadeSorteada.nivel));
                MostrarMensagem("Habilidade Adquirída: " + habilidadeSorteada.nome + "\nNível: " + habilidadeSorteada.nivel + "\nHerói: " + personagem.apelido + " - Nível: " + personagem.nivel);
            }
        }
        else if(tipo == TipoDeHabilidade.Arma)
        {
            //define o sorteio da habilidade 
            List<HabilidadeBase> listaArmaOriginal = null;

            switch (personagem.arma.nome)
            {
                case "Espada":
                    listaArmaOriginal = habilidadesEspada;
                    break;
                case "Arco":
                    listaArmaOriginal = habilidadesArco;
                    break;
                case "Livro":
                    listaArmaOriginal = habilidadesLivro;
                    break;
            }

            HabilidadeBase habilidadeSorteada = listaArmaOriginal[Random.Range(0, listaArmaOriginal.Count)]; //sorteia uma habilidade
            HabilidadeBase habilidadeExistente = personagem.listaDeHabilidadesDeArma.Find(id => id.idHabilidade == habilidadeSorteada.idHabilidade); //verifica so o personagem já possui a habilidade

            if (habilidadeExistente != null) //caso o personagem já a possua a habilidade sorteada
            {
                //atualiza o nível da habilidade existente do personagem
                if (nivel > habilidadeExistente.nivel)
                {
                    habilidadeExistente.nivel = nivel;
                    DadosHabilidade dadosExistente = personagem.habilidadesDeArmaSalvas.Find(id => id.idHabilidade == habilidadeExistente.idHabilidade);
                    if (dadosExistente != null)
                    {
                        dadosExistente.nivel = habilidadeExistente.nivel;
                    }
                    MostrarMensagem("Habilidade Evoluída: " + habilidadeExistente.nome + "\nNível: " + habilidadeExistente.nivel + "\nHerói: " + personagem.apelido + " - Nível: " + personagem.nivel);
                }
            }
            else //do contrário
            {
                //adiciona a habilidade sorteada ao inventário do personagem
                habilidadeSorteada.nivel = nivel;
                personagem.listaDeHabilidadesDeArma.Add(habilidadeSorteada);
                personagem.habilidadesDeArmaSalvas.Add(new DadosHabilidade(habilidadeSorteada.idHabilidade, habilidadeSorteada.nivel));
                MostrarMensagem("Habilidade Adquirída: " + habilidadeSorteada.nome + "\nNível: " + habilidadeSorteada.nivel + "\nHerói: " + personagem.apelido + " - Nível: " + personagem.nivel);
            }
        }
    }

    public void MostrarMensagem(string mensagem) //função que mostra a mensagem na tela
    {
        if(canvasAtual == null)
        {
            canvasAtual = GameObject.FindObjectOfType<Canvas>()?.transform;
            if (canvasAtual != null)
            {
                //define a mensagem como filho do canvas e o texto da mensagem 
                mensagemObjeto.transform.SetParent(canvasAtual, false);
                mensagemObjeto.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
                textoMensagem.text = mensagem;
                mensagemObjeto.SetActive(true);

                StopAllCoroutines();
                StartCoroutine(DesativarMensagem());
            }
        }
        else
        {
            //define a mensagem como filho do canvas e o texto da mensagem 
            mensagemObjeto.transform.SetParent(canvasAtual, false);
            mensagemObjeto.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            textoMensagem.text = mensagem;
            mensagemObjeto.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(DesativarMensagem());
        }
        mensagemObjeto.transform.localScale = Vector3.one;
    }

    private IEnumerator DesativarMensagem() //coroutine que desativa a mensagem
    {
        yield return new WaitForSeconds(duracao);

        mensagemObjeto.SetActive(false);
        mensagemObjeto.transform.SetParent(this.transform);
    }
    private void AdicionarListaAoDicionario(List<EquipamentoBase> lista) //função que adiciona os equipamentos ao dicionário
    {
        if (lista == null)
        {
            return;
        }

        foreach (var equipamento in lista)
        {
            if (equipamento != null && !_equipamentosPorID.ContainsKey(equipamento.id))
            {
                _equipamentosPorID.Add(equipamento.id, equipamento);
            }
        }
    }
}
