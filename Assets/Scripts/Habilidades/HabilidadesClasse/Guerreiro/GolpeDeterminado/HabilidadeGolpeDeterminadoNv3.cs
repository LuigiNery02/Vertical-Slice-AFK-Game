using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Golpe Determinado/Nv3")]
public class HabilidadeGolpeDeterminadoNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 4;
    [SerializeField]
    private int consumoDeWillPower = 3;
    [SerializeField]
    private float ignorarDefesaInimigo = 25;
    [SerializeField]
    private float probabilidadeDeStun = 25;
    [SerializeField]
    private int tempoDeStun = 2;
    public GameObject vfx;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidadeAtivaClasse)
        {
            if (base.ChecarAtivacao(personagem) && personagem.willPower >= consumoDeWillPower)
            {
                float danoOriginal = personagem._dano;

                personagem.efeitoPorAtaqueAtivado = true;
                personagem.podeAtivarEfeitoHabilidadeAtivaClasse = false;

                personagem.AtualizarWillPower(consumoDeWillPower, false);
                personagem.GastarSP(custoDeMana);

                personagem.AtivarEfeitoPorAtaque("GolpeDeterminadoNv3", (bool acerto) =>
                {
                    if (acerto)
                    {
                        personagem._dano *= multiplicadorDeDano;

                        IAPersonagemBase inimigo = personagem._personagemAlvo;

                        float defesaOriginal = inimigo.defesa;
                        inimigo.defesa -= ignorarDefesaInimigo;

                        inimigo.defesa -= ignorarDefesaInimigo;
                        if (inimigo.defesa <= 0)
                        {
                            inimigo.defesa = 0;
                        }
                        if (CalcularProbabiilidadeDeStun() && !inimigo.stunado)
                        {
                            inimigo.tempoDeStun = tempoDeStun;
                            inimigo.VerificarComportamento("stun");
                        }
                        personagem.StartCoroutine(EsperarFrame(personagem, inimigo, danoOriginal, defesaOriginal));
                    }
                    else
                    {
                        RemoverEfeito(personagem);
                    }

                });

                if (personagem.vfxHabilidadeAtivaClasse == null)
                {
                    GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
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
        personagem.RemoverEfeitoPorAtaque("GolpeDeterminadoNv3");
        personagem.efeitoPorAtaqueAtivado = false;
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(1, false);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem, IAPersonagemBase inimigo, float dano, float defesa)
    {
        yield return null; //agurada um frame
        personagem._dano = dano;
        if (inimigo == personagem._personagemAlvo)
        {
            inimigo.defesa = defesa;
        }
        RemoverEfeito(personagem);
    }

    private bool CalcularProbabiilidadeDeStun()
    {
        int rng = Random.Range(0, 100);

        return rng < probabilidadeDeStun;
    }
}
