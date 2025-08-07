using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Sacerdote/Escudo de Luz/Nv3")]
public class HabilidadeEscudoDeLuzNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private int barreiraProjetilValor = 15;
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
                    personagem.barreiraProjetil = true;
                    personagem.barreiraProjetilValor = barreiraProjetilValor;

                    if (personagem.vfxHabilidadeAtivaClasse == null)
                    {
                        GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                        personagem.vfxHabilidadeAtivaClasse = vfxInstanciado;
                    }
                    else
                    {
                        personagem.GerenciarVFXHabilidade(1, true);
                    }

                    base.ChecarEfeitosAoAtivarHabilidade(personagem);
                    base.AtivarEfeito(personagem);
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.barreiraProjetil = false;
        personagem.barreiraProjetilValor = 0;
        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }
}
