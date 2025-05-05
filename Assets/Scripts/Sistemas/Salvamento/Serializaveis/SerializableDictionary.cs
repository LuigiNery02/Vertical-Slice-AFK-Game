using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<TKey> chaves = new List<TKey>(); //chaves da lista

    [SerializeField]
    private List<TValue> valores = new List<TValue>(); //valores das chaves da lista
    public void OnAfterDeserialize()
    {
        this.Clear();

        for(int i = 0; i < chaves.Count; i++)
        {
            this.Add(chaves[i], valores[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        chaves.Clear();
        valores.Clear();
        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            chaves.Add(pair.Key);
            valores.Add(pair.Value);
        }
    }
}
