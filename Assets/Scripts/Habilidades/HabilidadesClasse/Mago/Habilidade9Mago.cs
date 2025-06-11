using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9Mago : HabilidadeBase
{
    private float _ataqueMagicoOriginal; //ataque m�gico original do personagem
    private float _defesaMagicaOriginal; //defesa m�gico original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _ataqueMagicoOriginal = personagem.danoAtaqueMagico;
        _defesaMagicaOriginal = personagem.personagem.defesaMagica;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.danoAtaqueMagico += (_ataqueMagicoOriginal / 4); //aumenta o ataque m�gico em 25%
                personagem.personagem.defesaMagica += (_defesaMagicaOriginal / 4); //aumenta a defesa m�gica em 25%
                break;
            case 2:
                personagem.danoAtaqueMagico += (_ataqueMagicoOriginal / 2); //aumenta o ataque m�gico em 50%
                personagem.personagem.defesaMagica += (_defesaMagicaOriginal / 2); //aumenta a defesa m�gica em 50%
                break;
            case 3:
                personagem.danoAtaqueMagico += _ataqueMagicoOriginal; //aumenta o ataque m�gico em 100%
                personagem.personagem.defesaMagica += _defesaMagicaOriginal; //aumenta a defesa m�gica em 100%
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.danoAtaqueMagico = _ataqueMagicoOriginal;
        personagem.personagem.defesaMagica = _defesaMagicaOriginal;
    }
}
