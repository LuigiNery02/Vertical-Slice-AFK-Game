using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11ArcoVenenoso : HabilidadeBase
{
    private bool _envenenamento; //variável que verifica se há efeito de envenenamento
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
                _envenenamento = true;
                //ataques causam envenenamento
                //dura 3 segundos
                break;
            case 2:
                _envenenamento = true;
                //ataques causam envenenamento
                //dura 5 segundos
                break;
            case 3:
                _envenenamento = true;
                //ataques causam envenenamento
                //dura 7 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        _envenenamento = false;
    }
}
