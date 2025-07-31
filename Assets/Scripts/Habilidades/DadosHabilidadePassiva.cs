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
    public float buffCastFixoAtivaClasse;
    public float buffCastFixoAtivaArma;
    public HabilidadeElemento elemento;
    public HashSet<HabilidadeElemento> elementosRecentes;
    public int sequencia;
    public bool buffProximaHabilidade;
    public Dictionary<HabilidadeAtiva, float>habilidadesBuffadas;

    public System.Action<int> eventoWillPowerLideranca;
    public System.Action<HabilidadeAtiva> eventoConjuracaoFocada;
    public System.Action<HabilidadeAtiva> eventoCorrenteArcana;
    public System.Action<HabilidadeAtiva> evento2CorrenteArcana;
    public System.Action<HabilidadeAtiva> eventoArcoElemental;
    public System.Action<HabilidadeAtiva> eventoRemocaoBuff;
    public List<System.Action<int>> eventosExternos;

    public DadosHabilidadePassiva()
    {
        alvosComBonus = new HashSet<IAPersonagemBase>();
    }
}