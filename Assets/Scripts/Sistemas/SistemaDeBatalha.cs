using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoDeBatalha { MANUAL, CONTINUA} //estados do sistema de batalha
public enum PrimeiroAlvo { ALVO_PROXIMO, ALVO_VISTO} //tipos de como os personagens v�o definir seus alvos na batalha

sealed class SistemaDeBatalha : MonoBehaviour
{
    //�rea referente aos estados da batalha
    [Header("Estado de Batalha")]
    public EstadoDeBatalha estado;

    //�rea referente aos ajustes da batalha
    [Header("Ajustes de Batalha")]
    public PrimeiroAlvo primeiroAlvo;

    //�rea referente os feedbacks visuais
    [Header("Feedbacks Visuais")]
    //[HideInInspector]
    public bool usarAnima��es; //vari�vel para verificar se os personagens devem usar as anima��es
    //[HideInInspector]
    public bool usarSfxs; //vari�vel para verificar se deve haver SFX
    //[HideInInspector]
    public bool usarSliders; //vari�vel para verificar se os personagens devem ter sliders para representar suas vidas

    private bool _batalhaIniciou; //vari�vel que define se a batalha foi iniciada 
    public void IniciarBatalha() //fun��o que inicia a batalha
    {
        if(!_batalhaIniciou) //checa se a batalha j� n�o foi iniciada
        {
            _batalhaIniciou = true; //define a batalha como iniciada
            EncontrarPersonagens(); //chama a fun��o de encontrar personagens
        }
    }

    public void DefinirBatalhaContinua() //define se o estado da batalha � continuo ou n�o
    {
        //checa o estado da batalha, se for manual fica continua, e sor continua fica manual
        if(estado == EstadoDeBatalha.MANUAL)
        {
            estado = EstadoDeBatalha.CONTINUA;
        }
        else
        {
            estado = EstadoDeBatalha.MANUAL;
        }

        //chama a fun��o de iniciar a batalha
        IniciarBatalha();
    }

    private void EncontrarPersonagens() //fun��o que encontra todos os personagens na cena
    {
        //procura todos os personagens (objetos que possuem o script IAPersonagemBase) na cena
        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase personagem in personagens)
        {
            personagem.IniciarBatalha(); //chama a fun��o "IniciarBatalha" de todos os personagens encontrados
        }
    }

    public void DefinirFeedbackVisual(string feedback) //fun��o para definir quais feedbacks visuais ser�o usados
    {
        if(feedback == "anima��o")
        {
            usarAnima��es = !usarAnima��es;
        }
        else if (feedback == "sfx")
        {
            usarSfxs = !usarSfxs;
        }
        else if (feedback == "slider")
        {
            usarSliders = !usarSliders;
        }
    }
}
