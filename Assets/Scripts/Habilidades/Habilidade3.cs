using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Habilidade3 : HabilidadesBase
{
    [Header("Área de Dano")]
    [SerializeField]
    private GameObject _area; //objeto que causará dano
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade3;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade3;
    }
    private void EfeitoHabilidade3() //função de efeito da habilidade 3
    {
        //ativa a área e o torna imune a dano
        personagem.imuneADanos = true;
        personagem.movimentoEspecialAtual = 2;
        personagem._personagemAlvo = null;
        personagem.VerificarComportamento("movimentoEspecial");
        _area.SetActive(true);
    }

    private void RemoverEfeitoHabilidade3() //função de remover efeito da habilidade 3
    {
        //desativa a área e volta a poder tomar dano
        personagem.executandoMovimentoEspecial = false;
        personagem.imuneADanos = false;
        personagem.FinalizarMovimentoEspecial();
        _area.SetActive(false);
    }
}
