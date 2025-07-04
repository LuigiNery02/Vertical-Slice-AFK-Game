using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Classe { Guerreiro, Ladino, Elementalista, Sacerdote }
public enum PreferenciaAtributo{ Forca, Agilidade, Destreza, Constituicao, Inteligencia, Sabedoria }

[System.Serializable]
public class PersonagemData
{
    #region Definições Personagem
    [Header("Definições Personagem")]
    public Classe classe; //classe do personagem
    public ArmaBase arma; //arma do personagem
    [HideInInspector]
    public int armaID; //id da arma
    public List<PreferenciaAtributo> atributosDePreferencia = new List<PreferenciaAtributo>(); //atributos de preferência do personagem
    [HideInInspector]
    public List<PreferenciaAtributo> listaSortearAtributo = new List<PreferenciaAtributo>(); //lista que define em pesos todos os pesos dos atributos do personagem
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
    public float hpBase;
    public float hp;
    public float hpRegeneracao;
    public float spBase;
    public float sp;
    public float spRegeneracao;
    public float fatorClasse;
    public float danoBase;
    public float dano;
    public float velocidadeDeAtaque;
    public float precisaoBase;
    public float precisao;
    public float chanceCritico;
    public float multiplicadorCritico;
    public float defesa;
    public float defesaMagica;
    public float esquivaBase;
    public float esquiva;
    public float velocidadeDeMovimento;
    public float ranged;
    public float multiplicadorAtaque = 1;
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
    [HideInInspector]
    public List<DadosHabilidade> habilidadesDeClasseSalvas = new List<DadosHabilidade>(); //dados das habilidades de classe salvas
    [HideInInspector]
    public List<DadosHabilidade> habilidadesDeArmaSalvas = new List<DadosHabilidade>(); //dados das habilidades de arma salvas
    [HideInInspector]
    public string habilidadeClasseID; //id da habilidade de classe equipada
    [HideInInspector]
    public string habilidadeArmaID; //id da habilidade de arma equipada
    #endregion

    #region Equipamento
    public EquipamentoBase equipamentoCabecaAcessorio; //equipamento da cabeça acessório do personagem
    public EquipamentoBase equipamentoCabecaTopo; //equipamento da cabeça topo do personagem
    public EquipamentoBase equipamentoCabecaMedio; //equipamento da cabeça médio do personagem
    public EquipamentoBase equipamentoCabecaBaixo; //equipamento da cabeça baixo do personagem
    public EquipamentoBase equipamentoArmadura; //equipamento da armadura do personagem
    public EquipamentoBase equipamentoBracadeira; //equipamento da braçadeira do personagem
    public EquipamentoBase equipamentoMaoEsquerda; //equipamento da mão esquerda do personagem
    public EquipamentoBase equipamentoMaoDireita; //equipamento da mão direita do personagem
    public EquipamentoBase equipamentoBota; //equipamento da bota do personagem
    public EquipamentoBase equipamentoAcessorio1; //equipamento do acessório1 do personagem
    public EquipamentoBase equipamentoAcessorio2; //equipamento do acessório2 do personagem
    public EquipamentoBase equipamentoBuffConsumivel; //equipamento do buff de consumível do personagem

    [HideInInspector]
    public List<string> idsEquipamentosEquipados = new List<string>(); //lista com os IDs dos equipamentos equipados ao personagem
    #endregion

    //função de subir nível do personagem
    public delegate void delegateSubirNivel();
    public delegateSubirNivel funcaoSubirNivel;
    public void DefinirPersonagem(PersonagemAtributosIniciais atributosIniciais) //função que define dados iniciais importantes do personagem
    {
        switch (classe)
        {
            case Classe.Guerreiro:
                hpBase = atributosIniciais.hpBaseGuerreiro;
                spBase = atributosIniciais.spBaseGuerreiro;
                fatorClasse = atributosIniciais.fatorClasseGuerreiro;
                break;
            case Classe.Ladino:
                hpBase = atributosIniciais.hpBaseLadino;
                spBase = atributosIniciais.spBaseLadino;
                fatorClasse = atributosIniciais.fatorClasseLadino;
                break;
            case Classe.Elementalista:
                hpBase = atributosIniciais.hpBaseElementalista;
                spBase = atributosIniciais.spBaseElementalista;
                fatorClasse = atributosIniciais.fatorClasseElementalista;
                break;
            case Classe.Sacerdote:
                hpBase = atributosIniciais.hpBaseSacerdote;
                spBase = atributosIniciais.spBaseSacerdote;
                fatorClasse = atributosIniciais.fatorClasseSacerdote;
                break;
        }

        danoBase = arma.dano;
        multiplicadorCritico = atributosIniciais.multiplicadorCritico;
        precisaoBase = atributosIniciais.precisaoBase;
        ranged = atributosIniciais.rangedBase;

        esquivaBase = atributosIniciais.esquivaBase;

        velocidadeDeMovimento = atributosIniciais.velocidadeDeMovimentoBase;

        DefinicoesAtributos();
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
            EscolherAtributo();
            SubirDeNivel();
        }
    }

    public void SubirDeNivel() //função que sobe o nível do personagem
    {
        if(nivel < 99)
        {
            nivel++;

            switch (nivel)
            {
                case 8:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Classe, this.classe, 1, this);
                    break;
                case 18:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Arma, this.classe, 1, this);
                    break;
                case 28:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Classe, this.classe, 1, this);
                    break;
                case 48:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Arma, this.classe, 1, this);
                    break;
                case 60:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Classe, this.classe, 1, this);
                    break;
                case 80:
                    GerenciadorDeInventario.instancia.SortearHabilidade(TipoDeHabilidade.Arma, this.classe, 1, this);
                    break;
                default:
                    GerenciadorDeInventario.instancia.MostrarMensagem("Herói " + apelido + " subiu para o nível " + nivel);
                    break;
            }
            expProximoNível += (expProximoNível / 10); //atualiza o valor necessário para passar de nível
            if (funcaoSubirNivel != null)
            {
                funcaoSubirNivel();
            }

            DefinicoesBatalha();
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
                    if(forca < 50)
                    {
                        forca++;
                    }
                    else
                    {
                        EscolherAtributo();
                    }
                    break;
                case PreferenciaAtributo.Agilidade:
                    if (agilidade < 50)
                    {
                        agilidade++;
                    }
                    else
                    {
                        EscolherAtributo();
                    }
                    break;
                case PreferenciaAtributo.Destreza:
                    if (destreza < 50)
                    {
                        destreza++;
                    }
                    else
                    {
                        EscolherAtributo();
                    }
                    break;
                case PreferenciaAtributo.Constituicao:
                    if (constituicao < 50)
                    {
                        constituicao++;
                    }
                    else
                    {
                        EscolherAtributo();
                    }
                    break;
                case PreferenciaAtributo.Inteligencia:
                    if (inteligencia < 50)
                    {
                        inteligencia++;
                    }
                    else
                    {
                        EscolherAtributo();
                    }
                    break;
                case PreferenciaAtributo.Sabedoria:
                    if (sabedoria < 50)
                    {
                        sabedoria++;
                    }
                    else
                    {
                        EscolherAtributo();
                    }
                    break;
            }
        }
    }
    #endregion

    public void DefinicoesAtributos() //função que define a lista de atributos com mais chance de serem escolhidos
    {
        //adiciona na lista atributos com peso 8 (80%)
        foreach (var atributo in atributosDePreferencia)
        {
            for (int i = 0; i < 8; i++)
            {
                listaSortearAtributo.Add(atributo);
            }
        }

        //adiciona na lista atributos com peso 2 (20%)
        foreach (PreferenciaAtributo atributo in Enum.GetValues(typeof(PreferenciaAtributo)))
        {
            if (!atributosDePreferencia.Contains(atributo))
            {
                for (int i = 0; i < 2; i++)
                {
                    listaSortearAtributo.Add(atributo);
                }
            }
        }
    }

    public void DefinicoesBatalha() //função que define os atributos de batalha do personagem
    {
        int STR = forca; //atributo força do personagem
        int AGI = agilidade; //atributo agilidade do personagem
        int DEX = destreza; //atributo destreza do personagem
        int INT = inteligencia; //atributo inteligência do personagem
        int VIT = constituicao; //atributo constituição do personagem
        int LUK = sabedoria; //atributo sabedoria/sorte do personagem
        int LVL = nivel; //nível do personagem

        //bônus a cada 10 pontos
        int bonusSTR = Mathf.FloorToInt(STR / 10f);
        int bonusAGI = Mathf.FloorToInt(AGI / 10f);
        int bonusDEX = Mathf.FloorToInt(DEX / 10f);
        int bonusVIT = Mathf.FloorToInt(VIT / 10f);
        int bonusINT = Mathf.FloorToInt(INT / 10f);
        int bonusLUK = Mathf.FloorToInt(LUK / 10f);

        switch (classe)
        {
            case Classe.Guerreiro:
                hp = (((hpBase + VIT + STR + LVL) * 20) * (fatorClasse * 4));
                sp = ((spBase + (INT * 10) + (DEX * 3)) * LVL * (fatorClasse / 4));
                break;
            case Classe.Ladino:
                hp = (((hpBase + VIT + STR + LVL) * 20) * (fatorClasse * 3));
                sp = ((spBase + (INT * 10) + (DEX * 3)) * LVL * (fatorClasse / 2));
                break;
            case Classe.Elementalista:
                hp = (((hpBase + VIT + STR + LVL) * 20) * (fatorClasse * 2));
                sp = ((spBase + (INT * 10) + (DEX * 3)) * LVL * (fatorClasse / 2));
                break;
            case Classe.Sacerdote:
                hp = (((hpBase + VIT + STR + LVL) * 20) * (fatorClasse * 2));
                sp = ((spBase + (INT * 10) + (DEX * 3)) * LVL * (fatorClasse / 1));
                break;
        }
        hp += (hp * 0.01f * bonusVIT); //efeito de bônus do atributo constituição

        hpRegeneracao = (VIT * 0.5f);
        spRegeneracao = (INT * 0.5f);

        multiplicadorAtaque = 1;
        switch (arma.armaDano)
        {
            case TipoDeDano.DANO_MELEE:
                dano = (danoBase + (STR * 3) + (DEX * 1) * multiplicadorAtaque);
                dano += (bonusSTR * 6); //efeito de bônus do atributo força
                break;
            case TipoDeDano.DANO_RANGED:
                dano = (danoBase + (DEX * 3) + (STR * 1) * multiplicadorAtaque);
                dano += (bonusDEX * 3); //efeito de bônus do atributo destreza
                break;
            case TipoDeDano.DANO_MAGICO:
                dano = danoBase + ((INT * 4) + (DEX * 1) * multiplicadorAtaque );
                dano += (bonusINT * 4); //efeito de bônus do atributo inteligência
                break;
        }

        precisao = (precisaoBase + (DEX * 2) + (LUK * 0.5f));
        precisao += (bonusDEX * 10); //efeito de bônus do atributo destreza

        float aspd = arma.velocidadeDeAtaque + (AGI * 0.3f) + (DEX * 0.1f);
        aspd += (bonusAGI * 0.3f); //efeito de bônus do atributo agilidade
        velocidadeDeAtaque = Mathf.Clamp(2f - (aspd * 0.02f), 0.2f, 2f);

        esquiva = (esquivaBase + (AGI * 2) + (LUK * 0.5f));
        esquiva += (bonusAGI * 10); //efeito de bônus do atributo agilidade

        defesa = (VIT * 0.5f);
        defesa += defesa * (bonusVIT * 0.01f); //efeito de bônus do atributo constituição
        defesaMagica = (INT * 0.5f);
        defesaMagica += defesaMagica * (bonusVIT * 0.01f); //efeito de bônus do atributo constituição

        chanceCritico = (LUK * 0.5f);
        chanceCritico += (bonusLUK * 2); //efeito de bônus do atributo sabedoria

        for (int i = 1; i < 13; i++)
        {
            AplicarEfeitoEquipamento(i);
        }
    }

    public void AplicarEfeitoEquipamento(int indice) //função que aplica os efeitos do equipamento ao personagem
    {
        switch (indice)
        {
            case 1:
                if(equipamentoCabecaAcessorio != null)
                {
                    equipamentoCabecaAcessorio.AplicarEfeito();
                }
                break;
            case 2:
                if(equipamentoCabecaTopo != null)
                {
                    equipamentoCabecaTopo.AplicarEfeito();
                }  
                break;
            case 3:
                if(equipamentoCabecaMedio != null)
                {
                    equipamentoCabecaMedio.AplicarEfeito();
                }
                break;
            case 4:
                if(equipamentoCabecaBaixo != null)
                {
                    equipamentoCabecaBaixo.AplicarEfeito();
                } 
                break;
            case 5:
                if(equipamentoArmadura != null)
                {
                    equipamentoArmadura.AplicarEfeito();
                }
                break;
            case 6:
                if(equipamentoBracadeira != null)
                {
                    equipamentoBracadeira.AplicarEfeito();
                } 
                break;
            case 7:
                if(equipamentoMaoEsquerda != null)
                {
                    equipamentoMaoEsquerda.AplicarEfeito();
                }
                break;
            case 8:
                if(equipamentoMaoDireita != null)
                {
                    equipamentoMaoDireita.AplicarEfeito();
                }
                break;
            case 9:
                if(equipamentoBota != null)
                {
                    equipamentoBota.AplicarEfeito();
                }
                break;
            case 10:
                if(equipamentoAcessorio1 != null)
                {
                    equipamentoAcessorio1.AplicarEfeito();
                } 
                break;
            case 11:
                if(equipamentoAcessorio2 != null)
                {
                    equipamentoAcessorio2.AplicarEfeito();
                } 
                break;
            case 12:
                if(equipamentoBuffConsumivel != null)
                {
                    equipamentoBuffConsumivel.AplicarEfeito();
                }
                break;
        }
    }
}
