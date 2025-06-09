using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11ArcoFogo : HabilidadeBase
{
    private bool _queimadura; //vari�vel que verifica se h� efeito de queimadura
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                _queimadura = true;
                //ataques causam queimadura
                //dura 3 segundos
                break;
            case 2:
                _queimadura = true;
                //ataques causam queimadura
                //dura 5 segundos
                break;
            case 3:
                _queimadura = true;
                //ataques causam queimadura
                //dura 7 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        _queimadura = false;
    }
}
