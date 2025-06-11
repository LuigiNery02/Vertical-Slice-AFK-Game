using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade6Mago : HabilidadeBase
{
    private float _defesaMagicaOriginal; //defesa m�gica original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        _defesaMagicaOriginal = personagem.personagem.defesaMagica;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                personagem.personagem.defesaMagica += (_defesaMagicaOriginal / 2); //aumenta em 50% a defesa m�gica do personagem
                break;
            case 2:
                personagem.personagem.defesaMagica += _defesaMagicaOriginal; //aumenta em 100% a defesa m�gica do personagem
                break;
            case 3:
                personagem.personagem.defesaMagica += (_defesaMagicaOriginal * 2); //aumenta em 200% a defesa m�gica do personagem
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        personagem.personagem.defesaMagica = _defesaMagicaOriginal;
    }
}
