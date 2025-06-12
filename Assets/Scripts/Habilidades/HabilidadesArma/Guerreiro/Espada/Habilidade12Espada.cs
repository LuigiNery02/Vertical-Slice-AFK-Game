using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12Espada : HabilidadeBase
{
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        personagem.efeitoPorAtaque = CausarSangramento;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
    }

    private void CausarSangramento() //fun��o que ativa o efeito de sangramento
    {
        switch (nivel)
        {
            case 1:
                personagem._personagemAlvo.danoSangramento = 1;
                break;
            case 2:
                personagem._personagemAlvo.danoSangramento = 2;
                break;
            case 3:
                personagem._personagemAlvo.danoSangramento = 3;
                break;
        }

        if (!personagem._personagemAlvo.sangramento)
        {
            personagem._personagemAlvo.sangramento = true;
            personagem._personagemAlvo.Sangramento();
        }  
    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = false;
        if (personagem._personagemAlvo != null && personagem._personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem._personagemAlvo.sangramento = false;
        }
    }
}
