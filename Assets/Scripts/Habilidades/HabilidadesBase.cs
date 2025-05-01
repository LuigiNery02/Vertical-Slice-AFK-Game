using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesBase : MonoBehaviour
{
    public string tituloHabilidade; //titulo da habilidade
    [TextArea]
    public string descri��o; //descri��o da habilidade
    public bool temTempoDeEfeito; //vari�vel que define se o personagem possui tempo de efeito
    public float tempoDeEfeito; //tempo do efeito da habilidade
    public float tempoDeRecarga; //tempo de recarga da habilidade

    //[HideInInspector]
    public bool podeAtivarEfeito = true; //vari�vel que determina se pode ou n�o ativar o efeito

    public IAPersonagemBase personagem; //personagem que utilizar� a habilidade

    public delegate void delegateEfeito();
    public delegateEfeito efeitoHabilidade; //fun��o do efeito de cada habilidade

    public delegate void delegateRemoverEfeito();
    public delegateEfeito removerEfeitoHabilidade; //fun��o de desativar o efeito de cada habilidade

    public void AtivarEfeito() //fun��o que ativa o efeito da habilidade
    {
        if (podeAtivarEfeito)
        {
            podeAtivarEfeito = false;
            efeitoHabilidade();
            if (temTempoDeEfeito)
            {
                StartCoroutine(TempoDeEfeito());
            }
        }
    }

    IEnumerator TempoDeEfeito() //fun��o que conta em segundos o tempo que a habilidade est� ativa
    {
        yield return new WaitForSeconds(tempoDeEfeito);
        removerEfeitoHabilidade();
        StartCoroutine(TempoDeRecargaDoEfeito());
    }

    public void RemoverEfeito() //fun��o que desativa o efeito da habilidade que n�o possui tempo de efeito
    {
        removerEfeitoHabilidade();
        StartCoroutine(TempoDeRecargaDoEfeito());
    }

    public void RemoverEfeitoExternamente() //fun��o que remove o efeito da habilidade de forma externa
    {
        removerEfeitoHabilidade();
    }

    IEnumerator TempoDeRecargaDoEfeito() //fun��o que conta em segundos o tempo para recaregar a habilidade
    {
        yield return new WaitForSeconds(tempoDeRecarga);
        podeAtivarEfeito = true;
    }
}
