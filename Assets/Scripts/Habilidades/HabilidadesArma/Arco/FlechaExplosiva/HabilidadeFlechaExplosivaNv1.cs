using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Habilidades/Passiva/Arma/Arco/Flecha Explosiva/Nv1")]
public class HabilidadeFlechaExplosivaNv1 : HabilidadePassiva
{
    [Header("Configurações Habilidade")]
    [SerializeField]
    private float multiplicadorDeDano = 1.2f;
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
            dados.contadorAtaquesBasicos = 0;

            float danoOriginal = personagem._dano;

            personagem.efeitoPorAtaqueAtivado = true;

            personagem.AtivarEfeitoPorAtaque("FlechaExplosivaNv1", (bool acerto) =>
            {
                if (acerto)
                {
                    dados.contadorAtaquesBasicos++;

                    if(dados.contadorAtaquesBasicos == 3)
                    {
                        if (personagem.vfxHabilidadePassivaArma == null)
                        {
                            GameObject vfxInstanciado = GameObject.Instantiate(vfx, personagem.transform.position + Vector3.zero, personagem.transform.rotation, personagem.transform);
                            personagem.vfxHabilidadePassivaArma = vfxInstanciado;
                        }
                        else
                        {
                            personagem.GerenciarVFXHabilidade(4, true);
                        }
                    }

                    if(dados.contadorAtaquesBasicos >= 4)
                    {
                        dados.contadorAtaquesBasicos = 0;
                        personagem._dano *= multiplicadorDeDano;
                        personagem.StartCoroutine(EsperarFrame(personagem, danoOriginal));
                        personagem.GerenciarVFXHabilidade(4, false);
                    }
                }
            });
        }
    }

    public override void RemoverEfeito(IAPersonagemBase personagem)
    {
        personagem.RemoverEfeitoPorAtaque("FlechaExplosivaNv1");
        personagem.GerenciarVFXHabilidade(4, false);
        personagem.dadosDasHabilidadesPassivas.Remove(this);
    }

    IEnumerator EsperarFrame(IAPersonagemBase personagem, float dano)
    {
        yield return null; //agurada um frame
        personagem._dano = dano;
    }
}
