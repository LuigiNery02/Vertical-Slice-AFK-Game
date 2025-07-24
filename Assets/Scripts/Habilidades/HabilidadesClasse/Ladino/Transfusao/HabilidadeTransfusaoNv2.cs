using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Ladino/Transfusão/Nv2")]
public class HabilidadeTransfusaoNv2 : HabilidadePassiva
{
    [Header("Configurações da Habilidade")]
    [SerializeField]
    private float multiplicadorEfeitosNegativos = 0.2f;

    public override void AtivarEfeito(IAPersonagemBase personagem)
    {
        if (base.ChecarRuna(personagem, nivel))
        {
            if (!personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
            {
                dados = new DadosHabilidadePassiva();
                personagem.dadosDasHabilidadesPassivas[this] = dados;
            }

            if (dados.monitoramento == null)
            {
                if (dados.alvosComBonus == null)
                {
                    dados.alvosComBonus = new HashSet<IAPersonagemBase>();
                }

                dados.monitoramento = personagem.StartCoroutine(MonitorarAlvos(personagem, dados));
            }
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            if (dados.monitoramento != null)
            {
                personagem.StopCoroutine(dados.monitoramento);
            }

            foreach (var alvo in dados.alvosComBonus)
            {
                alvo.multiplicadorEfeitosNegativos = 0f;
            }

            dados.alvosComBonus.Clear();
        }
    }

    private IEnumerator MonitorarAlvos(IAPersonagemBase personagem, DadosHabilidadePassiva dados)
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

            foreach (var inimigo in personagens)
            {
                if(inimigo.controlador != personagem.controlador)
                {
                    bool temEfeitoNegativo = inimigo.efeitoMarcadorDeAlvo == EfeitoMarcadorDeAlvo.EXPOSTO || inimigo.efeitoMarcadorDeAlvo == EfeitoMarcadorDeAlvo.MARCADO_PARA_EXECUCAO;

                    if (temEfeitoNegativo)
                    {
                        if (!dados.alvosComBonus.Contains(inimigo))
                        {
                            inimigo.multiplicadorEfeitosNegativos += multiplicadorEfeitosNegativos;
                            dados.alvosComBonus.Add(inimigo);
                        }
                    }
                    else
                    {
                        if (dados.alvosComBonus.Contains(inimigo))
                        {
                            inimigo.multiplicadorEfeitosNegativos -= multiplicadorEfeitosNegativos;
                            dados.alvosComBonus.Remove(inimigo);
                        }
                    }
                }
            }
            yield return wait;
        }
    }
}
