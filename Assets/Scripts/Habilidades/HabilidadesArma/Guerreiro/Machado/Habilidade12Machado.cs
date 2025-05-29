using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12Machado : HabilidadeBase
{
    private bool _sangramento; //vari�vel que verifica se h� efeito de sangramento
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                _sangramento = true;
                //ataques causam sangramento (1.5 de dano por segundo)
                break;
            case 2:
                _sangramento = true;
                //ataques causam sangramento (2.5 de dano por segundo)
                break;
            case 3:
                _sangramento = true;
                //ataques causam sangramento (3.5 de dano por segundo)
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {

    }
}
