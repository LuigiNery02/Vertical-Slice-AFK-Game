using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Guerreiro/Ultimo Pilar/Nv1")]
public class HabilidadeUltimoPilarNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorBonusHP = 0.01f;
    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }

            var dados = personagem.dadosDasHabilidadesPassivas[this];

            void CurarAoGastarWillPower(int quantidade)
            {
                float cura = personagem.personagem.hp * multiplicadorBonusHP * quantidade;
                personagem.ReceberHP(cura);
            }

            dados.eventoWillPowerLideranca = CurarAoGastarWillPower;

            personagem.aoGastarWillPower += CurarAoGastarWillPower;
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            if (dados.eventoWillPowerLideranca != null)
            {
                personagem.aoGastarWillPower -= dados.eventoWillPowerLideranca;
            }

            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }
    }
}
