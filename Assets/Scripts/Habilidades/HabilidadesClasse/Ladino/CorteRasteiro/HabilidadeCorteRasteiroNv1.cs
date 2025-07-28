using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Ladino/Corte Rasteiro/Nv1")]
public class HabilidadeCorteRasteiroNv1 : HabilidadeAtiva
{
    [SerializeField]
    private float multiplicadorDeAtaque = 1.5f;
    [SerializeField]
    private int numeroDeMarcadores = 1;
    [SerializeField]
    private GameObject hit;

    [Header("Configurações Hit")]
    [SerializeField]
    public float velocidadeDeMovimento = 20;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                personagem.GastarSP(custoDeMana);

                base.ChecarCastingHabilidade1(personagem, () =>
                {
                    float dano = personagem._dano * multiplicadorDeAtaque;

                    //movimento especial
                    personagem.movimentoEspecial = "CorteRasteiro";
                    personagem.VerificarComportamento("movimentoEspecial");

                    personagem.StartCoroutine(EsperarTempoParaInstanciarHit(personagem, dano));
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        base.RemoverEfeito(personagem);
    }

    IEnumerator EsperarTempoParaInstanciarHit(IAPersonagemBase personagem, float dano)
    {
        Transform posicao = null;

        yield return new WaitForSeconds(0.5f);
        //instancia o hit
        if(personagem.GetComponentInChildren<AtivarArmaPersonagem>() != null)
        {
            posicao = personagem.GetComponentInChildren<AtivarArmaPersonagem>().maoBone;
        }
        else
        {
            Vector3 posicaoValor = personagem.transform.position;
            posicaoValor.y += 1.5f;
            posicao.position = posicaoValor;
        }

        GameObject corteRasteiro = Instantiate(hit, posicao.position, hit.transform.rotation);

        HitAtaqueEspecial2Personagem hitComponente = corteRasteiro.GetComponent<HitAtaqueEspecial2Personagem>();
        if (hitComponente != null)
        {
            hitComponente._personagemPai = personagem;
            hitComponente.dano = dano;
            hitComponente.valorMarcadores = numeroDeMarcadores;
            hitComponente.MoverAteAlvo(personagem._personagemAlvo.transform, velocidadeDeMovimento);
        }
        RemoverEfeito(personagem);
    }
}
