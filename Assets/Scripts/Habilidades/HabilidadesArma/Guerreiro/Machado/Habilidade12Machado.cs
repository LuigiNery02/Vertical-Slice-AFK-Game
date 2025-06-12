using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12Machado : HabilidadeBase
{
    private bool _sangramento; //variável que verifica se há efeito de sangramento
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        personagem.efeitoPorAtaque = CausarSangramento;
    }

    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
    }
    private void CausarSangramento() //função que ativa o efeito de sangramento
    {
        switch (nivel)
        {
            case 1:
                personagem._personagemAlvo.danoSangramento = 1.5f;
                break;
            case 2:
                personagem._personagemAlvo.danoSangramento = 2.5f;
                break;
            case 3:
                personagem._personagemAlvo.danoSangramento = 3.5f;
                break;
        }

        if (!personagem._personagemAlvo.sangramento)
        {
            personagem._personagemAlvo.sangramento = true;
            personagem._personagemAlvo.Sangramento();
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = false;
        if (personagem._personagemAlvo != null && personagem._personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem._personagemAlvo.sangramento = false;
        }
    }
}
