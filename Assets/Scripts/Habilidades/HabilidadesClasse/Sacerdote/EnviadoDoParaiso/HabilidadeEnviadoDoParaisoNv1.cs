using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Classe/Sacerdote/Enviado do Paraíso/Nv1")]
public class HabilidadeEnviadoDoParaisoNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorCuraEscudo = 0.1f;
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

            personagem.efeitoPorAliadoCuradoAtivado = true;

            personagem.AtivarEfeitoPorAliadoCurado("EnviadoDoParaisoNv1", (IAPersonagemBase aliado, float cura) =>
            {
                float valorEscudo = cura * multiplicadorCuraEscudo;

                aliado.valorEscudo += valorEscudo;
                aliado.escudoAtivado = true;

                if (aliado.escudoVfx == null)
                {
                    GameObject vfxInstanciado = GameObject.Instantiate(vfx, aliado.transform.position + Vector3.zero, aliado.transform.rotation, aliado.transform);
                    aliado.escudoVfx = vfxInstanciado;
                }
                else
                {
                    aliado.escudoVfx.SetActive(true);
                }

                if (!dados.alvosComBonus.Contains(aliado))
                {
                    dados.alvosComBonus.Add(aliado);
                }

                dados.bonusAplicado = true;
            });
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        if (personagem.dadosDasHabilidadesPassivas.TryGetValue(this, out var dados))
        {
            if (dados.bonusAplicado)
            {
                foreach (var aliado in GameObject.FindObjectsOfType<IAPersonagemBase>())
                {
                    if(aliado.controlador == personagem.controlador && aliado != personagem && dados.alvosComBonus.Contains(aliado))
                    {
                        aliado.valorEscudo = 0;
                        aliado.escudoAtivado = false;
                        if (aliado.escudoVfx != null)
                        {
                            aliado.escudoVfx.SetActive(false);
                        }
                    }
                }
            }

            personagem.dadosDasHabilidadesPassivas.Remove(this);
        }

        personagem.efeitoPorAliadoCuradoAtivado = false;
        personagem.RemoverEfeitoPorHabilidadeAliado("EnviadoDoParaisoNv1");
    }
}
