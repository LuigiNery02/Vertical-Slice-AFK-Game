using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Habilidade6 : HabilidadesBase
{
    private float _velocidadeOriginal; //velocidade original do alvo
    private float _cooldownOriginal; //cooldown original do alvo

    private IAPersonagemBase _personagemPai;
    private IAPersonagemBase _personagemAlvo;
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade6;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade6;
        _personagemPai = GetComponent<IAPersonagemBase>();
    }
    private void EfeitoHabilidade6() //função de efeito da habilidade 6
    {
        if(_personagemPai._personagemAlvo != null)
        {
            _personagemAlvo = _personagemPai._personagemAlvo;
            _velocidadeOriginal = _personagemAlvo._velocidade; //guarda a velocidade original
            _cooldownOriginal = _personagemAlvo._cooldown; //guarda o cooldowm original
            //aplica o efeito no personagem alvo
            _personagemAlvo._velocidade = _velocidadeOriginal / 2;
            _personagemAlvo._cooldown = _cooldownOriginal * 2;
        }
        else
        {
            this.StopAllCoroutines();
            podeAtivarEfeito = true;
        }
    }

    private void RemoverEfeitoHabilidade6() //função de remover efeito da habilidade 6
    {
        if (_personagemAlvo != null)
        {
            //retira o efeito do personagem alvo
            _personagemAlvo._velocidade = _velocidadeOriginal;
            _personagemAlvo._cooldown = _cooldownOriginal;
            _personagemAlvo = null;
        }
    }
}
