using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Espada/Lâmina Extra/Nv3")]
public class HabilidadeLaminaExtraNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 0.05f;
    [SerializeField]
    private float buffDefesa = 5;
    [SerializeField]
    private float buffDefesaMagica = 5;
    [SerializeField]
    private float multiplicadorDeHP = 0.05f;
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

                    float hp = personagem._hpMaximoEInicial * multiplicadorDeHP;
                    personagem._hpMaximoEInicial += hp;

                    personagem.StartCoroutine(EsperarTempoDeEfeito(personagem, dano, hp));

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
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(2, false);
    }

    IEnumerator EsperarTempoDeEfeito(IAPersonagemBase personagem, float dano, float hp)
    {
        yield return new WaitForSeconds(tempoDeEfeito);
        personagem._dano -= dano;
        personagem.defesa -= buffDefesa;
        personagem.defesaMagica -= buffDefesaMagica;
        personagem._hpMaximoEInicial -= hp;
        RemoverEfeito(personagem);
    }
}
