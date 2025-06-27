using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoEquipamento{ CabecaAcessorio, CabecaTopo, CabecaMedio, CabecaBaixo, MaoEsquerda, MaoDireita, Armadura, Bracadeira, Botas, Acessorio1, Acessorio2, BuffTemporario}

[System.Serializable]
public class EquipamentoBase : MonoBehaviour
{
    [Header("Dados")] //Área referente aos dados do equipamento
    public string nome; //nome do equipamento
    public TipoEquipamento tipoEquipamento; //tipo de equipamento
    public Sprite icone; //ícone do equipamento
    [TextArea(3, 10)]
    public string descricao; //descrição do equipamento
    public string descricaoCurta; //curta descrição do equipamento
    public int indice; //indice do item nos equipamentos do personagem
    public string id; //ID do equipamento

    [Header("Modificadores")] //Área referente aos modificadores do equipamento
    public float ataqueMelee; //valor do dano de ataque melee
    public float ataqueDistancia; //valor do dano de ataque à distância
    public float ataqueMagico; //valor do dano de ataque mágico
    public float defesa; //valor da defesa
    public float defesaMagica; //valor da defesa mágica
    public float velocidadeDeMovimento; //valor da velocidade de movimento
    public int precisao; //valor da precisão
    public float hp; //valor do hp
    public bool buffConsumivel; //verifica se o equipamento é um consumivel

    [HideInInspector]
    public PersonagemData personagem; //personagem que possui o item equipado

    public void AplicarEfeito() //função que aplica o efeito do equipamento ao personagem
    {
        personagem.ataque += ataqueMelee;
        //personagem.ataqueMagico += ataqueMagico;
        //personagem.ataqueDistancia += ataqueDistancia;

        personagem.defesa += defesa;
        personagem.defesaMagica += defesaMagica;

        //personagem.velocidadeMovimento += velocidadeDeMovimento;

        personagem.precisao += precisao;

        personagem.hp += hp;
    }

    public void RemoverEfeito() //função que remove o efeito do equipamento do personagem
    {
        personagem.ataque -= ataqueMelee;
        //personagem.ataqueMagico -= ataqueMagico;
        //personagem.ataqueDistancia -= ataqueDistancia;

        personagem.defesa -= defesa;
        personagem.defesaMagica -= defesaMagica;

        //personagem.velocidadeMovimento -= velocidadeDeMovimento;

        personagem.precisao -= precisao;

        personagem.hp -= hp;
    }
}
