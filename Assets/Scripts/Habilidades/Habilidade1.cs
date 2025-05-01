using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Habilidade1 : HabilidadesBase
{
    [Header("Atributos")]
    [SerializeField]
    private float _dano; //novo dano do personagem
    [SerializeField]
    private float _cooldown; //novo cooldown do personagem
    [SerializeField]
    private float _velocidade; //nova velocidade do personagem

    private float _danoOriginal; //dano original do personagem
    private float _cooldownOriginal; //cooldown original do personagem
    private float _velocidadeOriginal; //velocidade original do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade1;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade1;

        //guarda os atributos originais do personagem
        _danoOriginal = personagem._danoAtaqueBasico;
        _cooldownOriginal = personagem._cooldown;
        _velocidadeOriginal = personagem._velocidade;
    }
    private void EfeitoHabilidade1() //função de efeito da habilidade 1
    {
        //define os novos atributos do personagem
        personagem._danoAtaqueBasico = _dano;
        personagem._cooldown = _cooldown;
        personagem._velocidade = _velocidade;
    } 

    private void RemoverEfeitoHabilidade1() //função de remover efeito da habilidade 1
    {
        //reseta os atributos originais do personagem
        personagem._danoAtaqueBasico = _danoOriginal;
        personagem._cooldown = _cooldownOriginal;
        personagem._velocidade = _velocidadeOriginal;
    }
}
