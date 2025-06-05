using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciadorDePersonagens : MonoBehaviour, Salvamento
{
    public List<PersonagemData> personagens = new List<PersonagemData>(); //lista de personagens criados

    public IAPersonagemBase[] personagem; //personagens do jogador

    private int personagensSelecionados; //valor do n�mero de personagens selecionados
    public void CarregarSave(GameData data) //fun��o de carregar os dados salvos do gerenciador de personagens
    {
        personagens = data.personagens;
    }

    public void SalvarSave(GameData data) //fun��o de salvar os dados do gerenciador de personagens
    {

    }

    public void SelecionarPersonagem(PersonagemData personagemBase)
    {
        personagem[personagensSelecionados].personagem = personagemBase;
        personagensSelecionados++;
    }
}
