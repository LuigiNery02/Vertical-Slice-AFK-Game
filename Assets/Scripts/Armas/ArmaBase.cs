using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TipoDeDano { DANO_MELEE, DANO_RANGED, DANO_MAGICO } //tipo de dano da arma
public class ArmaBase: MonoBehaviour
{
    public string nome; //nome da arma
    public TipoDeArma armaTipo; //tipo de ataque da arma
    public TipoDeDano armaDano; //tipo de dano da arma
    public float dano; //valor do dano da arma
    public float velocidadeDoProjetil; //velocidade do projétil da arma
    public int id; //id da arma
}
