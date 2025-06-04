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
    public List<HabilidadeBase> habilidadesClasseArqueiro = new List<HabilidadeBase>(); //lista de habilidades da classe arqueiro
    public List<HabilidadeBase> habilidadesClasseMago = new List<HabilidadeBase>(); //lista de habilidades da classe mago

    [Header("Lista de Habilidades de Arma: Guerreiro")]
    public List<HabilidadeBase> habilidadesEspada = new List<HabilidadeBase>(); //lista de habilidades da espada
    public List<HabilidadeBase> habilidadesLanca = new List<HabilidadeBase>(); //lista de habilidades da lança
    public List<HabilidadeBase> habilidadesMachado = new List<HabilidadeBase>(); //lista de habilidades da machado

    [Header("Lista de Habilidades de Arma: Arqueiro")]
    public List<HabilidadeBase> habilidadesArcoFogo = new List<HabilidadeBase>(); //lista de habilidades do arco de fogo
    public List<HabilidadeBase> habilidadesArcoGelo = new List<HabilidadeBase>(); //lista de habilidades do arco de gelo
    public List<HabilidadeBase> habilidadesArcoVenenoso = new List<HabilidadeBase>(); //lista de habilidades do arco venenoso

    [Header("Lista de Habilidades de Arma: Mago")]
    public List<HabilidadeBase> habilidadesCajadoFogo = new List<HabilidadeBase>(); //lista de habilidades do cajado de fogo
    public List<HabilidadeBase> habilidadesCajadoGelo = new List<HabilidadeBase>(); //lista de habilidades do cajado de gelo
    public List<HabilidadeBase> habilidadesCajadoVenenoso = new List<HabilidadeBase>(); //lista de habilidades do cajado venenoso

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

    private Transform canvasAtual; //canvas atual da cena

    public void CarregarSave(GameData data)
    {
        equipouEquipamento = data.gerenciadorInventarioEquipado;

        if (equipouEquipamento)
        {
            equipamentosCabecaAcessorio = data.gerenciadorInventarioCabecaAcessorio;
            equipamentosCabecaTopo = data.gerenciadorInventarioCabecaTopo;
            equipamentosCabecaMedio = data.gerenciadorInventarioCabecaMedio;
            equipamentosCabecaBaixo = data.gerenciadorInventarioCabecaBaixo;
            equipamentosArmadura = data.gerenciadorInventarioArmadura;
            equipamentosBracadeira = data.gerenciadorInventarioBracadeira;
            equipamentosMaoEsquerda = data.gerenciadorInventarioMaoEsquerda;
            equipamentosMaoDireita = data.gerenciadorInventarioMaoDireita;
            equipamentosBota = data.gerenciadorInventarioBota;
            equipamentosAcessorio1 = data.gerenciadorInventarioAcessorio1;
            equipamentosAcessorio2 = data.gerenciadorInventarioAcessorio2;
            equipamentosBuffConsumivel = data.gerenciadorInventarioBuffConsumivel;
        }
    }

    public void SalvarSave(GameData data)
    {
        data.gerenciadorInventarioCabecaAcessorio = equipamentosCabecaAcessorio;
        data.gerenciadorInventarioCabecaTopo = equipamentosCabecaTopo;
        data.gerenciadorInventarioCabecaMedio = equipamentosCabecaMedio;
        data.gerenciadorInventarioCabecaBaixo = equipamentosCabecaBaixo;
        data.gerenciadorInventarioArmadura = equipamentosArmadura;
        data.gerenciadorInventarioBracadeira = equipamentosBracadeira;
        data.gerenciadorInventarioMaoEsquerda = equipamentosMaoEsquerda;
        data.gerenciadorInventarioMaoDireita = equipamentosMaoDireita;
        data.gerenciadorInventarioBota = equipamentosBota;
        data.gerenciadorInventarioAcessorio1 = equipamentosAcessorio1;
        data.gerenciadorInventarioAcessorio2 = equipamentosAcessorio2;
        data.gerenciadorInventarioBuffConsumivel = equipamentosBuffConsumivel;

        data.gerenciadorInventarioEquipado = equipouEquipamento;
    }

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

    public void CarregandoCena(Scene cena, LoadSceneMode modo) //função chamada quando carregar outra cena
    {
        StopAllCoroutines();
        mensagemObjeto.SetActive(false);
        mensagemObjeto.transform.SetParent(this.transform);
        canvasAtual = GameObject.FindObjectOfType<Canvas>()?.transform; //encontra o canvas na cena
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
                case Classe.Arqueiro:
                    listaClasseOriginal = habilidadesClasseArqueiro;
                    break;
                case Classe.Mago:
                    listaClasseOriginal = habilidadesClasseMago;
                    break;
            }

            HabilidadeBase habilidadeSorteada = listaClasseOriginal[Random.Range(0, listaClasseOriginal.Count)]; //sorteia uma habilidade
            HabilidadeBase habilidadeExistente = personagem.listaDeHabilidadesDeClasse.Find(id => id.idHabilidade == habilidadeSorteada.idHabilidade); //verifica so o personagem já possui a habilidade

            if(habilidadeExistente != null) //caso o personagem já a possua a habilidade sorteada
            {
                //atualiza o nível da habilidade existente do personagem
                if(nivel > habilidadeExistente.nivel)
                {
                    habilidadeExistente.nivel = nivel;
                    MostrarMensagem("Habilidade Evoluída: " + habilidadeExistente.nome + "\nNível: " + habilidadeExistente.nivel + "\nHerói: " + personagem.apelido);
                }
            }
            else //do contrário
            {
                //adiciona a habilidade sorteada ao inventário do personagem
                habilidadeSorteada.nivel = nivel;
                personagem.listaDeHabilidadesDeClasse.Add(habilidadeSorteada);
                personagem.idsHabilidadesDeClasse.Add(habilidadeSorteada.idHabilidade);
                MostrarMensagem("Habilidade Adquirída: " + habilidadeSorteada.nome + "\nNível: " + habilidadeSorteada.nivel + "\nHerói: " + personagem.apelido);
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
                case "Lança":
                    listaArmaOriginal = habilidadesLanca;
                    break;
                case "Machado":
                    listaArmaOriginal = habilidadesMachado;
                    break;
                case "Arco de Fogo":
                    listaArmaOriginal = habilidadesArcoFogo;
                    break;
                case "Arco de Gelo":
                    listaArmaOriginal = habilidadesArcoGelo;
                    break;
                case "Arco Venenoso":
                    listaArmaOriginal = habilidadesArcoVenenoso;
                    break;
                case "Cajado de Fogo":
                    listaArmaOriginal = habilidadesCajadoFogo; 
                    break;
                case "Cajado de Gelo":
                    listaArmaOriginal = habilidadesCajadoGelo;
                    break;
                case "Cajado Venenoso":
                    listaArmaOriginal = habilidadesCajadoVenenoso;
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
                    MostrarMensagem("Habilidade Evoluída: " + habilidadeExistente.nome + "\nNível: " + habilidadeExistente.nivel + "\nHerói: " + personagem.apelido);
                }
            }
            else //do contrário
            {
                //adiciona a habilidade sorteada ao inventário do personagem
                habilidadeSorteada.nivel = nivel;
                personagem.listaDeHabilidadesDeArma.Add(habilidadeSorteada);
                personagem.idsHabilidadesDeArma.Add(habilidadeSorteada.idHabilidade);
                MostrarMensagem("Habilidade Adquirída: " + habilidadeSorteada.nome + "\nNível: " + habilidadeSorteada.nivel + "\nHerói: " + personagem.apelido);
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
}
