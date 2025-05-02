using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Habilidade5 : HabilidadesBase
{
    [Header("Atributos")]
    [SerializeField]
    private float _cura; //valor da cura do efeito

    private IAPersonagemBase _personagemPai;
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade5;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade5;
        _personagemPai = GetComponent<IAPersonagemBase>();
    }
    private void EfeitoHabilidade5() //função de efeito da habilidade 5
    {
        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase personagem in personagens)
        {
            if(personagem.controlador == _personagemPai.controlador) //verifica se é personagem aliado
            {
                personagem.ReceberHP(_cura); //aplica cura em todos os aliados e em si mesmo
            }
        }
    }

    private void RemoverEfeitoHabilidade5() //função de remover efeito da habilidade 5
    {

    }
}
