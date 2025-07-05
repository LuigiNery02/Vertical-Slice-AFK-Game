using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarHitPersonagem : MonoBehaviour
{
    private IAPersonagemBase _personagem; //personagem controlador do hit
    private void OnEnable()
    {
        _personagem = GetComponentInParent<IAPersonagemBase>(); //encontra o personagem
    }

    public void AtivarHit()
    {
        _personagem.AtivarHit(); //chama a função de ativar o hit do personagem
    }

    //public void FinalizarMovimentoEspecial()
    //{
    //    _personagem.FinalizarMovimentoEspecial();
    //}
}
