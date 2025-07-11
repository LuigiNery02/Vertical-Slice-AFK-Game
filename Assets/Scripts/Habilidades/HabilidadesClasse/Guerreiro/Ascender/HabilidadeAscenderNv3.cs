using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Guerreiro/Ascender/Nv3")]
public class HabilidadeAscenderNv3 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private int efeitoPorWillPowerGasto = 2;
    [SerializeField]
    private float multiplicadorBonusAtaque = 0.1f;
    [SerializeField]
    private float bonusDefesas = 10;
    [SerializeField]
    private float bonusEscudo = 10;
    [SerializeField]
    private float tempoDeEfeito = 2f;
    public GameObject vfx;

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
                    if (!personagem.escudoAtivado)
                    {
                        personagem.escudoAtivado = true;
                        personagem.valorEscudo = (personagem._hpMaximoEInicial / bonusEscudo);
                        if (personagem.escudoVfx == null)
                        {
                            GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                            personagem.escudoVfx = vfxInstanciado;
                        }
                        else
                        {
                            personagem.escudoVfx.SetActive(true);
                        }
                        personagem.StartCoroutine(AplicarEscudoTemporario(personagem));
                    }
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
                {
                    personagem.StopCoroutine(buff);
                }
            }

            dados.buffsAtaqueAtivos.Clear();
        }

        if (dados.bonusAplicados != null)
        {
            int i = 0;
            while (i + 2 < dados.bonusAplicados.Count)
            {
                personagem._dano -= dados.bonusAplicados[i];
                personagem.defesa -= dados.bonusAplicados[i + 1];
                personagem.defesaMagica -= dados.bonusAplicados[i + 2];
                i += 3;
            }

            dados.bonusAplicados.Clear();
        }

        personagem.dadosDasHabilidadesPassivas.Remove(this);
    }

    private IEnumerator AplicarBuffTemporario(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        float danoBase = personagem.personagem.arma.dano;

        float bonusDano = danoBase * multiplicadorBonusAtaque;

        personagem._dano += bonusDano;
        personagem.defesa += bonusDefesas;
        personagem.defesaMagica += bonusDefesas;

        dados.bonusAplicados.Add(bonusDano);
        dados.bonusAplicados.Add(bonusDefesas);

        yield return new WaitForSeconds(tempoDeEfeito);

        personagem._dano -= bonusDano;
        personagem.defesa -= bonusDefesas;
        personagem.defesaMagica -= bonusDefesas;

        int count = dados.bonusAplicados.Count;
        if (count >= 3)
        {
            dados.bonusAplicados.RemoveRange(count - 3, 3);
        }
    }

    IEnumerator AplicarEscudoTemporario(IAPersonagemBase personagem)
    {
        yield return new WaitForSeconds(tempoDeEfeito);
        personagem.escudoAtivado = false;
        personagem.valorEscudo = 0;
        if (personagem.escudoVfx != null)
        {
            personagem.escudoVfx.SetActive(false);
        }
    }
}
