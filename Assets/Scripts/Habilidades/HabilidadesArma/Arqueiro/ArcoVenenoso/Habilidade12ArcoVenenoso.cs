using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12ArcoVenenoso : HabilidadeBase
{
    private bool _envenenamento; //variável que verifica se há efeito de envenenamento
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

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {

    }
}
