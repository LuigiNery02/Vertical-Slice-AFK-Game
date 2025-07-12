using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Espada/Onda de Choque/Nv3")]
public class HabilidadeOndaDeChoqueNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeAtaque = 3.5f;
    [SerializeField]
    private GameObject hit;

    [Header("Configurações de Expansão Hit")]
    [SerializeField]
    private float velocidadeDeCrescimento = 5f;
    [SerializeField]
    private float escalaMaxima = 20;
    [SerializeField]
    private float duracao = 3;
    [SerializeField]
    public float velocidadeDeMovimento = 3;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaArma)
        {
            if (base.ChecarAtivacao(personagem) && base.ChecarRuna(personagem, nivel))
            {
                float dano = personagem._dano * multiplicadorDeAtaque;

                //movimento especial
                personagem.VerificarComportamento("movimentoEspecial");

                //instancia o hit 
                Vector3 posicaoInstancia = personagem.transform.position + personagem.transform.forward * 1.5f;
                GameObject ondaDeChoque = Instantiate(hit, posicaoInstancia, hit.transform.rotation);

                HitAtaqueEspecial1Personagem hitComponente = ondaDeChoque.GetComponent<HitAtaqueEspecial1Personagem>();
                if (hitComponente != null)
                {
                    hitComponente._personagemPai = personagem;
                    hitComponente.dano = dano;
                    hitComponente.velocidadeDeCrescimento = velocidadeDeCrescimento;
                    hitComponente.escalaMaxima = escalaMaxima;
                    hitComponente.duracao = duracao;
                    hitComponente.velocidadeDeMovimento = velocidadeDeMovimento;
                    hitComponente.direcaoDeMovimento = personagem.transform.forward;
                }
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {

    }
}
