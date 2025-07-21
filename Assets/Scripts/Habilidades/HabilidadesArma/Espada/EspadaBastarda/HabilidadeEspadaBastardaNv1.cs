using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Espada/Espada Bastarda/Nv1")]
public class HabilidadeEspadaBastardaNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicacaoDiminuicaoDeDano = 0.1f;
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadePassivaArma)
        {
            if (base.ChecarRuna(personagem, nivel))
            {
                personagem.efeitoPorAtaqueAtivado = true;

                personagem.AtivarEfeitoPorAtaque("EspadaBastardaNv1", (bool acerto) =>
                {
                    if (acerto)
                    {
                        IAPersonagemBase inimigo = personagem._personagemAlvo;

                        if(inimigo._comportamento != EstadoDoPersonagem.MORTO && !inimigo.medo)
                        {
                            inimigo.medo = true;
                            inimigo.reducaoDanoMedo = multiplicacaoDiminuicaoDeDano;
                            personagem.vfxHabilidadePassivaArma = null;

                            if (personagem.vfxHabilidadePassivaArma == null)
                            {
                                GameObject vfxInstanciado = GameObject.Instantiate(vfx, inimigo.transform.position + Vector3.zero, inimigo.transform.rotation, inimigo.transform);
                                personagem.vfxHabilidadePassivaArma = vfxInstanciado;
                                Destroy(vfxInstanciado, 2);
                            }
                        }
                    }
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("EspadaBastardaNv1");
        base.RemoverEfeito(personagem);
    }
}
