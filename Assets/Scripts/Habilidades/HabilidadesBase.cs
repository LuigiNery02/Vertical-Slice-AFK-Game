using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesBase : MonoBehaviour
{
    public string tituloHabilidade; //titulo da habilidade
    public string descri��o; //descri��o da habilidade
    public float tempoDeRecarga; //tempo de recarga da habilidade

    public void AtivarEfeito() //fun��o que ativa o efeito da habilidade
    {
        Debug.Log(tituloHabilidade + "ativar efeito");
    }
}
