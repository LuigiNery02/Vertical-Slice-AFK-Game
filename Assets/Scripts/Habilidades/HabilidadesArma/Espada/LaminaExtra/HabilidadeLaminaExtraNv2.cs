using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Espada/Lâmina Extra/Nv2")]
public class HabilidadeLaminaExtraNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 0.05f;
    [SerializeField]
    private float buffDefesa = 5;
    [SerializeField]
    private float buffDefesaMagica = 5;
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

                    personagem.defesa += buffDefesa;

                    personagem.defesaMagica += buffDefesaMagica;

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
        personagem.defesa -= buffDefesa;
        personagem.defesaMagica -= buffDefesaMagica;
        RemoverEfeito(personagem);
    }
}
