using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidade2Mago : HabilidadeBase
{
    private float _area; //valor da �rea que causar� dano em �rea
    public override void Inicializar()
    {
        efeitoHabilidade = EfeitoHabilidade;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade;

        //guarda os atributos originais do personagem
        personagem.efeitoPorAtaque = DanoEmArea;
    }
    private void EfeitoHabilidade() //fun��o de efeito da habilidade 
    {
        personagem.efeitoPorAtaqueAtivado = true;
    }

    private void DanoEmArea() //fun��o de dano em �rea
    {
        if (personagem.efeitoPorAtaqueAtivado)
        {
            switch (nivel)
            {
                case 1: 
                    _area = 5f; 
                    break;
                case 2: 
                    _area = 10f; 
                    break;
                case 3: 
                    _area = 15f; 
                    break;
            }

            Collider[] alvosNaArea = Physics.OverlapSphere(personagem._personagemAlvo.transform.position, _area);

            foreach (Collider colisor in alvosNaArea)
            {
                IAPersonagemBase inimigo = colisor.GetComponent<IAPersonagemBase>();

                if (inimigo != null && inimigo.controlador != personagem.controlador && inimigo._comportamento != EstadoDoPersonagem.MORTO)
                {
                    //inimigo.SofrerDano(personagem.danoAtaqueMagico);
                }
            }
        }
    }

    private void RemoverEfeitoHabilidade() //fun��o de remover efeito da habilidade 
    {
        //reseta o ataque como n�o para dano em �rea
        personagem.efeitoPorAtaqueAtivado = false;
    }
}
