using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Espada/Lâmina Extra/Nv1")]

public class HabilidadeLaminaExtraNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 0.05f;
    public GameObject vfx;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaArma)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade2(personagem, () =>
                {
                    float dano = personagem._dano;
                    dano *= multiplicadorDeDano;
                    personagem._dano += dano;

                    personagem.StartCoroutine(EsperarTempoDeEfeito(personagem, dano));

                    if (personagem.vfxHabilidadeAtivaArma == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(2, true);
                    }
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.efeitoPorAtaqueAtivado = false;
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(2, false);
    }

    IEnumerator EsperarTempoDeEfeito(IAPersonagemBase personagem, float dano)
    {
        yield return new WaitForSeconds(tempoDeEfeito);
        personagem._dano -= dano;
        RemoverEfeito(personagem);
    }
}
