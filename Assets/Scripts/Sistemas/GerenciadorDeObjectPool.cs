using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TipoProjetil
{
    public string chave; //nome (chave) do pool
    public GameObject prefab; //prefab do pool
}

public class GerenciadorDeObjectPool : MonoBehaviour
{
    [SerializeField]
    private List<TipoProjetil> tiposDeProjeteis; // lista configur�vel no Inspector
    [SerializeField]
    private int quantidadeInicial = 10; //n�mero de objetos a serem inst�nciados na cena 

    private Dictionary<string, Queue<GameObject>> dicionarioDePools = new(); //pools
    private Dictionary<string, GameObject> prefabsOriginais = new(); //prefabs

    private void Awake()
    {
        //inst�ncia os prefabs na cena e os adiciona nos dicion�rios
        foreach (var tipo in tiposDeProjeteis)
        {
            Queue<GameObject> fila = new();
            for (int i = 0; i < quantidadeInicial; i++)
            {
                GameObject novo = Instantiate(tipo.prefab, transform);
                novo.SetActive(false);
                fila.Enqueue(novo);
            }

            dicionarioDePools.Add(tipo.chave, fila);
            prefabsOriginais.Add(tipo.chave, tipo.prefab);
        }
    }

    public GameObject ObterPool(string chave) //fun��o para um objeto obter o pool
    {
        if (!dicionarioDePools.ContainsKey(chave)) //verifica se o nome do objeto existe no dicion�rio
        {
            return null;
        }

        //verifica se h� objetos dispon�veis, do contr�rio ir� instanciar outro

        Queue<GameObject> fila = dicionarioDePools[chave];

        if (fila.Count == 0)
        {
            GameObject novo = Instantiate(prefabsOriginais[chave], transform);
            novo.SetActive(false);
            fila.Enqueue(novo);
        }

        GameObject go = fila.Dequeue();
        go.SetActive(true);
        return go;
    }

    public void DevolverPool(string chave, GameObject go) //fun��o para devolver o pool para o dicion�rio e o desativar
    {
        go.SetActive(false); 
        dicionarioDePools[chave].Enqueue(go);
    }

    public GameObject ObterPrefab(string chave) //fun��o para um objeto obter o prefab
    {
        if (!prefabsOriginais.ContainsKey(chave))
        {
            return null;
        }
        return prefabsOriginais[chave];
    }
}
