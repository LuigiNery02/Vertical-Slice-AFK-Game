using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Classe { Guerreiro, Arqueiro, Mago }
public enum PreferenciaAtributo{ Forca, Agilidade, Destreza, Constituicao, Inteligencia, Sabedoria }

[System.Serializable]
public class PersonagemData
{
    #region Definições Personagem
    [Header("Definições Personagem")]
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
    #endregion

    #region Definições Batalha
    [Header("Definições Batalha")]
    public float hp; //valor do hp (vida) máximo do personagem
    public float ataque; //valor do ataque do personagem
    public float ataqueMagico; //valor do ataque mágico do personagem
    public float ataqueDistancia; //valor do ataque à distância do personagem
    public float defesa; //valor defesa do personagem
    public float defesaMagica; //valor da defesa mágica do personagem
    public float velocidadeAtaque; //velocidade de ataque do personagem
    public float esquiva; //probabilidade de esquiva do personagem
    public float precisao; //precisão do personagem
    public float pontosDeHabilidade; //pontos de habilidades do personagem
    public float suporte; //valor do suporte do personagem
    #endregion

    #region Definições Habilidades
    [Header("Definições Habilidades")]
    public bool runaNivel1; //variável que representa se o personagem possuí uma runa nível 1 equipada
    public bool runaNivel2; //variável que representa se o personagem possuí uma runa nível 2 equipada
    public bool runaNivel3; //variável que representa se o personagem possuí uma runa nível 3 equipada
    public List<HabilidadeBase> listaDeHabilidadesDeClasse = new List<HabilidadeBase>(); //lista de habilidades de classe que o personagem possuí
    public List<HabilidadeBase> listaDeHabilidadesDeArma = new List<HabilidadeBase>(); //lista de habilidades de arma que o personagem possuí
    public HabilidadeBase habilidadeClasse; //habilidade de classe do personagem
    public HabilidadeBase habilidadeArma; //habilidade de arma do personagem
    //public HabilidadeBase habilidadeArma;
    #endregion
    private List<PreferenciaAtributo> listaSortearAtributo = new List<PreferenciaAtributo>(); //lista que define em pesos todos os pesos dos atributos do personagem

    public void DefinirPersonagem() //função que define dados iniciais importantes do personagem
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

        DefinicoesBatalha();
    }

    #region Level Up
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
            switch(nivel)
            {
                case 8:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Classe, this.classe, 1, this);
                    break;
                case 18:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Arma, this.classe, 1, this);
                    break;
                case 28:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Classe, this.classe, 2, this);
                    break;
                case 48:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Arma, this.classe, 2, this);
                    break;
                case 60:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Classe, this.classe, 3, this);
                    break;
                case 80:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Arma, this.classe, 3, this);
                    break;
            }
            expProximoNível += (expProximoNível / 10); //atualiza o valor necessário para passar de nível
        }

        DefinicoesBatalha();
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
    #endregion

    public void DefinicoesBatalha() //função que define os atributos de batalha do personagem
    {
        //restaura os padrões
        hp = 100;
        ataque = arma.dano;
        ataqueMagico = arma.danoMagico;
        ataqueDistancia = arma.danoDistancia;
        velocidadeAtaque = arma.velocidadeDeAtaque;
        esquiva = 1;
        precisao = 2;
        pontosDeHabilidade = 60;

        switch (classe)
        {
            case Classe.Guerreiro:
                defesa = 10;
                defesaMagica = 5;
                suporte = 5;
                break;
            case Classe.Arqueiro:
                defesa = 10;
                defesaMagica = 5;
                suporte = 10;
                break;
            case Classe.Mago:
                defesa = 5;
                defesaMagica = 10;
                suporte = 15;
                break;
        }

        //atualiza os valores
        if (constituicao != 1)
        {
            hp += constituicao;
            defesa += constituicao;
            defesaMagica += constituicao;
        }

        if (forca != 1)
        {
            ataque += (forca + arma.dano);
        }

        if (inteligencia != 1)
        {
            ataqueMagico += (inteligencia + arma.danoMagico);
            pontosDeHabilidade += inteligencia;
        }

        if (destreza != 1)
        {
            ataqueDistancia += (destreza + arma.danoDistancia);
            precisao += destreza;
        }

        if (agilidade != 1)
        {
            velocidadeAtaque = Mathf.Clamp(arma.velocidadeDeAtaque - (agilidade * 0.01f), 0.2f, arma.velocidadeDeAtaque);
            esquiva += agilidade;
        }

        if (sabedoria != 1)
        {
            suporte += sabedoria;
        }
    }
}
