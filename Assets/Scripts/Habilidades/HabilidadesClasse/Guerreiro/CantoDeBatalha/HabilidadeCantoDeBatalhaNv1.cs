using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Canto de Batalha/Nv1")]
public class HabilidadeCantoDeBatalhaNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private int consumoDeWillPower = 1;
    [SerializeField] 
    private float bonusPorcentagemDefesas = 3;
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel) && personagem.willPower >= consumoDeWillPower)
            {
                personagem.GastarSP(custoDeMana);
                personagem.AtualizarWillPower(consumoDeWillPower, false);

                base.ChecarCastingHabilidade1(personagem, () =>
                {
                    personagem.StartCoroutine(ExecutarBuff(personagem));
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }

    private IEnumerator ExecutarBuff(IAPersonagemBase personagem)
    {
        var aliados = GameObject.FindObjectsOfType<IAPersonagemBase>();
        var defesaOriginal = new Dictionary<IAPersonagemBase, float>();
        var defesaMagicaOriginal = new Dictionary<IAPersonagemBase, float>();
        var vfxInstanciados = new Dictionary<IAPersonagemBase, GameObject>();

        foreach (var aliado in aliados)
        {
            if (aliado.controlador == personagem.controlador)
            {
                aliado.defesa += bonusPorcentagemDefesas;
                aliado.defesaMagica += bonusPorcentagemDefesas;

                if (vfx != null)
                {
                    GameObject vfxObj = GameObject.Instantiate(vfx, aliado.transform.position, aliado.transform.rotation, aliado.transform);
                    vfxInstanciados[aliado] = vfxObj;
                }
            }
        }

        if (personagem.vfxHabilidadeAtivaClasse == null)
        {
            GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position, personagem.transform.rotation, personagem.transform);
            personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
        }
        else
        {
            personagem.GerenciarVFXHabilidade(1, true);
        }

        yield return new WaitForSeconds(tempoDeEfeito);

        foreach (var aliado in aliados)
        {
            if (aliado != null && aliado.controlador == personagem.controlador)
            {
                aliado.defesa -= bonusPorcentagemDefesas;
                aliado.defesaMagica -= bonusPorcentagemDefesas;

                if (vfxInstanciados.TryGetValue(aliado, out GameObject vfxObj) && vfxObj != null)
                {
                    GameObject.Destroy(vfxObj);
                }
            }
        }

        RemoverEfeito(personagem);
    }
}
