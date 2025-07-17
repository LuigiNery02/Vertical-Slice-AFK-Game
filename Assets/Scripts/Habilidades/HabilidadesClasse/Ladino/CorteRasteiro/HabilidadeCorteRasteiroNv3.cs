using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Ladino/Corte Rasteiro/Nv3")]
public class HabilidadeCorteRasteiroNv3 : HabilidadeAtiva
{
    [SerializeField]
    private float multiplicadorDeAtaque = 3f;
    [SerializeField]
    private int numeroDeMarcadores = 3;
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
                personagem.podeAtivarEfeitoHabilidadeAtivaClasse = false;
                personagem.GastarSP(custoDeMana);

                float dano = personagem._dano * multiplicadorDeAtaque;

                //movimento especial
                personagem.movimentoEspecial = "CorteRasteiro";
                personagem.VerificarComportamento("movimentoEspecial");

                //instancia o hit 
                Transform posicao = personagem.GetComponentInChildren<AtivarArmaPersonagem>().maoBone;
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
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        base.RemoverEfeito(personagem);
    }
}
