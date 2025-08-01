using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Espada/Far�l Guia/Nv3")]
public class HabilidadeFarolGuiaNv3 : HabilidadePassiva
{
    [Header("Configura��es Habilidade")]
    [SerializeField]
    private float porcentagemDanoCausado = 0.08f;
    [SerializeField]
    private float probabilidadeDeCura = 5;
    public GameObject vfx;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadePassivaArma)
        {
            if (base.ChecarRuna(personagem, nivel))
            {
                personagem.efeitoPorDanoCausadoAtivado = true;

                personagem.AtivarEfeitoPorDanoCausado("FarolGuiaNv3", () =>
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
        personagem.RemoverEfeitoPorDanoCausado("FarolGuiaNv3");
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
