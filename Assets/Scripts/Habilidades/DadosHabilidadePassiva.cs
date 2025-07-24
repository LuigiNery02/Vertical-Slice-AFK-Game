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
    public float valorMultiplicadoRange;
    public float valorOriginalRange;
    public List<Coroutine> buffsAtaqueAtivos;
    public List<float> buffValores;
    public List<float> bonusAplicados;
    public float valorOriginalHP;
    public float tempoDeStunOriginal;
    public bool stunBonusAplicado = false;
    public HashSet<IAPersonagemBase> alvosComBonus;
    public int contadorAtaquesBasicos = 0;

    public System.Action<int> eventoWillPowerLideranca;
    public List<System.Action<int>> eventosExternos;

    public DadosHabilidadePassiva()
    {
        alvosComBonus = new HashSet<IAPersonagemBase>();
    }
}