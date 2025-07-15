using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Espada/Corte Rápido/Nv2")]

public class HabilidadeCorteRapidoNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 2.5f;
    public GameObject vfx;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaArma)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.efeitoPorAtaqueAtivado = true;
                personagem.podeAtivarEfeitoHabilidadeAtivaArma = false;
                personagem.GastarSP(custoDeMana);

                float danoOriginal = personagem._dano;
                int golpes = 0;

                personagem.AtivarEfeitoPorAtaque("CorteRapidoNv2", (bool acerto) =>
                {
                    if (acerto)
                    {
                        if (personagem.vfxHabilidadeAtivaArma == null)
                        {
                            GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                            personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
                        }
                        else
                        {
                            personagem.GerenciarVFXHabilidade(2, true);
                        }

                        golpes++;
                        personagem._dano *= multiplicadorDeDano;
                        personagem.StartCoroutine(EsperarFrame(personagem, danoOriginal, golpes));
                    }
                    else
                    {
                        golpes++;
                        if(golpes > 1)
                        {
                            RemoverEfeito(personagem);
                        }
                    }
                });

                personagem.movimentoEspecial = "CorteRapido";
                personagem.VerificarComportamento("movimentoEspecial");
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("CorteRapidoNv2");
        personagem.efeitoPorAtaqueAtivado = false;
        personagem.GerenciarVFXHabilidade(2, false);
        base.RemoverEfeito(personagem);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem, float dano, int golpes)
    {
        yield return null; //agurada um frame
        if(golpes > 1)
        {
            personagem._dano = dano;
            RemoverEfeito(personagem);
        }
    }
}
