using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
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
        tempo = DateTime.Now.ToString();
        tempoAtualBatalhaContinua = 0;
        duracaoBatalhaContinua = 0;
        acontecendoBatalhaContinua = false;
        batalhasRestantes = 0;
        dropsRestantes = 0;
        drops = 0;
    }
}
