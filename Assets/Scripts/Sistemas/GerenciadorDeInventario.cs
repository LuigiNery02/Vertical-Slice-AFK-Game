using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciadorDeInventario : MonoBehaviour
{
    public static GerenciadorDeInventario instancia { get; private set; }

    [Header("Mensagem")]
    public GameObject mensagemObjeto; //objeto da mensagem
    public Text textoMensagem; //texto da mensagem
    public float duracao; //dura��o da mensagem na tela

    [Header("Lista de Habilidades de Classe")]
    public List<HabilidadeBase> habilidadesClasseGuerreiro = new List<HabilidadeBase>(); //lista de habilidades da classe guerreiro
    public List<HabilidadeBase> habilidadesClasseArqueiro = new List<HabilidadeBase>(); //lista de habilidades da classe arqueiro
    public List<HabilidadeBase> habilidadesClasseMago = new List<HabilidadeBase>(); //lista de habilidades da classe mago
    public List<HabilidadeBase> habilidadesEspada = new List<HabilidadeBase>(); //lista de habilidades da espada

    private Transform canvasAtual; //canvas atual da cena

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

    public void CarregandoCena(Scene cena, LoadSceneMode modo) //fun��o chamada quando carregar outra cena
    {
        StopAllCoroutines();
        mensagemObjeto.SetActive(false);
        mensagemObjeto.transform.SetParent(this.transform);
        canvasAtual = GameObject.FindObjectOfType<Canvas>()?.transform; //encontra o canvas na cena
    }

    public void SortearHabilidade(TipoDeHabilidade tipo, Classe classe, int nivel ,PersonagemData personagem) //fun��o que sorteia uma habilidade para o personagem
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
            HabilidadeBase habilidadeExistente = personagem.listaDeHabilidadesDeClasse.Find(id => id.idHabilidade == habilidadeSorteada.idHabilidade); //verifica so o personagem j� possui a habilidade

            if(habilidadeExistente != null) //caso o personagem j� a possua a habilidade sorteada
            {
                //atualiza o n�vel da habilidade existente do personagem
                if(nivel > habilidadeExistente.nivel)
                {
                    habilidadeExistente.nivel = nivel;
                    MostrarMensagem("Habilidade Evolu�da: " + habilidadeExistente.nome + "\nN�vel: " + habilidadeExistente.nivel + "\nHer�i: " + personagem.apelido);
                }
            }
            else //do contr�rio
            {
                //adiciona a habilidade sorteada ao invent�rio do personagem
                habilidadeSorteada.nivel = nivel;
                personagem.listaDeHabilidadesDeClasse.Add(habilidadeSorteada);
                MostrarMensagem("Habilidade Adquir�da: " + habilidadeSorteada.nome + "\nN�vel: " + habilidadeSorteada.nivel + "\nHer�i: " + personagem.apelido);
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
            }

            HabilidadeBase habilidadeSorteada = listaArmaOriginal[Random.Range(0, listaArmaOriginal.Count)]; //sorteia uma habilidade
            HabilidadeBase habilidadeExistente = personagem.listaDeHabilidadesDeArma.Find(id => id.idHabilidade == habilidadeSorteada.idHabilidade); //verifica so o personagem j� possui a habilidade

            if (habilidadeExistente != null) //caso o personagem j� a possua a habilidade sorteada
            {
                //atualiza o n�vel da habilidade existente do personagem
                if (nivel > habilidadeExistente.nivel)
                {
                    habilidadeExistente.nivel = nivel;
                    MostrarMensagem("Habilidade Evolu�da: " + habilidadeExistente.nome + "\nN�vel: " + habilidadeExistente.nivel + "\nHer�i: " + personagem.apelido);
                }
            }
            else //do contr�rio
            {
                //adiciona a habilidade sorteada ao invent�rio do personagem
                habilidadeSorteada.nivel = nivel;
                personagem.listaDeHabilidadesDeArma.Add(habilidadeSorteada);
                MostrarMensagem("Habilidade Adquir�da: " + habilidadeSorteada.nome + "\nN�vel: " + habilidadeSorteada.nivel + "\nHer�i: " + personagem.apelido);
            }
        }
    }

    public void MostrarMensagem(string mensagem) //fun��o que mostra a mensagem na tela
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
    }

    private IEnumerator DesativarMensagem() //coroutine que desativa a mensagem
    {
        yield return new WaitForSeconds(duracao);

        mensagemObjeto.SetActive(false);
        mensagemObjeto.transform.SetParent(this.transform);
    }
}
