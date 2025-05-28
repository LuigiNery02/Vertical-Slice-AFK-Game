using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade6Arqueiro : HabilidadeBase
{
    private float _defesaOriginal; //defesa original do personagem
    private float _defesaMagicaOriginal; //defesa mágica original do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //_defesaOriginal = personagem.defesa;
        //_defesaMagicaOriginal = personagem.defesaMagica;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem.defesa += (_defesaOriginal / 2); //aumenta em 50% a defesa do personagem
                //personagem.defesaMagica += (_defesaMagicaOriginal / 2); //aumenta em 50% a defesa mágica do personagem
                break;
            case 2:
                //personagem.defesa += _defesaOriginal; //aumenta em 100% a defesa do personagem
                //personagem.defesaMagica += _defesaMagicaOrigina; //aumenta em 100% a defesa mágica do personagem
                break;
            case 3:
                //personagem.defesa += (_defesaOriginal * 2); //aumenta em 200% a defesa do personagem
                //personagem.defesaMagica += (_defesaMagicaOriginal * 2); //aumenta em 200% a defesa mágica do personagem
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem.defesa = _defesaOriginal;
        //personagem.defesaMagica = _defesaMagicaOriginal;
    }
}
