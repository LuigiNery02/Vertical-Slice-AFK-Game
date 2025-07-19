using UnityEngine;

public class HitAtaqueEspecial2Personagem : MonoBehaviour
{
    [HideInInspector]
    public IAPersonagemBase _personagemPai; //personagem que criou este ataque
    [HideInInspector]
    public float dano;
    [HideInInspector]
    public float velocidadeDeMovimento;
    [HideInInspector]
    public Transform _alvo;
    [HideInInspector]
    public int valorMarcadores;
    [HideInInspector]
    public TrailRenderer _trailRenderer; //trilha da movimentação do hit

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

                alvoDoDano.AtualizarMarcadoresDeAlvo(valorMarcadores, true);

                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_alvo != null)
        {
            //move o ataque com base na posição do alvo
            Vector3 posicaoAtual = transform.position;
            Vector3 posicaoAlvo = _alvo.position;

            //mantém o mesmo y da posição atual
            posicaoAlvo.y = posicaoAtual.y;

            transform.LookAt(posicaoAlvo);

            //crrige a rotação X para 90 graus
            Vector3 rotacaoCorrigida = transform.eulerAngles;
            rotacaoCorrigida.x = 90f;
            transform.eulerAngles = rotacaoCorrigida;

            //move apenas em X e Z (altura fica fixa)
            transform.position = Vector3.MoveTowards(posicaoAtual, posicaoAlvo, velocidadeDeMovimento * Time.deltaTime);
        }
    }

    public void MoverAteAlvo(Transform alvo, float velocidade) //função que recebe variáveis necessárias para que o hit se mova
    {
        if (alvo == null)
        {
            return; //não se move caso o alvo não esteja mais acessível ou nulo
        }

        _alvo = alvo;
        velocidadeDeMovimento = velocidade;

        //ativa a trilha 
        _trailRenderer = GetComponent<TrailRenderer>();
        if (_trailRenderer != null)
        {
            _trailRenderer.enabled = true;
        }
    }
}
