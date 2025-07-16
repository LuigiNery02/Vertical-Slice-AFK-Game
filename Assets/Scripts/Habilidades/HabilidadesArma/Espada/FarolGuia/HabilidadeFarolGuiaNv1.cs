using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Espada/Faról Guia/Nv1")]
public class HabilidadeFarolGuiaNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float porcentagemDanoCausado = 0.05f;
    [SerializeField]
    private float probabilidadeDeCura = 3;
    public GameObject vfx;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadePassivaArma)
        {
            if (base.ChecarRuna(personagem, nivel))
            {
                personagem.efeitoPorDanoCausadoAtivado = true;

                personagem.AtivarEfeitoPorDanoCausado("FarolGuiaNv1", () =>
                {
                    if (CalcularProbabilidadeDeCura())
                    {
                        float danoCausado = personagem._dano - personagem._personagemAlvo.defesa;
                        danoCausado *= porcentagemDanoCausado;

                        personagem.ReceberHP(danoCausado);

                        if (personagem.vfxHabilidadePassivaArma == null)
                        {
                            GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                            personagem.vfxHabilidadePassivaArma = vfxInstanciado;
                        }
                        else
                        {
                            personagem.GerenciarVFXHabilidade(4, true);
                        }

                        personagem.StartCoroutine(EsperarVFX(personagem));
                    }
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorDanoCausado("FarolGuiaNv1");
        personagem.efeitoPorDanoCausadoAtivado = false;
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(4, false);
    }

    private bool CalcularProbabilidadeDeCura()
    {
        int rng = Random.Range(0, 100);

        return rng < probabilidadeDeCura;
    }

    IEnumerator EsperarVFX(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(2);
        personagem.GerenciarVFXHabilidade(4, false);
    }
}
