using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

sealed class GerenciadorDoJogo : MonoBehaviour
{
    public void CarregarCena(string cena) //função de carregar uma cena do jogo
    {
        SceneManager.LoadScene(cena);
    }
    public void SairDoJogo() //função para sair do jogo
    {
        Application.Quit();
    }
}
