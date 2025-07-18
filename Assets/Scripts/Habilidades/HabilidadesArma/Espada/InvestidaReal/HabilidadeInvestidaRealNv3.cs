using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Espada/Investida Real/Nv3")]
public class HabilidadeInvestidaRealNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float distanciaMaxima = 10;
    [SerializeField]
    private float velocidadeDeInvestida = 20;
    [SerializeField]
    private float tempoDeStun = 0.5f;
    [SerializeField]
    private float multilpicadorAtaque = 2;
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
                    personagem.StartCoroutine(ExecutarInvestida(personagem));
                });
            }
        }
    }

    private IEnumerator ExecutarInvestida(IAPersonagemBase personagem)
    {
        personagem.movimentoEspecial = "InvestidaReal";
        personagem.VerificarComportamento("movimentoEspecial");

        Transform alvo = personagem._alvoAtual;
        Vector3 direcao = (alvo.position - personagem.transform.position).normalized;

        float distanciaPercorrida = 0;

        if (personagem.vfxHabilidadeAtivaArma == null)
        {
            GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
            vfxInstanciado.transform.SetParent(personagem.transform);
            personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
        }
        else
        {
            personagem.GerenciarVFXHabilidade(2, true);
        }

        while (distanciaPercorrida < distanciaMaxima)
        {
            float distanciaAteAlvo = Vector3.Distance(personagem.transform.position, alvo.position);

            if (distanciaAteAlvo <= 2f)
            {
                personagem._personagemAlvo.CancelarHabilidade();
                personagem._personagemAlvo.tempoDeStun = tempoDeStun;
                personagem._personagemAlvo.VerificarComportamento("stun");

                float dano = (personagem._dano * multilpicadorAtaque);
                personagem._personagemAlvo.SofrerDano(dano, false);
                personagem.GerenciarVFXHabilidade(2, false);

                RemoverEfeito(personagem);
                break;
            }

            float deslocamento = velocidadeDeInvestida * Time.deltaTime;
            personagem.transform.position += direcao * deslocamento;
            personagem.transform.forward = direcao;

            distanciaPercorrida += deslocamento;

            yield return null;
        }

        personagem.VerificarComportamento("perseguir");
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        base.RemoverEfeito(personagem);
    }
}
