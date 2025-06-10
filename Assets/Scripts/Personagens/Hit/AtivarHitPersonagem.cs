using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarHitPersonagem : MonoBehaviour
{
    private IAPersonagemBase _personagem;
    private void OnEnable()
    {
        _personagem = GetComponentInParent<IAPersonagemBase>();
    }

    public void AtivarHit()
    {
        _personagem.AtivarHit();
    }

    public void FinalizarMovimentoEspecial()
    {
        _personagem.FinalizarMovimentoEspecial();
    }
}
