using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5Mago : HabilidadeBase
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
                tempoDeEfeito = 1;
                //paralisa o inimigo alvo por 1 segundo
                break;
            case 2:
                tempoDeEfeito = 2;
                //paralisa o inimigo alvo por 2 segundos
                break;
            case 3:
                tempoDeEfeito = 3;
                //paralisa o inimigo alvo por 3 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //desparaliza o inimigo alvo
    }
}
