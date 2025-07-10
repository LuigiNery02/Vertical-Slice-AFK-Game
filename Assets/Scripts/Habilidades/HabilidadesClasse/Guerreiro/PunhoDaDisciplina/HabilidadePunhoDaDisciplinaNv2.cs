using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Punho da Disciplina/Nv2")]
public class HabilidadePunhoDaDisciplinaNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 1.5f;
    [SerializeField]
    private float multiplicadorDiminuicaoDeDanoInimigo = 0.8f;
    [SerializeField]
    private float tempoDiminuicaoDeDanoInimigo = 4f;
    public GameObject vfx;
    public GameObject vfxInimigo;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem))
            {
                float danoOriginal = personagem._dano;
                personagem.efeitoPorAtaque = null;
                personagem.efeitoPorAtaqueAtivado = true;
                personagem.podeAtivarEfeitoHabilidadeAtivaClasse = false;

                personagem.GastarSP(custoDeMana);

                personagem.AtivarEfeitoPorAtaque("GolpeDeterminadoNv2", (bool acerto) =>
                {
                    if (acerto)
                    {
                        personagem._dano *= multiplicadorDeDano;

                        IAPersonagemBase inimigo = personagem._personagemAlvo;

                        if (inimigo != null && !inimigo.recebeuDebuffPunhoDisciplina)
                        {
                            inimigo.recebeuDebuffPunhoDisciplina = true;
                            inimigo.ataqueDiminuido = true;

                            float inimigoDanoOriginal = inimigo._dano;
                            inimigo._dano *= multiplicadorDiminuicaoDeDanoInimigo;

                            personagem.StartCoroutine(TempoEfeitoDiminuirDano(inimigo, inimigoDanoOriginal));
                        }

                        personagem.StartCoroutine(EsperarFrame(personagem, danoOriginal));
                    }
                    else
                    {
                        RemoverEfeito(personagem);
                    }
                });

                if (personagem.vfxHabilidadeAtivaClasse == null)
                {
                    GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position, personagem.transform.rotation, personagem.transform);
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
        personagem.RemoverEfeitoPorAtaque("GolpeDeterminadoNv2");
        personagem.efeitoPorAtaqueAtivado = false;
        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }

    private IEnumerator EsperarFrame(IAPersonagemBase personagem, float dano)
    {
        yield return null;
        personagem._dano = dano;
        RemoverEfeito(personagem);
    }

    private IEnumerator TempoEfeitoDiminuirDano(IAPersonagemBase inimigo, float danoOriginal)
    {
        GameObject vfxInstanciado = null;
        if (vfxInimigo != null)
        {
            vfxInstanciado = GameObject.Instantiate(vfxInimigo, inimigo.transform.position, inimigo.transform.rotation, inimigo.transform);
        }

        yield return new WaitForSeconds(tempoDiminuicaoDeDanoInimigo);

        if (inimigo != null)
        {
            inimigo._dano = danoOriginal;
            inimigo.recebeuDebuffPunhoDisciplina = false;
            inimigo.ataqueDiminuido = false;
        }

        if (vfxInstanciado != null)
        {
            GameObject.Destroy(vfxInstanciado);
        }
    }
}
