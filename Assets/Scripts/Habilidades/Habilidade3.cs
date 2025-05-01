using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Habilidade3 : HabilidadesBase
{
    [Header("�rea de Dano")]
    [SerializeField]
    private GameObject _area; //objeto que causar� dano
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade3;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade3;
    }
    private void EfeitoHabilidade3() //fun��o de efeito da habilidade 3
    {
        //ativa a �rea e o torna imune a dano
        personagem.imuneADanos = true;
        personagem.movimentoEspecialAtual = 2;
        personagem._personagemAlvo = null;
        personagem.VerificarComportamento("movimentoEspecial");
        _area.SetActive(true);
    }

    private void RemoverEfeitoHabilidade3() //fun��o de remover efeito da habilidade 3
    {
        //desativa a �rea e volta a poder tomar dano
        personagem.executandoMovimentoEspecial = false;
        personagem.imuneADanos = false;
        personagem.FinalizarMovimentoEspecial();
        _area.SetActive(false);
    }
}
