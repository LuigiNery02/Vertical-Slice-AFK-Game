using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade9Guerreiro : HabilidadeBase
{
    private List<IAPersonagemBase> listaDeInimigos = new List<IAPersonagemBase>(); //lista de inimigos na cena
    private int _numeroDeAtaques; //numero de ataques do personagem
    private int _ataquesAcertados; //número de ataques acertados pelo personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        int ataquesAtuais = _ataquesAcertados;

        switch (nivel)
        {
            case 1:
                _numeroDeAtaques = 1;
                //cria a lista com os personagens que não são alvos do personagem
                //checa se acertou: se acertou _ataquesAcertados++
                //checa se ataquesAtuais é igual que _ataquesAcertados: se verdadeira causa dano a eles
                //checa se ataquesAtuais é igual a numero de ataques: se verdadeiro chama a função verificarefeito
                break;
            case 2:
                _numeroDeAtaques = 2;
                //cria a lista com os personagens que não são alvos do personagem
                //checa se acertou: se acertou _ataquesAcertados++
                //checa se ataquesAtuais é igual que _ataquesAcertados: se verdadeira causa dano a eles
                //checa se ataquesAtuais é igual a numero de ataques: se verdadeiro chama a função verificarefeito
                break;
            case 3:
                _numeroDeAtaques = 3;
                //cria a lista com os personagens que não são alvos do personagem
                //checa se acertou: se acertou _ataquesAcertados++
                //checa se ataquesAtuais é igual que _ataquesAcertados: se verdadeira causa dano a eles
                //checa se ataquesAtuais é igual a numero de ataques: se verdadeiro chama a função verificarefeito
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        _ataquesAcertados = 0;
        listaDeInimigos = new List<IAPersonagemBase>(); //reseta a lista
    }
}
