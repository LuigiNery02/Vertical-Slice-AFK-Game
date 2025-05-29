using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12Lanca : HabilidadeBase
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
                //ataques causam sangramento (2 de dano por segundo)
                break;
            case 2:
                _sangramento = true;
                //ataques causam sangramento (3 de dano por segundo)
                break;
            case 3:
                _sangramento = true;
                //ataques causam sangramento (4 de dano por segundo)
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {

    }
}
