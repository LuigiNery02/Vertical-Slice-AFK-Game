using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Canto de Batalha/Nv2")]

public class HabilidadeCantoDeBatalhaNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private int consumoDeWillPower = 1;
    [SerializeField]
    private float bonusPorcentagemDefesas = 5;
    public GameObject vfx;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem) && personagem.willPower >= consumoDeWillPower)
            {
                personagem.podeAtivarEfeitoHabilidadeAtivaClasse = false;

                personagem.AtualizarWillPower(consumoDeWillPower, false);
                personagem.GastarSP(custoDeMana);

                personagem.StartCoroutine(ExecutarBuff(personagem));
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(1, false);
        personagem.podeAtivarEfeitoHabilidadeAtivaClasse = true;
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
                defesaOriginal[aliado] = aliado.defesa;
                defesaMagicaOriginal[aliado] = aliado.defesaMagica;

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

        RemoverEfeito(personagem);
    }
}
