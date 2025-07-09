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

    private float _personagemDanoOriginal;
    private float _inimigoDanoOriginal;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidade1)
        {
            if (base.ChecarAtivacao(personagem))
            {
                _personagemDanoOriginal = personagem._dano;
                personagem.efeitoPorAtaque = null;
                personagem.efeitoPorAtaqueAtivado = true;
                personagem.podeAtivarEfeitoHabilidade1 = false;

                personagem.GastarSP(custoDeMana);

                personagem.efeitoPorAtaque = (bool acerto) =>
                {
                    if (acerto)
                    {
                        personagem._dano *= multiplicadorDeDano;

                        IAPersonagemBase inimigo = personagem._personagemAlvo;

                        if (inimigo != null && !inimigo.recebeuDebuffPunhoDisciplina)
                        {
                            _inimigoDanoOriginal = inimigo._dano;
                            inimigo._dano *= multiplicadorDiminuicaoDeDanoInimigo;
                            inimigo.recebeuDebuffPunhoDisciplina = true;
                            inimigo.ataqueDiminuido = true;

                            personagem.StartCoroutine(TempoEfeitoDiminuirDano(inimigo));
                        }

                        personagem.StartCoroutine(EsperarFrame(personagem));
                    }
                    else
                    {
                        RemoverEfeito(personagem);
                    }
                };

                if (personagem.vfxHabilidade1 == null)
                {
                    GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position, personagem.transform.rotation, personagem.transform);
                    personagem.vfxHabilidade1 = vfxInstanciado;
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
        personagem._dano = _personagemDanoOriginal;
        personagem.efeitoPorAtaque = null;
        personagem.efeitoPorAtaqueAtivado = false;
        personagem.GerenciarVFXHabilidade(1, false);
        base.RemoverEfeito(personagem);
    }

    private IEnumerator EsperarFrame(IAPersonagemBase personagem)
    {
        yield return null;
        RemoverEfeito(personagem);
    }

    private IEnumerator TempoEfeitoDiminuirDano(IAPersonagemBase inimigo)
    {
        GameObject vfxInstanciado = GameObject.Instantiate(vfxInimigo, inimigo.transform.position, inimigo.transform.rotation, inimigo.transform);

        yield return new WaitForSeconds(tempoDiminuicaoDeDanoInimigo);

        if (inimigo != null)
        {
            inimigo._dano = _inimigoDanoOriginal;
        }

        inimigo.recebeuDebuffPunhoDisciplina = false;
        inimigo.ataqueDiminuido = false;

        if (vfxInstanciado != null)
        {
            GameObject.Destroy(vfxInstanciado);
        }
    }
}
