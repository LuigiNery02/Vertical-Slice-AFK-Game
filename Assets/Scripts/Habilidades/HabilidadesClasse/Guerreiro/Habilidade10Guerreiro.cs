using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10Guerreiro : HabilidadeBase
{
    private float _defesaOriginal; //defesa original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _defesaOriginal = personagem.personagem.defesa;
        personagem.efeitoPorEsquiva = AumentarDefesa;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorEsquivaAtivado = true;
    }

    private void AumentarDefesa() //função que aumenta a defesa do personagem por esquiva
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.defesa += (_defesaOriginal / 10);
                break;
            case 2:
                personagem.personagem.defesa += (_defesaOriginal / 5);
                break;
            case 3:
                personagem.personagem.defesa += (_defesaOriginal / 10) * 3;
                break;
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.efeitoPorEsquivaAtivado = false;
        personagem.personagem.defesa = _defesaOriginal;
    }
}
