using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelecaoDePersonagem : MonoBehaviour
{
    //�rea referente a UI
    [Header("UI")]
    [SerializeField]
    private GameObject _fun��es; //tela das fu���es dos personagens
    [SerializeField]
    private Image _imagemHabilidade1Fun��es; //imagem da habilidade 1 na "tela" de fun��es
    [SerializeField]
    private Image _imagemHabilidade2Fun��es; //imagem da habilidade 2 na "tela" de fun��es
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
    private Text _textoTituloHabilidade1; //texto do t�tulo da primeira habilidade
    [SerializeField]
    private Text _textoTituloHabilidade2; //texto do t�tulo da primeira habilidade
    [SerializeField]
    private Text _textoDescricaoHabilidade1; //texto da descri��o da primeira habilidade
    [SerializeField]
    private Text _textoDescricaoHabilidade2; //texto da descri��o da segunda habilidade

    //�rea referente � sprites
    [Header("Sprites")]
    [SerializeField]
    private Sprite[] _spritePersonagem; //sprite do personagem
    [SerializeField]
    private Sprite _spriteVazia; //sprite que representa um slot vazio

    private IAPersonagemBase _personagemSelecionado; //vari�vel que representa o personagem selecionado
    private SistemaDeBatalha _sistemaDeBatalha; //sistema de batalha
    private string _apelido; //vari�vel que representa o apelido do personagem selecionado
    private int _nivel; //vari�vel que representa o n�vel do personagem selecionado
    private string _classe; //vari�vel que representa a classe do personagem selecionado
    private string _armaNome; //vari�vel que representa o nome da arma do personagem selecionado
    private int _id; //vari�vel que representa o ID do personagem selecionado
    private string _tituloHabilidade1; //vari�vel que representa o t�tulo da primeira habilidade do personagem;
    private string _tituloHabilidade2; //vari�vel que representa o t�tulo da segunda habilidade do personagem;
    private string _detalheHabilidade1; //vari�vel que representa os detalhes da primeira habilidade do personagem;
    private string _detalheHabilidade2; //vari�vel que representa os detalhes da segunda habilidade do personagem;

    private void Start()
    {
        _sistemaDeBatalha = FindFirstObjectByType<SistemaDeBatalha>();
    }

    public void SelecionarPersonagem(IAPersonagemBase personagem) //fun��o para identificar o personagem selecionado
    {
        _personagemSelecionado = personagem; //define o personagem selecionado

        //atualiza os dados com base no personagem selecionado
        _apelido = _personagemSelecionado.personagem.apelido;
        _nivel = _personagemSelecionado.personagem.nivel;
        _classe = _personagemSelecionado.personagem.classe.ToString();
        _armaNome = _personagemSelecionado.personagem.arma.nome;
        if(_personagemSelecionado.personagem.classe == Classe.Guerreiro)
        {
            _id = 0;
        }
        else if(_personagemSelecionado.personagem.classe == Classe.Ladino)
        {
            _id = 1;
        }
        else if(_personagemSelecionado.personagem.classe == Classe.Elementalista)
        {
            _id = 2;
        }
        else if (_personagemSelecionado.personagem.classe == Classe.Sacerdote)
        {
            _id = 3;
        }

        _tituloHabilidade1 = "";
        _detalheHabilidade1 = "";
        _imagemHabilidade1.sprite = _spriteVazia;

        _tituloHabilidade2 = "";
        _detalheHabilidade2 = "";
        _imagemHabilidade2.sprite = _spriteVazia;

        AtualizarSele��o();
    }

    public void AtualizarSele��o() //fun��o para atualizar a sele��o com os dados do personagem selecionado
    {
        if(_personagemSelecionado != null)
        {
            //atualiza visualmente os dados do personagem selecionado
            _textos[0].text = "Apelido: " + _apelido;
            _textos[1].text = "N�vel: " + _nivel;
            _textos[2].text = "Classe: " + _classe;
            _textos[3].text = "Arma: " + _armaNome;

            //atualiza visualmente as imagens das habilidades do personagem selecionado
            if(_personagemSelecionado.habilidade1 != null)
            {
                _imagemHabilidade1.sprite = _personagemSelecionado.habilidade1.spriteHabilidade;
                _tituloHabilidade1 = _personagemSelecionado.habilidade1.nome;
                _detalheHabilidade1 = _personagemSelecionado.habilidade1.descricao;
            }
            
            if(_personagemSelecionado.habilidade2 != null)
            {
                _imagemHabilidade2.sprite = _personagemSelecionado.habilidade2.spriteHabilidade;
                _tituloHabilidade2 = _personagemSelecionado.habilidade2.nome;
                _detalheHabilidade2 = _personagemSelecionado.habilidade2.descricao;
            }

            _textoTituloHabilidade1.text = _tituloHabilidade1;
            _textoDescricaoHabilidade1.text = _detalheHabilidade1;

            _textoTituloHabilidade2.text = _tituloHabilidade2;
            _textoDescricaoHabilidade2.text = _detalheHabilidade2;

            if (_personagemSelecionado.habilidade1 != null && _personagemSelecionado.habilidade1.podeAtivarEfeito)
            {
                _imagemHabilidade1Fun��es.color = Color.white;
            }
            else
            {
                _imagemHabilidade1Fun��es.color = Color.gray;
            }

            if (_personagemSelecionado.habilidade2 != null && _personagemSelecionado.habilidade2.podeAtivarEfeito)
            {
                _imagemHabilidade2Fun��es.color = Color.white;
            }
            else
            {
                _imagemHabilidade2Fun��es.color = Color.gray;
            }

            //atualiza visualmente as imagens referente ao personagem
            _imagemPersonagem.sprite = _spritePersonagem[_id];

            #region Equipamentos            
            if (_personagemSelecionado.personagem.equipamentoCabecaAcessorio != null)
            {
                _imagensEquipamentos[0].sprite = _personagemSelecionado.personagem.equipamentoCabecaAcessorio.icone;
            }
            else
            {
                _imagensEquipamentos[0].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoCabecaTopo != null)
            {
                _imagensEquipamentos[1].sprite = _personagemSelecionado.personagem.equipamentoCabecaTopo.icone;
            }
            else
            {
                _imagensEquipamentos[1].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoCabecaMedio != null)
            {
                _imagensEquipamentos[2].sprite = _personagemSelecionado.personagem.equipamentoCabecaMedio.icone;
            }
            else
            {
                _imagensEquipamentos[2].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoCabecaBaixo != null)
            {
                _imagensEquipamentos[3].sprite = _personagemSelecionado.personagem.equipamentoCabecaBaixo.icone;
            }
            else
            {
                _imagensEquipamentos[3].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoArmadura != null)
            {
                _imagensEquipamentos[4].sprite = _personagemSelecionado.personagem.equipamentoArmadura.icone;
            }
            else
            {
                _imagensEquipamentos[4].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoBracadeira != null)
            {
                _imagensEquipamentos[5].sprite = _personagemSelecionado.personagem.equipamentoBracadeira.icone;
            }
            else
            {
                _imagensEquipamentos[5].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoMaoEsquerda != null)
            {
                _imagensEquipamentos[6].sprite = _personagemSelecionado.personagem.equipamentoMaoEsquerda.icone;
            }
            else
            {
                _imagensEquipamentos[6].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoMaoDireita != null)
            {
                _imagensEquipamentos[7].sprite = _personagemSelecionado.personagem.equipamentoMaoDireita.icone;
            }
            else
            {
                _imagensEquipamentos[7].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoBota != null)
            {
                _imagensEquipamentos[8].sprite = _personagemSelecionado.personagem.equipamentoBota.icone;
            }
            else
            {
                _imagensEquipamentos[8].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoAcessorio1 != null)
            {
                _imagensEquipamentos[9].sprite = _personagemSelecionado.personagem.equipamentoAcessorio1.icone;
            }
            else
            {
                _imagensEquipamentos[9].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoAcessorio2 != null)
            {
                _imagensEquipamentos[10].sprite = _personagemSelecionado.personagem.equipamentoAcessorio2.icone;
            }
            else
            {
                _imagensEquipamentos[10].sprite = _spriteVazia;
            }

            if (_personagemSelecionado.personagem.equipamentoBuffConsumivel != null)
            {
                _imagensEquipamentos[11].sprite = _personagemSelecionado.personagem.equipamentoBuffConsumivel.icone;
            }
            else
            {
                _imagensEquipamentos[11].sprite = _spriteVazia;
            }
            #endregion

            _fun��es.SetActive(true); //ativa as fun��es do personagem 
        }
    }

    public void AtivarHabilidade1() //fun��o que ativa a habilidade 1 do personagem selecionado
    {
        if(_sistemaDeBatalha.batalhaIniciou)
        {
            if(_personagemSelecionado.habilidade1 != null)
            {
                _personagemSelecionado.habilidade1.AtivarEfeito();
            }
        }
    }

    public void AtivarHabilidade2() //fun��o que ativa a habilidade 2 do personagem selecionado
    {
        if (_sistemaDeBatalha.batalhaIniciou)
        {
            if(_personagemSelecionado.habilidade2 != null)
            {
                _personagemSelecionado.habilidade2.AtivarEfeito();
            }
        }  
    }

    public void DesativarFun��es() //desativa as fun��es 
    {
        _fun��es.SetActive(false);
    }
}
