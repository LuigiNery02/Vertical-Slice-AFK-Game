using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11Machado : HabilidadeBase
{
    private bool _sangramento; //variável que verifica se há efeito de sangramento
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                _sangramento = true;
                //ataques causam sangramento (6.5 de dano por segundo)
                //dura 3 segundos
                break;
            case 2:
                _sangramento = true;
                //ataques causam sangramento (6.5 de dano por segundo)
                //dura 5 segundos
                break;
            case 3:
                _sangramento = true;
                //ataques causam sangramento (6.5 de dano por segundo)
                //dura 7 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        _sangramento = false;
    }
}
