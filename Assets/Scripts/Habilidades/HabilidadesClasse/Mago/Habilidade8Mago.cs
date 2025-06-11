using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade8Mago : HabilidadeBase
{
    private float _pontosDeHabilidadeOriginal;
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        _pontosDeHabilidadeOriginal = personagem.personagem.pontosDeHabilidade;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.pontosDeHabilidade += 10;
                break;
            case 2:
                personagem.personagem.pontosDeHabilidade += 10;
                break;
            case 3:
                personagem.personagem.pontosDeHabilidade += 10;
                break;
        }

        if (personagem.personagem.pontosDeHabilidade >= _pontosDeHabilidadeOriginal)
        {
            personagem.personagem.pontosDeHabilidade = _pontosDeHabilidadeOriginal;
        }
    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {

    }
}
