using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadesBase : MonoBehaviour
{
    public string tituloHabilidade; //titulo da habilidade
    [TextArea]
    public string descrição; //descrição da habilidade
    public bool temTempoDeEfeito; //variável que define se o personagem possui tempo de efeito
    public float tempoDeEfeito; //tempo do efeito da habilidade
    public float tempoDeRecarga; //tempo de recarga da habilidade

    //[HideInInspector]
    public bool podeAtivarEfeito = true; //variável que determina se pode ou não ativar o efeito

    public IAPersonagemBase personagem; //personagem que utilizará a habilidade

    public delegate void delegateEfeito();
    public delegateEfeito efeitoHabilidade; //função do efeito de cada habilidade

    public delegate void delegateRemoverEfeito();
    public delegateEfeito removerEfeitoHabilidade; //função de desativar o efeito de cada habilidade

    public void AtivarEfeito() //função que ativa o efeito da habilidade
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

    IEnumerator TempoDeEfeito() //função que conta em segundos o tempo que a habilidade está ativa
    {
        yield return new WaitForSeconds(tempoDeEfeito);
        removerEfeitoHabilidade();
        StartCoroutine(TempoDeRecargaDoEfeito());
    }

    public void RemoverEfeito() //função que desativa o efeito da habilidade que não possui tempo de efeito
    {
        removerEfeitoHabilidade();
        StartCoroutine(TempoDeRecargaDoEfeito());
    }

    public void RemoverEfeitoExternamente() //função que remove o efeito da habilidade de forma externa
    {
        removerEfeitoHabilidade();
    }

    IEnumerator TempoDeRecargaDoEfeito() //função que conta em segundos o tempo para recaregar a habilidade
    {
        yield return new WaitForSeconds(tempoDeRecarga);
        podeAtivarEfeito = true;
    }
}
