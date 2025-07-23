using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Arco/Flecha Estatica/Nv1")]
public class HabilidadeFlechaEstaticaNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private int numeroDeRebates = 1;
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
                    personagem.efeitoPorAtaqueAtivado = true;

                    personagem.AtivarEfeitoPorAtaque("FlechaEstaticaNv1", (bool acerto) =>
                    {
                        if (acerto)
                        {
                            if(personagem.rebatesRestantesFlechaEstatica >= numeroDeRebates)
                            {
                                RemoverEfeito(personagem);
                            }
                        }
                        else
                        {
                            RemoverEfeito(personagem);
                        }
                    });

                    personagem.rebaterHit = true;
                    personagem.numeroDeRebatesDoHit = numeroDeRebates;

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
        personagem.RemoverEfeitoPorAtaque("FlechaEstaticaNv1");
        personagem.rebaterHit = false;
        personagem.numeroDeRebatesDoHit = 0;
        personagem.rebatesRestantesFlechaEstatica = 0;
        personagem.GerenciarVFXHabilidade(2, false);
        base.RemoverEfeito(personagem);
    }
}
