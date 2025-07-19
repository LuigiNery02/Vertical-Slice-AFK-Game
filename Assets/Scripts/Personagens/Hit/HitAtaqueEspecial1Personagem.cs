using UnityEngine;

public class HitAtaqueEspecial1Personagem : MonoBehaviour
{
    [HideInInspector]
    public IAPersonagemBase _personagemPai; //personagem que criou este ataque
    [HideInInspector]
    public float dano;
    [HideInInspector]
    public float velocidadeDeCrescimento;
    [HideInInspector]
    public float escalaMaxima;
    [HideInInspector]
    public float duracao;
    [HideInInspector] 
    public float velocidadeDeMovimento;
    [HideInInspector] 
    public Vector3 direcaoDeMovimento;

    private float tempoDecorrido = 0;

    private void OnTriggerEnter(Collider other) //quando colidir com um objeto
    {
        if (other.GetComponent<IAPersonagemBase>() != null) //se colidiu com um personagem
        {
            IAPersonagemBase alvo = other.GetComponent<IAPersonagemBase>(); //define o personagem colidido como alvo

            if (other != _personagemPai && alvo._comportamento != EstadoDoPersonagem.MORTO && alvo == _personagemPai._personagemAlvo)
            {
                //define para o personagem que este ataque colidiu com um personagem
                IAPersonagemBase alvoDoDano = other.GetComponent<IAPersonagemBase>();

                //verifica o tipo de dano que causará (físico ou mágico)
                switch (_personagemPai.personagem.arma.armaDano)
                {
                    case TipoDeDano.DANO_MELEE:
                        alvoDoDano.SofrerDano(dano, false, _personagemPai);
                        break;
                    case TipoDeDano.DANO_RANGED:
                        alvoDoDano.SofrerDano(dano, false, _personagemPai);
                        break;
                    case TipoDeDano.DANO_MAGICO:
                        alvoDoDano.SofrerDano(dano, false, _personagemPai);
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        tempoDecorrido += Time.deltaTime;

        if (transform.localScale.x < escalaMaxima)
        {
            float novaEscala = transform.localScale.x + velocidadeDeCrescimento * Time.deltaTime;
            novaEscala = Mathf.Min(novaEscala, escalaMaxima);

            transform.localScale = new Vector3(novaEscala, transform.localScale.y, novaEscala);
        }

        transform.position += direcaoDeMovimento * velocidadeDeMovimento * Time.deltaTime;

        if (tempoDecorrido >= duracao)
        {
            Destroy(gameObject);
        }
    }

}
