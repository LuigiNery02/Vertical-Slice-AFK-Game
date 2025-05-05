using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //Sistema de Batalha
    public string tempo; //referente ao tempo do sistema de batalha
    public float duracaoBatalhaContinua; //referente à duração da batalha continua do sistema de batalha
    public bool acontecendoBatalhaContinua; //referente ao andamento da batalha continua do sistema de batalha
    public GameData()
    {
        //valores originais das variáveis ao iniciar um novo jogo
        tempo = DateTime.Now.ToString();
        duracaoBatalhaContinua = 0;
        acontecendoBatalhaContinua = false;
    }
}
