using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12ArcoVenenoso : HabilidadeBase
{
    private bool _envenenamento; //vari�vel que verifica se h� efeito de envenenamento
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
                _envenenamento = true;
                //ataques causam envenenamento
                break;
            case 2:
                _envenenamento = true;
                //ataques causam envenenamento
                break;
            case 3:
                _envenenamento = true;
                //ataques causam envenenamento
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {

    }
}
