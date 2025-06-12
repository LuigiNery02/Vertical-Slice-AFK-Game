using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade12CajadoFogo : HabilidadeBase
{
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;
        personagem.efeitoPorAtaque = CausarQueimadura;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
    }
    private void CausarQueimadura() //função que ativa o efeito de queimadura
    {
        if (!personagem._personagemAlvo.queimadura)
        {
            switch (nivel)
            {
                case 1:
                    personagem._personagemAlvo.danoQueimadura = 0.5f;
                    break;
                case 2:
                    personagem._personagemAlvo.danoQueimadura = 1;
                    break;
                case 3:
                    personagem._personagemAlvo.danoQueimadura = 1.5f;
                    break;
            }

            personagem._personagemAlvo.queimadura = true;
            personagem._personagemAlvo.Queimadura();
        }
    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = false;
        if (personagem._personagemAlvo != null && personagem._personagemAlvo._comportamento != EstadoDoPersonagem.MORTO)
        {
            personagem._personagemAlvo.queimadura = false;
        }
    }
}
