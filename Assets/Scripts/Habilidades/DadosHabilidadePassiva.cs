using System.Collections.Generic;
using UnityEngine;

public class DadosHabilidadePassiva
{
    public bool bonusAplicado;
    public Coroutine monitoramento;
    public float valorOriginalAtaque;
    public float valorOriginalDefesa;
    public float valorOriginalDefesaMagica;
    public float valorMultiplicadoAtaque;
    public List<Coroutine> buffsAtaqueAtivos;
    public List<float> buffValores;
    public List<float> bonusAplicados;
    public float valorOriginalHP;

    public System.Action<int> eventoWillPowerLideranca;
    public List<System.Action<int>> eventosExternos;

}