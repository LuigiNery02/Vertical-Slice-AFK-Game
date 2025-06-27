using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2Guerreiro : HabilidadeBase
{
    private float _danoOriginal; //dano original do personagem
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        //_danoOriginal = personagem._danoAtaqueBasico;
    }
    private void EfeitoHabilidade() //função de efeito da habilidade 
    {
        switch (nivel)
        {
            case 1:
                //personagem._danoAtaqueBasico += (_danoOriginal / 4); //aumenta em 25% o dano do personagem
                if (personagem._personagemAlvo != null)
                {
                    personagem.movimentoEspecialAtual = 1;
                    personagem.VerificarComportamento("movimentoEspecial");
                }
                break;
            case 2:
                //personagem._danoAtaqueBasico += (_danoOriginal / 2); //aumenta em 50% o dano do personagem
                if (personagem._personagemAlvo != null)
                {
                    personagem.movimentoEspecialAtual = 1;
                    personagem.VerificarComportamento("movimentoEspecial");
                }
                break;
            case 3:
                //personagem._danoAtaqueBasico += (_danoOriginal); //aumenta em 100% o dano do personagem
                if (personagem._personagemAlvo != null)
                {
                    personagem.movimentoEspecialAtual = 1;
                    personagem.VerificarComportamento("movimentoEspecial");
                }
                break;
        }

    }

    private void RemoverEfeitoHabilidade() //função de remover efeito da habilidade 
    {
        //reseta os atributos originais do personagem
        //personagem._danoAtaqueBasico = _danoOriginal;
    }
}
