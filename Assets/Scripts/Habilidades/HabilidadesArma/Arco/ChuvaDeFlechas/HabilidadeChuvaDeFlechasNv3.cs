using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Arma/Arco/Chuva de Flechas/Nv3")]
public class HabilidadeChuvaDeFlechasNv3 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 2;
    [SerializeField]
    private float raioVfx = 10;
    [SerializeField]
    private float tempoDeVfx = 1;
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
                    personagem.movimentoEspecial = "ChuvaDeFlechas";
                    personagem.VerificarComportamento("movimentoEspecial");

                    personagem.StartCoroutine(TempoDeVfx(personagem));
                });
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.GerenciarVFXHabilidade(2, false);
        personagem.FinalizarMovimentoEspecial();
        base.RemoverEfeito(personagem);
    }

    IEnumerator TempoDeVfx(IAPersonagemBase personagem)
    {
        float dano = personagem._dano * multiplicadorDeDano;

        Vector3 direcaoFrente = personagem.transform.forward;
        Vector3 offsetAleatorio = personagem.transform.right * Random.Range(-raioVfx / 2f, raioVfx / 2f);

        Vector3 posicaoAlvo = personagem.transform.position + direcaoFrente * Random.Range(3f, 6f) + offsetAleatorio;

        GameObject vfxInstanciado = GameObject.Instantiate(vfx, posicaoAlvo, Quaternion.identity);
        personagem.vfxHabilidadeAtivaArma = vfxInstanciado;
        vfxInstanciado.transform.localScale = (Vector3.one * raioVfx) / 9;

        Collider[] colliders = Physics.OverlapSphere(posicaoAlvo, raioVfx / 2f);

        foreach (var collider in colliders)
        {
            IAPersonagemBase inimigo = collider.GetComponent<IAPersonagemBase>();

            if (inimigo != null && inimigo.controlador != personagem.controlador && inimigo._comportamento != EstadoDoPersonagem.MORTO)
            {
                inimigo.SofrerDano(dano, false, personagem);
            }
        }

        yield return new WaitForSeconds(tempoDeVfx);

        RemoverEfeito(personagem);
    }
}
