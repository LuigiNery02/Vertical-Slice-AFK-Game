using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoDeBatalha { MANUAL, CONTINUA} //estados do sistema de batalha
public enum PrimeiroAlvo { ALVO_PROXIMO, ALVO_VISTO} //tipos de como os personagens vão definir seus alvos na batalha

sealed class SistemaDeBatalha : MonoBehaviour
{
    //Área referente aos estados da batalha
    [Header("Estado de Batalha")]
    public EstadoDeBatalha estado;

    //Área referente aos ajustes da batalha
    [Header("Ajustes de Batalha")]
    public PrimeiroAlvo primeiroAlvo;

    //Área referente os feedbacks visuais
    [Header("Feedbacks Visuais")]
    //[HideInInspector]
    public bool usarAnimações; //variável para verificar se os personagens devem usar as animações
    //[HideInInspector]
    public bool usarSfxs; //variável para verificar se deve haver SFX
    //[HideInInspector]
    public bool usarSliders; //variável para verificar se os personagens devem ter sliders para representar suas vidas

    private bool _batalhaIniciou; //variável que define se a batalha foi iniciada 
    public void IniciarBatalha() //função que inicia a batalha
    {
        if(!_batalhaIniciou) //checa se a batalha já não foi iniciada
        {
            _batalhaIniciou = true; //define a batalha como iniciada
            EncontrarPersonagens(); //chama a função de encontrar personagens
        }
    }

    public void DefinirBatalhaContinua() //define se o estado da batalha é continuo ou não
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

        //chama a função de iniciar a batalha
        IniciarBatalha();
    }

    private void EncontrarPersonagens() //função que encontra todos os personagens na cena
    {
        //procura todos os personagens (objetos que possuem o script IAPersonagemBase) na cena
        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase personagem in personagens)
        {
            personagem.IniciarBatalha(); //chama a função "IniciarBatalha" de todos os personagens encontrados
        }
    }

    public void DefinirFeedbackVisual(string feedback) //função para definir quais feedbacks visuais serão usados
    {
        if(feedback == "animação")
        {
            usarAnimações = !usarAnimações;
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
