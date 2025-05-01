using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Habilidade2 : HabilidadesBase
{
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade2;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade2;
    }
    private void EfeitoHabilidade2() //função de efeito da habilidade 2
    {
        if(personagem._personagemAlvo != null)
        {
            personagem.movimentoEspecialAtual = 1;
            personagem.VerificarComportamento("movimentoEspecial");
        }

    }

    private void RemoverEfeitoHabilidade2() //função de remover efeito da habilidade 2
    {
        personagem.executandoMovimentoEspecial = false;
    }
}
