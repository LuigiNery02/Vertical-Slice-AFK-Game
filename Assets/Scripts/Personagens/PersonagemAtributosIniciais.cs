using UnityEngine;

public class PersonagemAtributosIniciais : MonoBehaviour
{
    //área referente ao hp do personagem
    [Header("HP")]
    public float hpBaseGuerreiro = 1; //valor base do HP da classe guerreiro
    public float hpBaseLadino = 1; //valor base do HP da classe ladino
    public float hpBaseElementalista = 1; //valor base do HP da classe elementalista
    public float hpBaseSacerdote = 1; //valor base do HP da classe sacerdote

    //área referente ao sp (pontos de habilidade) do personagem
    [Header("SP")]
    public float spBaseGuerreiro; //valor base do SP da classe guerreiro
    public float spBaseLadino; //valor base do SP da classe ladino
    public float spBaseElementalista; //valor base do SP da classe elementalista
    public float spBaseSacerdote; //valor base do SP da classe sacerdote

    //área referente ao ataque do personagem
    [Header("Ataque")]
    public float precisaoBase; //valor base da precisão
    public float multiplicadorCritico = 2; //valor do multiplicador do crítico
    public float rangedBase; //valor base do ranged do personagem

    //área referente à esquiva do personagem
    [Header("Esquiva")]
    public float esquivaBase; //valor base da esquiva do persongem

    //área referente ao fator classe do personagem
    [Header("Fator Classe")]
    public float fatorClasseGuerreiro = 1; //fator da classe guerreiro
    public float fatorClasseLadino = 1; //fator da classe ladino
    public float fatorClasseElementalista = 1; //fator da classe elementalista
    public float fatorClasseSacerdote = 1; //fator da classe sacerdote

    //área referente ao movimento do personagem
    [Header("Velocidade de Movimento")]
    public float velocidadeDeMovimentoBase; //velocidade de movimento base do personagem
}
