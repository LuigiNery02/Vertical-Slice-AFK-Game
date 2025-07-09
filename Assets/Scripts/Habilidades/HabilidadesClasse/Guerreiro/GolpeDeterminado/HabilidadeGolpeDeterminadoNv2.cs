using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Ativa/Classe/Guerreiro/Golpe Determinado/Nv2")]
public class HabilidadeGolpeDeterminadoNv2 : HabilidadeAtiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 3;
    [SerializeField]
    private int consumoDeWillPower = 3;
    [SerializeField]
    private float ignorarDefesaInimigo = 25;
    public GameObject vfx;

    private float _personagemDanoOriginal;
    private float _inimigoDefesaOriginal;
    private IAPersonagemBase _inimigo;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (personagem.podeAtivarEfeitoHabilidade1)
        {
            if (base.ChecarAtivacao(personagem) && personagem.willPower >= consumoDeWillPower)
            {
                _personagemDanoOriginal = personagem._dano;
                personagem.efeitoPorAtaque = null;
                personagem.efeitoPorAtaqueAtivado = true;
                personagem.podeAtivarEfeitoHabilidade1 = false;

                personagem.AtualizarWillPower(consumoDeWillPower, false);
                personagem.GastarSP(custoDeMana);

                personagem.efeitoPorAtaque = (bool acerto) =>
                {
                    if (acerto)
                    {
                        personagem._dano *= multiplicadorDeDano;
                        _inimigo = personagem._personagemAlvo;
                        _inimigoDefesaOriginal = _inimigo.defesa;
                        _inimigo.defesa -= ignorarDefesaInimigo;
                        if(_inimigo.defesa <= 0)
                        {
                            _inimigo.defesa = 0;
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
                    GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
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
        if(_inimigo == personagem._personagemAlvo)
        {
            _inimigo.defesa = _inimigoDefesaOriginal;
        }
        personagem.efeitoPorAtaque = null;
        personagem.efeitoPorAtaqueAtivado = false;
        base.RemoverEfeito(personagem);
        personagem.GerenciarVFXHabilidade(1, false);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem)
    {
        yield return null; //agurada um frame
        RemoverEfeito(personagem);
    }
}
