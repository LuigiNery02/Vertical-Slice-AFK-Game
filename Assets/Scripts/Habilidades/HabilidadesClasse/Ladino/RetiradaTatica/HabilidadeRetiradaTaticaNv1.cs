using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Ladino/Retirada Tatica/Nv1")]
public class HabilidadeRetiradaTaticaNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private Vector3 posicaoDeDesaparecimento;
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade1(personagem, () =>
                {
                    personagem.StartCoroutine(EsperarTempoDeDesaparecimento(personagem));
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        base.RemoverEfeito(personagem);
    }

    IEnumerator EsperarTempoDeDesaparecimento(IAPersonagemBase personagem)
    {
        Vector3 posicaoOriginal = personagem.transform.position;

        personagem.transform.position = posicaoDeDesaparecimento;
        personagem.VerificarComportamento("Idle");

        IAPersonagemBase[] inimigos = GameObject.FindObjectsOfType<IAPersonagemBase>();

        foreach (var inimigo in inimigos)
        {
            if (inimigo.controlador != personagem.controlador && inimigo._personagemAlvo == personagem)
            {
                inimigo.VerificarComportamento("selecionarAlvo");
            }
        }

        yield return new WaitForSeconds(tempoDeEfeito);

        personagem.transform.position = posicaoOriginal;
        personagem.VerificarComportamento("selecionarAlvo");

        if (personagem.vfxHabilidadeAtivaClasse == null)
        {
            GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position, personagem.transform.rotation, personagem.transform);
            personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
        }
        else
        {
            personagem.GerenciarVFXHabilidade(1, true);
        }

        personagem.StartCoroutine(EsperarVFX(personagem));

        RemoverEfeito(personagem);
    }

    IEnumerator EsperarVFX(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(0.5f);
        personagem.GerenciarVFXHabilidade(1, false);
    }
}
