using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Personagens Criados
    public List<PersonagemData> personagens; //personagens criados 
    public List<string> codigoPersonagensBatalhaContinua; //códigos dos personagens utilizados na última batalha continua

    //Gerenciador de Inventário
    public List<string> gerenciadorInventarioCabecaAcessorioID; //ID dos equipamentos da cabeça acessório do inventário
    public List<string> gerenciadorInventarioCabecaTopoID; //equipamentos da cabeça topo do inventário
    public List<string> gerenciadorInventarioCabecaMedioID; //equipamentos da cabeça médio do inventário
    public List<string> gerenciadorInventarioCabecaBaixoID; //equipamentos da cabeça baixo do inventário
    public List<string> gerenciadorInventarioArmaduraID; //equipamentos da armadura do inventário
    public List<string> gerenciadorInventarioBracadeiraID; //equipamentos da braçadeira do inventário
    public List<string> gerenciadorInventarioMaoEsquerdaID; //equipamentos da mão esquerda do inventário
    public List<string> gerenciadorInventarioMaoDireitaID; //equipamentos da mão direita do inventário
    public List<string> gerenciadorInventarioBotaID; //equipamentos da bota do inventário
    public List<string> gerenciadorInventarioAcessorio1ID; //equipamentos do acessório 1 do inventário
    public List<string> gerenciadorInventarioAcessorio2ID; //equipamentos do acessório 2 do inventário
    public List<string> gerenciadorInventarioBuffConsumivelID; //equipamentos de buff de consumível do inventário
    public bool gerenciadorInventarioEquipado; //referebte à variável equipou do gerenciador de inventário

    //Sistema de Batalha
    public string tempo; //referente ao tempo do sistema de batalha
    public float tempoAtualBatalhaContinua; //referente ao tempo atual da batalha continua no sistema de batalha
    public float duracaoBatalhaContinua; //referente à duração da batalha continua do sistema de batalha
    public bool acontecendoBatalhaContinua; //referente ao andamento da batalha continua do sistema de batalha
    public int batalhasRestantes; //referente as batalhas restantes do sistema de batalha
    public int dropsRestantes; //referente aos drops restantes do sistema de batalha

    //Sistema de Drop
    public int drops; //referente aos drops do sistema de drops
    public GameData()
    {
        //valores originais das variáveis ao iniciar um novo jogo
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
