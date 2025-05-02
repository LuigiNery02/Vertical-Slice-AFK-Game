using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class Habilidade4 : HabilidadesBase
{
    [Header("Escudos")]
    [SerializeField]
    private Transform _escudosPai; //pai que guarda os escudos dentro de si
    [SerializeField]
    private GameObject[] _escudos; //escudos que serão adicionados aos inimigos

    [Header("Atributos")]
    [SerializeField]
    private int _reducaoDeDano; //valor do efeito de redução de dano

    private IAPersonagemBase _personagemPai;
    private List<IAPersonagemBase> _personagensAliados = new List<IAPersonagemBase>(); //aliados do personagem
    private void Start()
    {
        efeitoHabilidade = EfeitoHabilidade4;
        removerEfeitoHabilidade = RemoverEfeitoHabilidade4;
        _personagemPai = GetComponent<IAPersonagemBase>();
    }
    private void EfeitoHabilidade4() //função de efeito da habilidade 4
    {
        _personagensAliados.Clear();

        IAPersonagemBase[] personagens = FindObjectsOfType<IAPersonagemBase>();

        foreach (IAPersonagemBase personagem in personagens)
        {
            if (personagem.controlador == _personagemPai.controlador) //verifica se é personagem aliado
            {
                _personagensAliados.Add(personagem);

                if (_escudos.Length >= _personagensAliados.Count)
                {
                    int index = _personagensAliados.Count - 1; //índice correto

                    _escudos[index].SetActive(true); //ativa o escudo
                    _escudos[index].transform.SetParent(personagem.transform); //faz o escudo seguir o personagem
                    _escudos[index].transform.localPosition = new Vector3(0f, 0.5909996f, 0f); //centraliza no personagem
                }
            }

            personagem.reducaoDeDano = _reducaoDeDano; //ativa a redução de dano
        }
    }

    private void RemoverEfeitoHabilidade4() //função de remover efeito da habilidade 4
    {
        for (int i = 0; i < _escudos.Length; i++)
        {
            if (_escudos[i] != null)
            {
                
                _escudos[i].transform.SetParent(_escudosPai); //volta o escudo para o "pai" original
                _escudos[i].transform.localPosition = Vector3.zero; //reseta a posição
                _escudos[i].SetActive(false); //desativa o escudo
            }
        }

        foreach (IAPersonagemBase personagem in _personagensAliados)
        {
            personagem.reducaoDeDano = 0; //remove a redução de dano de todos os personagens
        }

        _personagensAliados.Clear(); //limpa a lista para evitar efeitos acumulados
    }
}
