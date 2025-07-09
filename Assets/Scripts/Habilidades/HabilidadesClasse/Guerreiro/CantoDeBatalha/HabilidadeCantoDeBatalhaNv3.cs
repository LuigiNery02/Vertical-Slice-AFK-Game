using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Canto de Batalha/Nv3")]

public class HabilidadeCantoDeBatalhaNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private int consumoDeWillPower = 10;
    [SerializeField]
    private float bonusPorcentagemDefesas = 50;
    [SerializeField]
    private float tempoImuneAMagias = 3;
    public GameObject vfx;

    private Dictionary<IAPersonagemBase, float> defesaOriginal = new();
    private Dictionary<IAPersonagemBase, float> defesaMagicaOriginal = new();
    private Dictionary<IAPersonagemBase, GameObject> vfxInstanciados = new();
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidade1)
        {
            if (base.ChecarAtivacao(personagem) && personagem.willPower >= consumoDeWillPower)
            {
                personagem.podeAtivarEfeitoHabilidade1 = false;

                personagem.AtualizarWillPower(consumoDeWillPower, false);
                personagem.GastarSP(custoDeMana);

                IAPersonagemBase[] aliados = GameObject.FindObjectsOfType<IAPersonagemBase>();
                List<IAPersonagemBase> aliadosAfetados = new();

                foreach (var aliado in aliados)
                {
                    if (aliado.controlador == personagem.controlador)
                    {
                        defesaOriginal[aliado] = aliado.defesa;
                        defesaMagicaOriginal[aliado] = aliado.defesaMagica;

                        aliado.defesa += bonusPorcentagemDefesas;
                        aliado.defesaMagica += bonusPorcentagemDefesas;

                        aliadosAfetados.Add(aliado);

                        if (vfx != null)
                        {
                            GameObject vfxObj = GameObject.Instantiate(vfx, aliado.transform.position + Vector3.zero, aliado.transform.rotation, aliado.transform);
                            vfxInstanciados[aliado] = vfxObj;
                        }
                    }
                }

                if (personagem.vfxHabilidade1 == null)
                {
                    GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                    personagem.vfxHabilidade1 = vfxInstanciado;
                }
                else
                {
                    personagem.GerenciarVFXHabilidade(1, true);
                }

                personagem.StartCoroutine(TempoEfeitoImuneAMagias(personagem, aliadosAfetados));
                base.AtivarEfeito(personagem);
            }
        }
    }

    IEnumerator TempoEfeitoImuneAMagias(IAPersonagemBase personagem, List<IAPersonagemBase> aliadosAfetados)
    {
        personagem.imuneAMagias = true;

        foreach (var aliado in aliadosAfetados)
        {
            if (aliado != null)
            {
                aliado.imuneAMagias = true;
            }
        }

        yield return new WaitForSeconds(tempoImuneAMagias);

        personagem.imuneAMagias = false;

        foreach (var aliado in aliadosAfetados)
        {
            if (aliado != null)
            {
                aliado.imuneAMagias = false;
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        IAPersonagemBase[] aliados = GameObject.FindObjectsOfType<IAPersonagemBase>();

        foreach (var aliado in aliados)
        {
            if (aliado != null && aliado.controlador == personagem.controlador)
            {
                if (defesaOriginal.TryGetValue(aliado, out float def))
                {
                    aliado.defesa = def;
                }
                if (defesaMagicaOriginal.TryGetValue(aliado, out float defM))
                {
                    aliado.defesaMagica = defM;
                }

                if (vfxInstanciados.TryGetValue(aliado, out GameObject vfxObj) && vfxObj != null)
                {
                    GameObject.Destroy(vfxObj);
                }
            }
        }

        defesaOriginal.Clear();
        defesaMagicaOriginal.Clear();
        vfxInstanciados.Clear();

        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }
}
