using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Punho da Disciplina/Nv3")]
public class HabilidadePunhoDaDisciplinaNv3 : HabilidadeAtiva
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

    private Dictionary<IAPersonagemBase, Coroutine> debuffsAtivos = new Dictionary<IAPersonagemBase, Coroutine>();
    private Dictionary<IAPersonagemBase, float> danosOriginais = new Dictionary<IAPersonagemBase, float>();

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidade1 && base.ChecarAtivacao(personagem))
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
                    IAPersonagemBase inimigo = personagem._personagemAlvo;

                    float danoFinal = multiplicadorDeDano;
                    float debuffFinal = multiplicadorDiminuicaoDeDanoInimigo;
                    float tempoFinal = tempoDiminuicaoDeDanoInimigo;

                    if (inimigo != null && inimigo.VerificarEfeitoNegativo())
                    {
                        danoFinal *= 2;
                        debuffFinal *= debuffFinal;
                        tempoFinal *= 2;
                    }

                    personagem._dano *= danoFinal;

                    if (inimigo != null)
                    {
                        if (debuffsAtivos.ContainsKey(inimigo))
                        {
                            personagem.StopCoroutine(debuffsAtivos[inimigo]);
                            inimigo._dano = danosOriginais[inimigo];
                        }

                        danosOriginais[inimigo] = inimigo._dano;
                        inimigo._dano *= debuffFinal;
                        inimigo.recebeuDebuffPunhoDisciplina = true;
                        inimigo.ataqueDiminuido = true;

                        Coroutine corrotina = personagem.StartCoroutine(TempoEfeitoDiminuirDano(inimigo, tempoFinal));
                        debuffsAtivos[inimigo] = corrotina;
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

    private IEnumerator TempoEfeitoDiminuirDano(IAPersonagemBase inimigo, float tempo)
    {
        GameObject vfxInstanciado = GameObject.Instantiate(vfxInimigo, inimigo.transform.position, inimigo.transform.rotation, inimigo.transform);

        yield return new WaitForSeconds(tempo);

        if (inimigo != null && danosOriginais.ContainsKey(inimigo))
        {
            inimigo._dano = danosOriginais[inimigo];
            danosOriginais.Remove(inimigo);
            debuffsAtivos.Remove(inimigo);
        }

        inimigo.recebeuDebuffPunhoDisciplina = false;
        inimigo.ataqueDiminuido = false;

        if (vfxInstanciado != null)
            GameObject.Destroy(vfxInstanciado);
    }
}
