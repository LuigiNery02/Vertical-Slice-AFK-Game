using UnityEngine;

public class HitAtaqueEspecial3Personagem : MonoBehaviour
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
    public string elemento;
    [HideInInspector]
    public GameObject vfxImpacto;

    private void OnTriggerEnter(Collider other) //quando colidir com um objeto
    {
        if (other.GetComponent<IAPersonagemBase>() != null) //se colidiu com um personagem
        {
            IAPersonagemBase alvo = other.GetComponent<IAPersonagemBase>(); //define o personagem colidido como alvo

            if (alvo != _personagemPai && alvo._comportamento != EstadoDoPersonagem.MORTO && alvo.controlador != _personagemPai.controlador)
            {
                alvo.SofrerDano(dano, false, _personagemPai);

                int cargaGelo = _personagemPai.cargasDeGelo;
                int cargaFogo = _personagemPai.cargasDeFogo;
                int cargaRaio = _personagemPai.cargasDeRaio;
                float danoBase = _personagemPai._dano;

                if (cargaGelo != 0 || cargaFogo != 0 || cargaRaio != 0)
                {
                    alvo.CausarEfeitoCargasElementais(cargaGelo, cargaFogo, cargaRaio, danoBase);
                }
                _personagemPai.AtualizarCargasElementais(elemento);

                GameObject vfxInstanciado = Instantiate(vfxImpacto, other.transform.position, Quaternion.identity, other.transform);
                Destroy(vfxInstanciado, 2f);

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
    }
}
