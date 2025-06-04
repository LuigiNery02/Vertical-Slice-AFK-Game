using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Personagens Criados
    public List<PersonagemData> personagens; //personagens criados 

    //Gerenciador de Inventário
    public List<EquipamentoBase> gerenciadorInventarioCabecaAcessorio; //equipamentos da cabeça acessório do inventário
    public List<EquipamentoBase> gerenciadorInventarioCabecaTopo; //equipamentos da cabeça topo do inventário
    public List<EquipamentoBase> gerenciadorInventarioCabecaMedio; //equipamentos da cabeça médio do inventário
    public List<EquipamentoBase> gerenciadorInventarioCabecaBaixo; //equipamentos da cabeça baixo do inventário
    public List<EquipamentoBase> gerenciadorInventarioArmadura; //equipamentos da armadura do inventário
    public List<EquipamentoBase> gerenciadorInventarioBracadeira; //equipamentos da braçadeira do inventário
    public List<EquipamentoBase> gerenciadorInventarioMaoEsquerda; //equipamentos da mão esquerda do inventário
    public List<EquipamentoBase> gerenciadorInventarioMaoDireita; //equipamentos da mão direita do inventário
    public List<EquipamentoBase> gerenciadorInventarioBota; //equipamentos da bota do inventário
    public List<EquipamentoBase> gerenciadorInventarioAcessorio1; //equipamentos do acessório 1 do inventário
    public List<EquipamentoBase> gerenciadorInventarioAcessorio2; //equipamentos do acessório 2 do inventário
    public List<EquipamentoBase> gerenciadorInventarioBuffConsumivel; //equipamentos de buff de consumível do inventário
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
