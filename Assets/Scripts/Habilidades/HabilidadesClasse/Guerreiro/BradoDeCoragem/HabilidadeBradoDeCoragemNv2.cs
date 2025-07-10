using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Brado de Coragem/Nv2")]

public class HabilidadeBradoDeCoragemNv2 : HabilidadeAtiva
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

                personagem.GastarSP(custoDeMana);

                personagem.imuneAStun = true;
                personagem.imuneAKnockback = true;

                if (personagem.vfxHabilidadeAtivaClasse == null)
                {
                    GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                    personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
                }
                else
                {
                    personagem.GerenciarVFXHabilidade(1, true);
                }

                base.AtivarEfeito(personagem);
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.imuneAStun = false;
        personagem.imuneAKnockback = false;
        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }
}
