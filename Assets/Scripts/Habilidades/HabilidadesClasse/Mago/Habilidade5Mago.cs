using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade5Mago : HabilidadeBase
{
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
                tempoDeEfeito = 1;
                personagem._personagemAlvo.VerificarComportamento("paralisia");
                break;
            case 2:
                tempoDeEfeito = 2;
                personagem._personagemAlvo.VerificarComportamento("paralisia");
                break;
            case 3:
                tempoDeEfeito = 3;
                personagem._personagemAlvo.VerificarComportamento("paralisia");
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //desparaliza o inimigo alvo
        personagem._personagemAlvo.VerificarComportamento("selecionarAlvo");
    }
}
