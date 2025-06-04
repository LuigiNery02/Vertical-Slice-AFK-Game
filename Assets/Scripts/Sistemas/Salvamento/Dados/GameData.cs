using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Personagens Criados
    public List<PersonagemData> personagens; //personagens criados 

    //Gerenciador de Invent�rio
    public List<EquipamentoBase> gerenciadorInventarioCabecaAcessorio; //equipamentos da cabe�a acess�rio do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioCabecaTopo; //equipamentos da cabe�a topo do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioCabecaMedio; //equipamentos da cabe�a m�dio do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioCabecaBaixo; //equipamentos da cabe�a baixo do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioArmadura; //equipamentos da armadura do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioBracadeira; //equipamentos da bra�adeira do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioMaoEsquerda; //equipamentos da m�o esquerda do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioMaoDireita; //equipamentos da m�o direita do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioBota; //equipamentos da bota do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioAcessorio1; //equipamentos do acess�rio 1 do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioAcessorio2; //equipamentos do acess�rio 2 do invent�rio
    public List<EquipamentoBase> gerenciadorInventarioBuffConsumivel; //equipamentos de buff de consum�vel do invent�rio
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
        gerenciadorInventarioCabecaAcessorio = new List<EquipamentoBase>();
        gerenciadorInventarioCabecaTopo = new List<EquipamentoBase>();
        gerenciadorInventarioCabecaMedio = new List<EquipamentoBase>();
        gerenciadorInventarioCabecaBaixo = new List<EquipamentoBase>();
        gerenciadorInventarioArmadura = new List<EquipamentoBase>();
        gerenciadorInventarioBracadeira = new List<EquipamentoBase>();
        gerenciadorInventarioMaoEsquerda = new List<EquipamentoBase>();
        gerenciadorInventarioMaoDireita = new List<EquipamentoBase>();
        gerenciadorInventarioBota = new List<EquipamentoBase>();
        gerenciadorInventarioAcessorio1 = new List<EquipamentoBase>();
        gerenciadorInventarioAcessorio2 = new List<EquipamentoBase>();
        gerenciadorInventarioBuffConsumivel = new List<EquipamentoBase>();
        tempo = DateTime.Now.ToString();
        tempoAtualBatalhaContinua = 0;
        duracaoBatalhaContinua = 0;
        acontecendoBatalhaContinua = false;
        batalhasRestantes = 0;
        dropsRestantes = 0;
        drops = 0;
    }
}
