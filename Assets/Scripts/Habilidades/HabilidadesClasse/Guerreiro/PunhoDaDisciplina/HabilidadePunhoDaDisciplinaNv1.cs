using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Punho da Disciplina/Nv1")]

public class HabilidadePunhoDaDisciplinaNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 1.5f;
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
                    personagem.efeitoPorAtaqueAtivado = true;

                    float danoOriginal = personagem._dano;

                    personagem.AtivarEfeitoPorAtaque("PunhoDaDisciplinaNv1", (bool acerto) =>
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
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("PunhoDaDisciplinaNv1");
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
