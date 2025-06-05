using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DadosHabilidade
{
    public string idHabilidade; //id da habilidade
    public int nivel; //nivel da habilidade

    public DadosHabilidade(string id, int nivel)
    {
        this.idHabilidade = id;
        this.nivel = nivel;
    }
}
