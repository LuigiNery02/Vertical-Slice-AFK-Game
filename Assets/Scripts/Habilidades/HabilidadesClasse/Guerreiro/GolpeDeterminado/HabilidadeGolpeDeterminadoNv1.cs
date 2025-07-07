using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Golpe Determinado/Nv1")]
public class HabilidadeGolpeDeterminadoNv1 : HabilidadeAtiva
{
    public float multiplicadorDeDano = 2f;
    public int consumoDeWillPower = 3;
    public GameObject vfx;

    private float _personagemDanoOriginal;
    private bool efeitoAtivado = false;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        Debug.Log("porra");
        if (!efeitoAtivado)
        {
            Debug.Log("porra2");
            if (base.ChecarAtivacao(personagem) && personagem.willPower >= consumoDeWillPower)
            {
                efeitoAtivado = true;
                _personagemDanoOriginal = personagem._dano;
                personagem.efeitoPorAtaque = null;
                personagem.efeitoPorAtaqueAtivado = true;
                personagem.podeAtivarEfeitoHabilidade1 = false;

                Instantiate(vfx);
                vfx.transform.parent = personagem.transform;
                vfx.transform.localPosition = Vector3.zero;

                personagem.AtualizarWillPower(consumoDeWillPower, false);
                personagem.GastarSP(custoDeMana);
                Debug.Log("porra3");

                personagem.efeitoPorAtaque = (bool acerto) =>
                {
                    if (acerto)
                    {
                        Debug.Log("porra4");
                        personagem._dano *= multiplicadorDeDano;
                        personagem.StartCoroutine(EsperarFrame(personagem));
                    }
                    else
                    {
                        Debug.Log("porra5");
                        RemoverEfeito(personagem);
                    }

                };
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem._dano = _personagemDanoOriginal;
        personagem.efeitoPorAtaque = null;
        personagem.efeitoPorAtaqueAtivado = false;
        personagem.podeAtivarEfeitoHabilidade1 = false;
        efeitoAtivado = false;
        Destroy(vfx);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem)
    {
        yield return null; //agurada um frame
        RemoverEfeito(personagem);
    }
}
