using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TipoDeHabilidade{ Classe, Arma }
public class HabilidadeBase : MonoBehaviour
{
    [Header("Características da Habilidade")]
    public string nome; //nome da habilidade
    [TextArea(10, 20)]
    public string descricao; //descrição da habilidade
    public TipoDeHabilidade tipoDeHabilidade; //tipo de habilidade
    public int nivel; //nível da habilidade
    public string idHabilidade; //id da habilidade (o mesmo id para os 3 diferentes níveis da habilidade)

    [Header("Atributos da Habilidade")]
    public float pontosDeHabilidade; //valor necessário de pontos de habilidade para ativar a habilidade
    public bool temTempoDeEfeito; //variável que define se a habilidade possui tempo de efeito
    public float tempoDeEfeito; //tempo do efeito da habilidade (caso possua)
    public float tempoDeRecarga; //tempo de recarga da habilidade

    [Header("Imagem")]
    public Sprite spriteHabilidade; //sprite da imagem da habilidade

    [HideInInspector]
    public bool podeAtivarEfeito = true; //variável que determina se pode ou não ativar o efeito

    public delegate void delegateEfeito();
    public delegateEfeito efeitoHabilidade; //função do efeito de cada habilidade

    public delegate void delegateRemoverEfeito();
    public delegateEfeito removerEfeitoHabilidade; //função de desativar o efeito de cada habilidade

    [HideInInspector]
    public IAPersonagemBase personagem; //personagem que utilizará a habilidade

    public void AtivarEfeito() //função que ativa o efeito da habilidade
    {
        if (podeAtivarEfeito && personagem._comportamento != EstadoDoPersonagem.MORTO)
        {
            podeAtivarEfeito = false;
            efeitoHabilidade();
            if(temTempoDeEfeito)
            {
                StartCoroutine(TempoDeEfeito());
            }
            //selecaoDePersonagem.AtualizarSeleção();
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
        podeAtivarEfeito = true;
        //selecaoDePersonagem.AtualizarSeleção();
    }

    IEnumerator TempoDeRecargaDoEfeito() //função que conta em segundos o tempo para recaregar a habilidade
    {
        yield return new WaitForSeconds(tempoDeRecarga);
        podeAtivarEfeito = true;
        //selecaoDePersonagem.AtualizarSeleção();
    }
}
