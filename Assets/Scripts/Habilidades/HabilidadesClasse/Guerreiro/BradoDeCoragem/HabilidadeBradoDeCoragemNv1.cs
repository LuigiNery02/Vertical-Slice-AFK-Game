using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Brado de Coragem/Nv1")]

public class HabilidadeBradoDeCoragemNv1 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem))
            {
                personagem.podeAtivarEfeitoHabilidadeAtivaClasse = false;
                personagem.efeitoPorAtaqueRecebidoAtivado = true;

                personagem.GastarSP(custoDeMana);

                personagem.imuneAStun = true;
                personagem.imuneAKnockback = true;
                personagem.AtivarEfeitoPorAtaqueRecebido("BradoDeCoragemNv1", (bool acerto) =>
                {
                    if (acerto)
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
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.imuneAStun = false;
        personagem.imuneAKnockback = false;
        personagem.RemoverEfeitoPorAtaqueRecebido("BradoDeCoragemNv1");
        personagem.efeitoPorAtaqueRecebidoAtivado = false;
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(1, false);
    }
}
