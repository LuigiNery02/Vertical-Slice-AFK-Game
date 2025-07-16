using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Espada/Ataque Pesado/Nv2")]
public class HabilidadeAtaquePesadoNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 2;
    [SerializeField]
    private float tempoDeStun = 2.5f;
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaArma)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                float danoOriginal = personagem._dano;

                personagem.efeitoPorAtaqueAtivado = true;
                personagem.podeAtivarEfeitoHabilidadeAtivaArma = false;

                personagem.GastarSP(custoDeMana);

                personagem.AtivarEfeitoPorAtaque("AtaquePesadoNv2", (bool acerto) =>
                {
                    if (acerto)
                    {
                        personagem._dano *= multiplicadorDeDano;

                        IAPersonagemBase inimigo = personagem._personagemAlvo;

                        if (!inimigo.stunado)
                        {
                            inimigo.tempoDeStun = tempoDeStun;
                            inimigo.VerificarComportamento("stun");
                        }
                        personagem.StartCoroutine(EsperarFrame(personagem, danoOriginal));
                    }
                    else
                    {
                        RemoverEfeito(personagem);
                    }

                });

                if (personagem.vfxHabilidadeAtivaArma == null)
                {
                    GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                    personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
                }
                else
                {
                    personagem.GerenciarVFXHabilidade(2, true);
                }
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("AtaquePesadoNv2");
        personagem.efeitoPorAtaqueAtivado = false;
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(2, false);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem, float dano)
    {
        yield return null; //agurada um frame
        personagem._dano = dano;
        RemoverEfeito(personagem);
    }
}
