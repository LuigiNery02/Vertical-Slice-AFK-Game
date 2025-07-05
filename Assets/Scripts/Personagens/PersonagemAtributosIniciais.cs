using UnityEngine;

public class PersonagemAtributosIniciais : MonoBehaviour
{
    //�rea referente ao hp do personagem
    [Header("HP")]
    public float hpBaseGuerreiro = 1; //valor base do HP da classe guerreiro
    public float hpBaseLadino = 1; //valor base do HP da classe ladino
    public float hpBaseElementalista = 1; //valor base do HP da classe elementalista
    public float hpBaseSacerdote = 1; //valor base do HP da classe sacerdote

    //�rea referente ao sp (pontos de habilidade) do personagem
    [Header("SP")]
    public float spBaseGuerreiro; //valor base do SP da classe guerreiro
    public float spBaseLadino; //valor base do SP da classe ladino
    public float spBaseElementalista; //valor base do SP da classe elementalista
    public float spBaseSacerdote; //valor base do SP da classe sacerdote

    //�rea referente ao ataque do personagem
    [Header("Ataque")]
    public float precisaoBase; //valor base da precis�o
    public float multiplicadorCritico = 2; //valor do multiplicador do cr�tico
    public float rangedBase; //valor base do ranged do personagem

    //�rea referente � esquiva do personagem
    [Header("Esquiva")]
    public float esquivaBase; //valor base da esquiva do persongem

    //�rea referente ao fator classe do personagem
    [Header("Fator Classe")]
    public float fatorClasseGuerreiro = 1; //fator da classe guerreiro
    public float fatorClasseLadino = 1; //fator da classe ladino
    public float fatorClasseElementalista = 1; //fator da classe elementalista
    public float fatorClasseSacerdote = 1; //fator da classe sacerdote

    //�rea referente ao movimento do personagem
    [Header("Velocidade de Movimento")]
    public float velocidadeDeMovimentoBase; //velocidade de movimento base do personagem
}
