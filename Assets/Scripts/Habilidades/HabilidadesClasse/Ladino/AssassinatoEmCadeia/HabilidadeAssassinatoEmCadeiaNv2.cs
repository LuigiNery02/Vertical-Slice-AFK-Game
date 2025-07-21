using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Ladino/Assassinato em Cadeia/Nv2")]
public class HabilidadeAssassinatoEmCadeiaNv2 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private int valorMarcadores = 3;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            personagem.efeitoPorMorteCausadaAtivada = true;

            personagem.AtivarEfeitoPorMorteCausada("AssassinatoEmCadeiaNv2", () =>
            {
                personagem.efeitoPorAtaqueAtivado = true;

                personagem.AtivarEfeitoPorAtaque("AssassinatoEmCadeiaNv2", (bool acerto) =>
                {
                    if (acerto)
                    {
                        personagem._personagemAlvo.AtualizarMarcadoresDeAlvo(valorMarcadores, true);
                        personagem.RemoverEfeitoPorAtaque("AssassinatoEmCadeiaNv2");
                    }
                });
            });
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("AssassinatoEmCadeiaNv2");
        personagem.RemoverEfeitoPorMorteCausada("AssassinatoEmCadeiaNv2");
    }
}
