using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoEquipamento{ CabecaAcessorio, CabecaTopo, CabecaMedio, CabecaBaixo, MaoEsquerda, MaoDireita, Armadura, Bracadeira, Botas, Acessorio1, Acessorio2, BuffTemporario}

[System.Serializable]
public class EquipamentoBase : MonoBehaviour
{
    [Header("Dados")] //�rea referente aos dados do equipamento
    public string nome; //nome do equipamento
    public TipoEquipamento tipoEquipamento; //tipo de equipamento
    public Sprite icone; //�cone do equipamento
    [TextArea(3, 10)]
    public string descricao; //descri��o do equipamento
    public string descricaoCurta; //curta descri��o do equipamento
    public int indice; //indice do item nos equipamentos do personagem
    public string id; //ID do equipamento

    [Header("Modificadores")] //�rea referente aos modificadores do equipamento
    public float multiplicadorAtaque; //valor do multiplicador de ataque
    public float chanceCritica; //valor da chance cr�tica
    public float defesa; //valor da defesa
    public float defesaMagica; //valor da defesa m�gica
    public float velocidadeDeAtaque; //valor da velocidade de ataque
    public float velocidadeDeMovimento; //valor da velocidade de movimento
    public float hpRegeneracao; //valor da regenera��o do hp
    public float spRegeneracao; //valor da regenera��o do sp
    public float ranged; //valor do ranged
    public bool buffConsumivel; //verifica se o equipamento � um consumivel

    [HideInInspector]
    public PersonagemData personagem; //personagem que possui o item equipado

    public void AplicarEfeito() //fun��o que aplica o efeito do equipamento ao personagem
    {
        personagem.multiplicadorAtaque += multiplicadorAtaque;
        personagem.chanceCritico += chanceCritica;
        personagem.velocidadeDeAtaque += velocidadeDeAtaque;
        personagem.ranged += ranged;

        personagem.defesa += defesa;
        personagem.defesaMagica += defesaMagica;

        personagem.velocidadeDeMovimento += velocidadeDeMovimento;

        personagem.hpRegeneracao += hpRegeneracao;
        personagem.spRegeneracao += spRegeneracao;
    }

    public void RemoverEfeito() //fun��o que remove o efeito do equipamento do personagem
    {
        personagem.multiplicadorAtaque -= multiplicadorAtaque;
        personagem.chanceCritico -= chanceCritica;
        personagem.velocidadeDeAtaque -= velocidadeDeAtaque;
        personagem.ranged -= ranged;

        personagem.defesa -= defesa;
        personagem.defesaMagica -= defesaMagica;

        personagem.velocidadeDeMovimento -= velocidadeDeMovimento;

        personagem.hpRegeneracao -= hpRegeneracao;
        personagem.spRegeneracao -= spRegeneracao;
    }
}
