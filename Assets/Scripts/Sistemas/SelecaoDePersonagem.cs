using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecaoDePersonagem : MonoBehaviour
{
    //área referente a UI
    [Header("UI")]
    [SerializeField]
    private GameObject _funções; //tela das fuñções dos personagens
    [SerializeField]
    private Image _imagemHabilidade1Funções; //imagem da habilidade 1 na "tela" de funções
    [SerializeField]
    private Image _imagemHabilidade2Funções; //imagem da habilidade 2 na "tela" de funções
    [SerializeField]
    private Text[] _textos; //textos dos dados dos personagens
    [SerializeField]
    private Image _imagemPersonagem; //imagem do personagem
    [SerializeField]
    private Image _imagemHabilidade1; //imagem da habilidade 1 
    [SerializeField]
    private Image _imagemHabilidade2; //imagem da habilidade 2
    [SerializeField]
    private Image[] _imagensEquipamentos; //imagens dos equipamentos
    [SerializeField]
    private Text _textoTituloHabilidade1; //texto do título da primeira habilidade
    [SerializeField]
    private Text _textoTituloHabilidade2; //texto do título da primeira habilidade
    [SerializeField]
    private Text _textoDescricaoHabilidade1; //texto da descrição da primeira habilidade
    [SerializeField]
    private Text _textoDescricaoHabilidade2; //texto da descrição da segunda habilidade

    //área referente à sprites
    [Header("Sprites")]
    [SerializeField]
    private Sprite[] _spriteHabilidade1; //sprites da habilidade 1
    [SerializeField]
    private Sprite[] _spriteHabilidade2; //sprites da habilidade 2
    [SerializeField]
    private Sprite[] _spritePersonagem; //sprite do personagem
    [SerializeField]
    private Sprite _spriteVazia; //sprite que representa um slot vazio
    private Sprite[] _spriteEquipamento; //sprite do equipamento do personagem

    private IAPersonagemBase _personagemSelecionado; //variável que representa o personagem selecionado
    private float _hp; //variável que representa o hp do personagem selecionado
    private float _velocidade; //variável que representa a velocidade do personagem selecionado
    private float _dano; //variável que representa o dano de ataque do personagem selecionado
    private float _cooldown; //variável que representa o cooldown de ataque do personagem selecionado
    private int _id; //variável que representa o ID do personagem selecionado
    private int _numeroDeEquipamentos; //variável que representa o numero de equipamentos do personagem
    private int _slotsEquipamento = 3; //variável que representa o número de slots de equipamento
    private string _tituloHabilidade1; //variável que representa o título da primeira habilidade do personagem;
    private string _tituloHabilidade2; //variável que representa o título da segunda habilidade do personagem;
    private string _detalheHabilidade1; //variável que representa os detalhes da primeira habilidade do personagem;
    private string _detalheHabilidade2; //variável que representa os detalhes da segunda habilidade do personagem;

    public void SelecionarPersonagem(IAPersonagemBase personagem) //função para identificar o personagem selecionado
    {
        _personagemSelecionado = personagem; //define o personagem selecionado

        //atualiza os dados com base no personagem selecionado
        _hp = _personagemSelecionado._hpMaximoEInicial;
        _velocidade = _personagemSelecionado._velocidade;
        _dano = _personagemSelecionado._danoAtaqueBasico;
        _cooldown = _personagemSelecionado._cooldown;
        _id = _personagemSelecionado.id;
        _numeroDeEquipamentos = _personagemSelecionado.numeroDeEquipamentos;
        if(_personagemSelecionado.spriteEquipamentos != null)
        {
            //reseta as sprites
            _spriteEquipamento = new Sprite[_slotsEquipamento];
            for(int i =0; i < _slotsEquipamento; i++)
            {
                _spriteEquipamento[i] = _spriteVazia;
            }

            //adiciona as novas sprites
            for(int i = 0; i < _numeroDeEquipamentos; i++)
            {
                _spriteEquipamento[i] = _personagemSelecionado.spriteEquipamentos[i];
            }
        }
        _tituloHabilidade1 = _personagemSelecionado.habilidade1.tituloHabilidade;
        _tituloHabilidade2 = _personagemSelecionado.habilidade2.tituloHabilidade;
        _detalheHabilidade1 = _personagemSelecionado.habilidade1.descrição;
        _detalheHabilidade2 = _personagemSelecionado.habilidade2.descrição;
        AtualizarSeleção();
    }

    public void AtualizarSeleção() //função para atualizar a seleção com os dados do personagem selecionado
    {
        if(_personagemSelecionado != null)
        {
            //atualiza visualmente os dados do personagem selecionado
            _textos[0].text = "HP: " + _hp;
            _textos[1].text = "Velocidade: " + _velocidade;
            _textos[2].text = "Dano: " + _dano;
            _textos[3].text = "Cooldown: " + _cooldown;

            //atualiza visualmente as imagens das habilidades do personagem selecionado
            _imagemHabilidade1Funções.sprite = _spriteHabilidade1[_id];
            _imagemHabilidade2Funções.sprite = _spriteHabilidade2[_id];
            _imagemHabilidade1.sprite = _spriteHabilidade1[_id];
            _imagemHabilidade2.sprite = _spriteHabilidade2[_id];
            if (_personagemSelecionado.habilidade1.podeAtivarEfeito)
            {
                _imagemHabilidade1Funções.color = Color.white;
            }
            else
            {
                _imagemHabilidade1Funções.color = Color.gray;
            }

            if (_personagemSelecionado.habilidade2.podeAtivarEfeito)
            {
                _imagemHabilidade2Funções.color = Color.white;
            }
            else
            {
                _imagemHabilidade2Funções.color = Color.gray;
            }

            //atualiza visualmente as imagens referente ao personagem
            _imagemPersonagem.sprite = _spritePersonagem[_id];
            for (int i = 0; i < _slotsEquipamento; i++)
            {
                _imagensEquipamentos[i].sprite = _spriteEquipamento[i];
            }

            //atualiza os textos das habilidades
            _textoTituloHabilidade1.text = _tituloHabilidade1;
            _textoTituloHabilidade2.text = _tituloHabilidade2;
            _textoDescricaoHabilidade1.text = _detalheHabilidade1;
            _textoDescricaoHabilidade2.text = _detalheHabilidade2;

            _funções.SetActive(true); //ativa as funções do personagem 
        }
    }

    public void AtivarHabilidade1() //função que ativa a habilidade 1 do personagem selecionado
    {
        _personagemSelecionado.habilidade1.AtivarEfeito();
    }

    public void AtivarHabilidade2() //função que ativa a habilidade 2 do personagem selecionado
    {
        _personagemSelecionado.habilidade2.AtivarEfeito();
    }

    public void DesativarFunções() //desativa as funções 
    {
        _funções.SetActive(false);
    }
}
