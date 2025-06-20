using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade10Mago : HabilidadeBase
{
    private float _defesaOriginal; //defesa original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _defesaOriginal = personagem.personagem.defesa;
        personagem.efeitoPorDano = AumentarDefesa;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        personagem.efeitoPorDanoAtivado = true;
    }

    private void AumentarDefesa() //fun��o que aumenta a defesa do personagem
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

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.defesa = _defesaOriginal;
        personagem.efeitoPorDanoAtivado = false;
    }
}
