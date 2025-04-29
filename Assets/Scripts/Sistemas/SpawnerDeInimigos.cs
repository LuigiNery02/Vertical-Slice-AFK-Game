using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class SpawnerDeInimigos : MonoBehaviour
{
    //Área referente aos inimigos
    [Header("Inimigos")]
    [SerializeField]
    private IAPersonagemBase[] _inimigos; //inimigos

    //Área referente aos valores de posições e rotação dos inimigos
    [Header("Valores")]
    [SerializeField]
    private float[] _posicoesY; //posições em Y de cada inimigo
    [SerializeField]
    private Vector2[] _posicoesXZ; //posições em X e Z dos inimigos
    [SerializeField]
    private float _rotacaoY; //rotação em y dos inimigos

    public void GerarInimigos() //função que gera aleatoriamente os inimigos
    {
        //desativa todos os inimigos na cena
        foreach (IAPersonagemBase personagem in FindObjectsOfType<IAPersonagemBase>())
        {
            if(personagem.controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO)
            {
                personagem.gameObject.SetActive(false);
            }
        }

        //sortear 3 inimigos
        for (int i = 0; i < 3; i++)
        {
            int sorteio = Random.Range(0, _inimigos.Length);
            string nomeAlvo = _inimigos[sorteio].gameObject.name;

            //procura um objeto com o mesmo nome e inativo
            GameObject instancia = null;
            foreach (IAPersonagemBase candidato in FindObjectsOfType<IAPersonagemBase>(true))
            {
                if (candidato.gameObject.name == nomeAlvo && !candidato.gameObject.activeSelf)
                {
                    instancia = candidato.gameObject;
                    break;
                }
            }

            if (instancia != null)
            {
                instancia.SetActive(true);
                //define a posição local com base no índice de ativação (i) e sorteio (sorteio)
                Vector3 novaPosicao = new Vector3(_posicoesXZ[i].x, _posicoesY[sorteio], _posicoesXZ[i].y);
                instancia.transform.localPosition = novaPosicao;

                //define rotação local em Y
                Vector3 novaRotacao = instancia.transform.localEulerAngles;
                novaRotacao.y = _rotacaoY;
                instancia.transform.localEulerAngles = novaRotacao;
            }
        }
    }

    public void ResetarPosicoesInimigos() //função que reseta a posição dos inimigos quando eles vencerem
    {
        int contador = 0;

        foreach (IAPersonagemBase personagem in FindObjectsOfType<IAPersonagemBase>())
        {
            if (personagem.controlador == ControladorDoPersonagem.PERSONAGEM_INIMIGO && personagem.gameObject.activeSelf)
            {
                //ajusta as posições
                float y = personagem.transform.localPosition.y;

                Vector3 novaPosicao = new Vector3(_posicoesXZ[contador].x, y, _posicoesXZ[contador].y);
                personagem.transform.localPosition = novaPosicao;

                //define rotação local em Y
                Vector3 novaRotacao = personagem.transform.localEulerAngles;
                novaRotacao.y = _rotacaoY;
                personagem.transform.localEulerAngles = novaRotacao;

                contador++;
            }
        }
    }

}
