using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Guerreiro/Ascender/Nv1")]
public class HabilidadeAscenderNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField] 
    private int efeitoPorWillPowerGasto = 2;
    [SerializeField] 
    private float multiplicadorBonusAtaque = 0.1f;
    [SerializeField] 
    private float tempoDeEfeito = 2;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.ContainsKey(this))
            {
                personagem.dadosDasHabilidadesPassivas[this] = new DadosHabilidadePassiva();
            }

            var dados = personagem.dadosDasHabilidadesPassivas[this];

            dados.buffsAtaqueAtivos ??= new List<Coroutine>();
            dados.bonusAplicados ??= new List<float>();

            personagem.aoGastarWillPower += (int quantidade) =>
            {
                int blocos = quantidade / efeitoPorWillPowerGasto;

                for (int i = 0; i < blocos; i++)
                {
                    Coroutine buff = personagem.StartCoroutine(AplicarBuffTemporario(personagem, dados));
                    dados.buffsAtaqueAtivos.Add(buff);
                }
            };
        } 
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (!personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            return;
        }  

        if (dados.buffsAtaqueAtivos != null)
        {
            foreach (var buff in dados.buffsAtaqueAtivos)
            {
                if (buff != null)
                    personagem.StopCoroutine(buff);
            }
            dados.buffsAtaqueAtivos.Clear();
        }

        if (dados.bonusAplicados != null)
        {
            foreach (var bonus in dados.bonusAplicados)
            {
                personagem._dano -= bonus;
            }
            dados.bonusAplicados.Clear();
        }

        personagem.dadosDasHabilidadesPassivas.Remove(this);
    }

    private IEnumerator AplicarBuffTemporario(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        float danoBase = personagem.personagem.arma.dano;
        float bonus = danoBase * multiplicadorBonusAtaque;

        personagem._dano += bonus;
        dados.bonusAplicados.Add(bonus);

        yield return new WaitForSeconds(tempoDeEfeito);

        personagem._dano -= bonus;
        dados.bonusAplicados.Remove(bonus);
    }
}
