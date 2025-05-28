using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade8Mago : HabilidadeBase
{
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
                //recupera 10 pontos de habilidade
                break;
            case 2:
                //recupera 20 pontos de habilidade
                break;
            case 3:
                //recupera 30 pontos de habilidade
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
