using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TipoDeHabilidade{ Classe, Arma }
public class HabilidadeBase : MonoBehaviour
{
    [Header("Caracter�sticas da Habilidade")]
    public string nome; //nome da habilidade
    [TextArea(10, 20)]
    public string descricao; //descri��o da habilidade
    public TipoDeHabilidade tipoDeHabilidade; //tipo de habilidade
    public int nivel; //n�vel da habilidade
    public string idHabilidade; //id da habilidade (o mesmo id para os 3 diferentes n�veis da habilidade)

    [Header("Atributos da Habilidade")]
    public float pontosDeHabilidade; //valor necess�rio de pontos de habilidade para ativar a habilidade
    public bool temTempoDeEfeito; //vari�vel que define se a habilidade possui tempo de efeito
    public float tempoDeEfeito; //tempo do efeito da habilidade (caso possua)
    public float tempoDeRecarga; //tempo de recarga da habilidade

    [Header("Imagem")]
    public Sprite spriteHabilidade; //sprite da imagem da habilidade

    [HideInInspector]
    public bool podeAtivarEfeito = true; //vari�vel que determina se pode ou n�o ativar o efeito

    public delegate void delegateEfeito();
    public delegateEfeito efeitoHabilidade; //fun��o do efeito de cada habilidade

    public delegate void delegateRemoverEfeito();
    public delegateEfeito removerEfeitoHabilidade; //fun��o de desativar o efeito de cada habilidade

    [HideInInspector]
    public IAPersonagemBase personagem; //personagem que utilizar� a habilidade

    public virtual void Inicializar() //fun��o que inicializa a habilidade quando o personagem � definido na batalha
    {
        if(personagem.pontosDeHabilidadeTexto != null)
        {
            personagem.pontosDeHabilidadeTexto.text = (personagem.spAtual + " / " + personagem._spMaximoEInicial);
        }
    }

    public void AtivarEfeito() //fun��o que ativa o efeito da habilidade
    {
        bool podeUsarHabilidade = false;
        if (podeAtivarEfeito && personagem._comportamento != EstadoDoPersonagem.MORTO)
        {
            if(personagem.spAtual >= pontosDeHabilidade)
            {
                switch (nivel)
                {
                    case 1:
                        if (personagem.personagem.runaNivel1)
                        {
                            podeUsarHabilidade = true;
                        }
                        else
                        {
                            if (GerenciadorDeInventario.instancia != null)
                            {
                                GerenciadorDeInventario.instancia.MostrarMensagem("Runa N�vel 1 n�o equipada");
                            }
                        }
                        break;
                    case 2:
                        if (personagem.personagem.runaNivel2)
                        {
                            podeUsarHabilidade = true;
                        }
                        else
                        {
                            if (GerenciadorDeInventario.instancia != null)
                            {
                                GerenciadorDeInventario.instancia.MostrarMensagem("Runa N�vel 2 n�o equipada");
                            }
                        }
                        break;
                    case 3:
                        if (personagem.personagem.runaNivel3)
                        {
                            podeUsarHabilidade = true;
                        }
                        else
                        {
                            if (GerenciadorDeInventario.instancia != null)
                            {
                                GerenciadorDeInventario.instancia.MostrarMensagem("Runa N�vel 3 n�o equipada");
                            }
                        }
                        break;
                }

                if (podeUsarHabilidade)
                {
                    Debug.Log("Efeito Ativado");
                    personagem.spAtual -= pontosDeHabilidade;
                    if(SistemaDeBatalha.usarSfxs)
                    {
                        personagem._audio.clip = personagem._habilidadeSFX;
                        personagem._audio.Play();
                    }
                    if (personagem.pontosDeHabilidadeTexto != null)
                    {
                        personagem.pontosDeHabilidadeTexto.text = (personagem.spAtual + " / " + personagem._spMaximoEInicial);
                    }
                    podeAtivarEfeito = false;
                    efeitoHabilidade();
                    if (temTempoDeEfeito)
                    {
                        personagem.EsperarEfeitoHabilidade(this, tempoDeEfeito);
                    }
                    //selecaoDePersonagem.AtualizarSele��o();
                }
            }
            else
            {
                if(GerenciadorDeInventario.instancia != null)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Pontos de Habilidade Insuficientes");
                }
            }
        }
    }

    //IEnumerator TempoDeEfeito() //fun��o que conta em segundos o tempo que a habilidade est� ativa
    //{
    //    yield return new WaitForSeconds(tempoDeEfeito);
    //    removerEfeitoHabilidade();
    //    StartCoroutine(TempoDeRecargaDoEfeito());
    //}

    public void RemoverEfeito() //fun��o que desativa o efeito da habilidade que n�o possui tempo de efeito
    {
        Debug.Log("Fim de Efeito");
        removerEfeitoHabilidade();
        if(tempoDeRecarga != 0)
        {
            personagem.EsperarRecargaHabilidade(this, tempoDeRecarga);
        }
        if (personagem.pontosDeHabilidadeTexto != null)
        {
            personagem.pontosDeHabilidadeTexto.text = (personagem.spAtual + " / " + personagem._spMaximoEInicial);
        }
    }

    public void RemoverEfeitoExternamente() //fun��o que remove o efeito da habilidade de forma externa
    {
        removerEfeitoHabilidade();
        podeAtivarEfeito = true;
        if (personagem.pontosDeHabilidadeTexto != null)
        {
            personagem.pontosDeHabilidadeTexto.text = (personagem.spAtual + " / " + personagem._spMaximoEInicial);
        }
        //selecaoDePersonagem.AtualizarSele��o();
    }

    //IEnumerator TempoDeRecargaDoEfeito() //fun��o que conta em segundos o tempo para recaregar a habilidade
    //{
    //    yield return new WaitForSeconds(tempoDeRecarga);
    //    podeAtivarEfeito = true;
    //    //selecaoDePersonagem.AtualizarSele��o();
    //}
}
