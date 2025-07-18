using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Golpe Determinado/Nv2")]
public class HabilidadeGolpeDeterminadoNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 3;
    [SerializeField]
    private int consumoDeWillPower = 3;
    [SerializeField]
    private float ignorarDefesaInimigo = 25;
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

                    personagem.AtivarEfeitoPorAtaque("GolpeDeterminadoNv2", (bool acerto) =>
                    {
                        if (acerto)
                        {
                            personagem._dano *= multiplicadorDeDano;
                            IAPersonagemBase inimigo = personagem._personagemAlvo;
                            float defesaOriginal = inimigo.defesa;
                            inimigo.defesa -= ignorarDefesaInimigo;
                            if (inimigo.defesa <= 0)
                            {
                                inimigo.defesa = 0;
                            }
                            personagem.StartCoroutine(EsperarFrame(personagem, inimigo, danoOriginal, defesaOriginal));
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
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        
        personagem.RemoverEfeitoPorAtaque("GolpeDeterminadoNv2");
        personagem.efeitoPorAtaqueAtivado = false;
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(1, false);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem, IAPersonagemBase inimigo ,float dano, float defesa)
    {
        yield return null; //agurada um frame
        personagem._dano = dano;
        if (inimigo == personagem._personagemAlvo)
        {
            inimigo.defesa = defesa;
        }
        RemoverEfeito(personagem);
    }
}
