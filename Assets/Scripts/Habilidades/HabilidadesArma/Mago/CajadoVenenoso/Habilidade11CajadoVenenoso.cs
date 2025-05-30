using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade11CajadoVenenoso : HabilidadeBase
{
    private bool _envenenamento; //vari�vel que verifica se h� efeito de envenenamento
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
                _envenenamento = true;
                //ataques causam envenenamento
                //dura 2 segundos
                break;
            case 2:
                _envenenamento = true;
                //ataques causam envenenamento
                //dura 4 segundos
                break;
            case 3:
                _envenenamento = true;
                //ataques causam envenenamento
                //dura 6 segundos
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        _envenenamento = false;
    }
}
