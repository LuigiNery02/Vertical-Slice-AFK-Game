using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11ArcoGelo : HabilidadeBase
{
    private bool _congelamento; //variável que verifica se há efeito de congelamento
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                _congelamento = true;
                //ataques causam congelamento
                //dura 3 segundos
                break;
            case 2:
                _congelamento = true;
                //ataques causam congelamento
                //dura 5 segundos
                break;
            case 3:
                _congelamento = true;
                //ataques causam congelamento
                //dura 7 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        _congelamento = false;
    }
}
