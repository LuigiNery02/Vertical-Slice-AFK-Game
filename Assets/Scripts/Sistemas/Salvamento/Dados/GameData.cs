using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Personagens Criados
    public List<PersonagemData> personagens; //personagens criados 
    public List<string> codigoPersonagensBatalhaContinua; //c�digos dos personagens utilizados na �ltima batalha continua

    //Gerenciador de Invent�rio
    public List<string> gerenciadorInventarioCabecaAcessorioID; //ID dos equipamentos da cabe�a acess�rio do invent�rio
    public List<string> gerenciadorInventarioCabecaTopoID; //equipamentos da cabe�a topo do invent�rio
    public List<string> gerenciadorInventarioCabecaMedioID; //equipamentos da cabe�a m�dio do invent�rio
    public List<string> gerenciadorInventarioCabecaBaixoID; //equipamentos da cabe�a baixo do invent�rio
    public List<string> gerenciadorInventarioArmaduraID; //equipamentos da armadura do invent�rio
    public List<string> gerenciadorInventarioBracadeiraID; //equipamentos da bra�adeira do invent�rio
    public List<string> gerenciadorInventarioMaoEsquerdaID; //equipamentos da m�o esquerda do invent�rio
    public List<string> gerenciadorInventarioMaoDireitaID; //equipamentos da m�o direita do invent�rio
    public List<string> gerenciadorInventarioBotaID; //equipamentos da bota do invent�rio
    public List<string> gerenciadorInventarioAcessorio1ID; //equipamentos do acess�rio 1 do invent�rio
    public List<string> gerenciadorInventarioAcessorio2ID; //equipamentos do acess�rio 2 do invent�rio
    public List<string> gerenciadorInventarioBuffConsumivelID; //equipamentos de buff de consum�vel do invent�rio
    public bool gerenciadorInventarioEquipado; //referebte � vari�vel equipou do gerenciador de invent�rio

    //Sistema de Batalha
    public string tempo; //referente ao tempo do sistema de batalha
    public float tempoAtualBatalhaContinua; //referente ao tempo atual da batalha continua no sistema de batalha
    public float duracaoBatalhaContinua; //referente � dura��o da batalha continua do sistema de batalha
    public bool acontecendoBatalhaContinua; //referente ao andamento da batalha continua do sistema de batalha
    public int batalhasRestantes; //referente as batalhas restantes do sistema de batalha
    public int dropsRestantes; //referente aos drops restantes do sistema de batalha

    //Sistema de Drop
    public int drops; //referente aos drops do sistema de drops
    public GameData()
    {
        //valores originais das vari�veis ao iniciar um novo jogo
        personagens = new List<PersonagemData>();
        codigoPersonagensBatalhaContinua = new List<string>();
        gerenciadorInventarioCabecaAcessorioID = new List<string>();
        gerenciadorInventarioCabecaTopoID = new List<string>();
        gerenciadorInventarioCabecaMedioID = new List<string>();
        gerenciadorInventarioCabecaBaixoID = new List<string>();
        gerenciadorInventarioArmaduraID = new List<string>();
        gerenciadorInventarioBracadeiraID = new List<string>();
        gerenciadorInventarioMaoEsquerdaID = new List<string>();
        gerenciadorInventarioMaoDireitaID = new List<string>();
        gerenciadorInventarioBotaID = new List<string>();
        gerenciadorInventarioAcessorio1ID = new List<string>();
        gerenciadorInventarioAcessorio2ID = new List<string>();
        gerenciadorInventarioBuffConsumivelID = new List<string>();
        tempo = DateTime.Now.ToString();
        tempoAtualBatalhaContinua = 0;
        duracaoBatalhaContinua = 0;
        acontecendoBatalhaContinua = false;
        batalhasRestantes = 0;
        dropsRestantes = 0;
        drops = 0;
    }
}
