using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

sealed class SistemaDeAtalhos : MonoBehaviour
{
    //�rea referente aos inputs
    [Header("Input")]
    [SerializeField]
    private InputAction _acao1; //a��o 1 do input
    [SerializeField]
    private InputAction _acao2; //a��o 2 do input
    [SerializeField]
    private InputAction _acao3; //a��o 3 do input
    [SerializeField]
    private InputAction _acao4; //a��o 4 do input
    [SerializeField]
    private InputAction _acao5; //a��o 5 do input
    [SerializeField]
    private InputAction _acao6; //a��o 6 do input

    //�rea referente aos eventos
    [Header("Event")]
    [SerializeField]
    private UnityEvent[] _evento; //evento do input

    private void OnEnable()
    {
        _acao1.Enable();
        _acao1.performed += ctx => ChamarEvento(0); 

        _acao2.Enable();
        _acao2.performed += ctx => ChamarEvento(1);

        _acao3.Enable();
        _acao3.performed += ctx => ChamarEvento(2);

        _acao4.Enable();
        _acao4.performed += ctx => ChamarEvento(3);

        _acao5.Enable();
        _acao5.performed += ctx => ChamarEvento(4);

        _acao6.Enable();
        _acao6.performed += ctx => ChamarEvento(5);

    }

    private void OnDisable()
    {
        _acao1.Disable();
        _acao2.Disable();
        _acao3.Disable();
        _acao4.Disable();
        _acao5.Disable();
        _acao6.Disable();
    }

    private void ChamarEvento(int idEvento) //fun��o de chamar o evento de input
    {
        _evento[idEvento].Invoke();
    }
}
