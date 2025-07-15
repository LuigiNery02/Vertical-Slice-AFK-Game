using System.Collections.Generic;
using UnityEngine;

public static class HabilidadesBuscador
{
    public static T BuscarHabilidadePorIDENivel<T>(List<T> listaNivel1, string id, int nivelDesejado) where T : HabilidadeBase
    {
        T atual = listaNivel1.Find(h => h.idHabilidade == id);

        if (atual == null)
        {
            Debug.LogWarning($"Habilidade com ID {id} não encontrada na lista.");
            return null;
        }

        int nivelAtual = 1;

        while (nivelAtual < nivelDesejado && atual.habilidadeProximoNivel != null)
        {
            atual = atual.habilidadeProximoNivel as T;
            nivelAtual++;
        }

        return atual;
    }
}
