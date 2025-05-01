using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesBase : MonoBehaviour
{
    public string tituloHabilidade; //titulo da habilidade
    public string descrição; //descrição da habilidade
    public float tempoDeRecarga; //tempo de recarga da habilidade

    public void AtivarEfeito() //função que ativa o efeito da habilidade
    {
        Debug.Log(tituloHabilidade + "ativar efeito");
    }
}
