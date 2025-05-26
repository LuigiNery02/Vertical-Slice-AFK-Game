using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Classe { Guerreiro, Arqueiro, Mago }
public enum PreferenciaAtributo{ Forca, Agilidade, Destreza, Constituicao, Inteligencia, Sabedoria }

[System.Serializable]
public class PersonagemData
{
    public Classe classe; //classe do personagem
    public ArmaBase arma; //arma do personagem
    public List<PreferenciaAtributo> atributosDePreferencia = new List<PreferenciaAtributo>(); //atributos de preferência do personagem
    public string codigoID; //ID de código único de cada personagem
    public string apelido; //apelido do personagem
    public int nivel; //nível do personagem
    public int forca; //força do personagem
    public int agilidade; //agilidade do personagem
    public int destreza; //destreza do personagem
    public int constituicao; //constituição do personagem
    public int inteligencia; //inteligência do personagem
    public int sabedoria; //sabedoria do personagem
    public float expAtual; //expriência atual do personagem
    public float expProximoNível; //experiência necessária para passar para o próximo nível 

    private List<PreferenciaAtributo> listaSortearAtributo = new List<PreferenciaAtributo>(); //lista que define em pesos todos os pesos dos atributos do personagem

    public void DefinirPersonagem() //função que define dados importantes do personagem
    {
        #region Pesos Atributos
        //adiciona na lista atributos com peso 8
        foreach (var atributo in atributosDePreferencia)
        {
            for(int i = 0; i < 8; i++)
            {
                listaSortearAtributo.Add(atributo);
            }
        }

        //adiciona na lista atributos com peso 2
        foreach (PreferenciaAtributo atributo in Enum.GetValues(typeof(PreferenciaAtributo)))
        {
            if (!atributosDePreferencia.Contains(atributo))
            {
                for(int i = 0; i < 2; i++)
                {
                    listaSortearAtributo.Add(atributo);
                }

            }
        }
        #endregion
    }

    public void GanharEXP(float exp) //função que faz com que o personagem ganhe experiência para subir de nível
    {
        expAtual += exp; //recebe exp e acrescenta ao exp atual

        //deve subir de nível caso o exp atual seja igual ou maior ao valor necessário para subir de nível
        if(expAtual >= expProximoNível)
        {
            expAtual = 0;
            SubirDeNivel();
        }
    }

    public void SubirDeNivel() //função que sobe o nível do personagem
    {
        nivel++;

        //define o nível máximo como 99
        if(nivel >= 99)
        {
            nivel = 99;
        }
        else
        {
            expProximoNível += (expProximoNível / 10); //atualiza o valor necessário para passar de nível
        }
    }

    public void EscolherAtributo() //função que sorteia um dos atributos do personagem para melhorar
    {
        if(nivel < 99)
        {
            //sorteia uma atributo da lista temporária
            PreferenciaAtributo atributoEscolhido = listaSortearAtributo[UnityEngine.Random.Range(0, listaSortearAtributo.Count)];

            //aumenta o atributo correspondente
            switch (atributoEscolhido)
            {
                case PreferenciaAtributo.Forca:
                    forca++;
                    break;
                case PreferenciaAtributo.Agilidade:
                    agilidade++;
                    break;
                case PreferenciaAtributo.Destreza:
                    destreza++;
                    break;
                case PreferenciaAtributo.Constituicao:
                    constituicao++;
                    break;
                case PreferenciaAtributo.Inteligencia:
                    inteligencia++;
                    break;
                case PreferenciaAtributo.Sabedoria:
                    sabedoria++;
                    break;
            }
        }
    }
}
