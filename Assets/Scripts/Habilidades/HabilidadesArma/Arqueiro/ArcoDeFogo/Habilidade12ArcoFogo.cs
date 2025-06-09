using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12ArcoFogo : HabilidadeBase
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
                //ataques causam queimadura (0.5 de dano por segundo)
                break;
            case 2:
                _queimadura = true;
                //ataques causam queimadura (1 de dano por segundo)
                break;
            case 3:
                _queimadura = true;
                //ataques causam queimadura (1.5 de dano por segundo)
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {

    }
}
