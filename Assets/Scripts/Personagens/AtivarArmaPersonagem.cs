using UnityEngine;

public class AtivarArmaPersonagem : MonoBehaviour
{
    public Transform maoBone; //osso da mão do personagem que vai segurar a arma
    public Transform livroBoneEspecial; //osso especial para o livro
    public Transform armasPai; //objeto pai que mantém as armas como filhos
    public GameObject[] armas; //armas disponíveis que o personagem pode segurar
    [HideInInspector]
    public int armaAtual; //variável para verificar a arma atual por ID

    private void OnDisable()
    {
        DesativarArma();
    }
    public void AtivarArma(int arma) //função que ativa a arma específica do personagem e à ajusta na mão do personagem
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
