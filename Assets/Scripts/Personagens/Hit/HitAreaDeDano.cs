using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAreaDeDano : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private int _dano; //dano da área
    [SerializeField]
    private float _cooldown; //cooldown do ataque da área

    private IAPersonagemBase _personagem;
    private List<IAPersonagemBase> personagensAtingidos = new List<IAPersonagemBase>(); //lista de personagens atingidos pela área

    private void OnEnable()
    {
        _personagem = GetComponentInParent<IAPersonagemBase>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IAPersonagemBase>() != null)
        {
            IAPersonagemBase alvo = other.GetComponent<IAPersonagemBase>();

            if (alvo.controlador != _personagem.controlador && alvo._comportamento != EstadoDoPersonagem.MORTO) //verifica se é um personagem inimigo e não está morto
            {
                if (!personagensAtingidos.Contains(alvo))
                {
                    alvo.SofrerDano(_dano);
                    personagensAtingidos.Add(alvo); //marca que já causou dano nesse alvo

                    if (this.gameObject.activeSelf)
                    {
                        StartCoroutine(CoolDown());
                    }
                }
            }
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_cooldown);
        personagensAtingidos.Clear();
    }
}
