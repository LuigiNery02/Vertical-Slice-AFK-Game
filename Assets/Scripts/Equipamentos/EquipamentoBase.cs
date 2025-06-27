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
    public float ataqueMelee; //valor do dano de ataque melee
    public float ataqueDistancia; //valor do dano de ataque � dist�ncia
    public float ataqueMagico; //valor do dano de ataque m�gico
    public float defesa; //valor da defesa
    public float defesaMagica; //valor da defesa m�gica
    public float velocidadeDeMovimento; //valor da velocidade de movimento
    public int precisao; //valor da precis�o
    public float hp; //valor do hp
    public bool buffConsumivel; //verifica se o equipamento � um consumivel

    [HideInInspector]
    public PersonagemData personagem; //personagem que possui o item equipado

    public void AplicarEfeito() //fun��o que aplica o efeito do equipamento ao personagem
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

    public void RemoverEfeito() //fun��o que remove o efeito do equipamento do personagem
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
