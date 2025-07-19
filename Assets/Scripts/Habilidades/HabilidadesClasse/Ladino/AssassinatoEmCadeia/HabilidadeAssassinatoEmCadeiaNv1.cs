using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Ladino/Assassinato em Cadeia/Nv1")]
public class HabilidadeAssassinatoEmCadeiaNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float porcentagemChanceDeEfeito = 25;
    [SerializeField]
    private int valorMarcadores = 1;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            personagem.efeitoPorAtaqueAtivado = true;

            personagem.AtivarEfeitoPorAtaque("AssassinatoEmCadeiaNv1", (bool acerto) =>
            {
                if (acerto)
                {
                    if (CalcularChanceDeEfeito(porcentagemChanceDeEfeito))
                    {
                        personagem._personagemAlvo.AtualizarMarcadoresDeAlvo(1, true);
                    }
                }
            });
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("AssassinatoEmCadeiaNv1");
        personagem.efeitoPorAtaqueAtivado = false;
    }

    private bool CalcularChanceDeEfeito(float chanceDeEfeito)
    {
        int rng = Random.Range(0, 100);

        return rng < chanceDeEfeito;
    }
}
