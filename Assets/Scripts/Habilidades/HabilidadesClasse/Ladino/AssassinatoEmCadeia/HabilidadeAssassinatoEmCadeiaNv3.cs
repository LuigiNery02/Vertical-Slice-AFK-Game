using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Ladino/Assassinato em Cadeia/Nv3")]
public class HabilidadeAssassinatoEmCadeiaNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private int valorMarcadores = 3;
    [SerializeField]
    private float multiplicadorAtaque = 0.3f;
    [SerializeField]
    private float tempoDeBuffDeAtaque = 5;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            personagem.efeitoPorMorteCausadaAtivada = true;

            personagem.AtivarEfeitoPorMorteCausada("AssassinatoEmCadeiaNv3", () =>
            {
                personagem.efeitoPorAtaqueAtivado = true;

                personagem.AtivarEfeitoPorAtaque("AssassinatoEmCadeiaNv3", (bool acerto) =>
                {
                    if (acerto)
                    {
                        personagem._personagemAlvo.AtualizarMarcadoresDeAlvo(valorMarcadores, true);
                        personagem.RemoverEfeitoPorAtaque("AssassinatoEmCadeiaNv3");
                        personagem.StartCoroutine(EsperarBuffAtaque(personagem));
                    }
                });
            });
        } 
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("AssassinatoEmCadeiaNv3");
        personagem.RemoverEfeitoPorMorteCausada("AssassinatoEmCadeiaNv3");
    }

    IEnumerator EsperarBuffAtaque(IAPersonagemBase personagem)
    {
        float danoOriginal = personagem._dano;
        personagem._dano += (danoOriginal * multiplicadorAtaque);
        yield return new WaitForSeconds(tempoDeBuffDeAtaque);
        personagem._dano = danoOriginal;
    }
}
