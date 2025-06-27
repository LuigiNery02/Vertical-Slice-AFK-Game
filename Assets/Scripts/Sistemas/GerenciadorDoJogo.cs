using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

sealed class GerenciadorDoJogo : MonoBehaviour
{
    public void CarregarCena(string cena) //fun��o de carregar uma cena do jogo
    {
        SceneManager.LoadScene(cena);
    }

    public void SalvarJogo() //fun��o que salva o jogo
    {
        if(SistemaDeSalvamento.instancia != null)
        {
            SistemaDeSalvamento.instancia.SalvarJogo();
        }
    }

    public void DeletarSave() //fun��o que deleta o save
    {
        if(SistemaDeSalvamento.instancia == null)
        {
            SistemaDeSalvamento.instancia.DeletarDados("save");
        }
        CarregarCena("TelaInicial");
    }

    public void SairDoJogo() //fun��o para sair do jogo
    {
        Application.Quit();
    }
}
