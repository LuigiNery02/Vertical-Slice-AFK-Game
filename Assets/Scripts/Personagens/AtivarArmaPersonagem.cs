using UnityEngine;

public class AtivarArmaPersonagem : MonoBehaviour
{
    public Transform maoBone; //osso da m�o do personagem que vai segurar a arma
    public Transform livroBoneEspecial; //osso especial para o livro
    public Transform armasPai; //objeto pai que mant�m as armas como filhos
    public GameObject[] armas; //armas dispon�veis que o personagem pode segurar
    [HideInInspector]
    public int armaAtual; //vari�vel para verificar a arma atual por ID

    private void OnDisable()
    {
        DesativarArma();
    }
    public void AtivarArma(int arma) //fun��o que ativa a arma espec�fica do personagem e � ajusta na m�o do personagem
    {
        armas[arma].SetActive(true);
        armaAtual = arma;

        //define o osso pai da arma
        if (arma != 2)
        {
            armas[arma].transform.SetParent(maoBone);
        }
        else
        {
            armas[arma].transform.SetParent(livroBoneEspecial);
        }
    }

    public void DesativarArma() //desativa as armas do personagem
    {
        for(int i = 0; i < armas.Length; i++)
        {
            armas[i].transform.SetParent(armasPai);
            armas[i].gameObject.SetActive(false);
        }
    }
}
