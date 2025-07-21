using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Golpe Determinado/Nv1")]
public class HabilidadeGolpeDeterminadoNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 2;
    [SerializeField]
    private int consumoDeWillPower = 3;
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
                    personagem.efeitoPorAtaqueAtivado = true;

                    float danoOriginal = personagem._dano;

                    personagem.AtivarEfeitoPorAtaque("GolpeDeterminadoNv1", (bool acerto) =>
                    {
                        if (acerto)
                        {
                            personagem._dano *= multiplicadorDeDano;
                            personagem.StartCoroutine(EsperarFrame(personagem, danoOriginal));
                        }
                        else
                        {
                            RemoverEfeito(personagem);
                        }
                    });

                    if (personagem.vfxHabilidadeAtivaClasse == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(1, true);
                    }

                    if(personagem._personagemAlvo != null)
                    {
                        personagem.VerificarComportamento("perseguir");
                    }
                    else
                    {
                        personagem.VerificarComportamento("selecionarAlvo");
                    }
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("GolpeDeterminadoNv1");
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(1, false);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem, float dano)
    {
        yield return null; //agurada um frame
        personagem._dano = dano;
        RemoverEfeito(personagem);
    }
}
