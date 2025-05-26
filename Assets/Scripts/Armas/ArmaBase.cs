using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ArmaBase: MonoBehaviour
{
    public string nome; //nome da arma
    public Classe armaClasse; //classe do personagem ao qual à arma pertence
    public float dano; //dano da arma
    public float danoMagico; //dano mágico da arma
    public float danoDistancia; //dano à distância
    public float velocidadeDeAtaque; //velocidade de ataque da arma
}
