using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDeCriacaoDePersonagens : MonoBehaviour, Salvamento
{
    [Header("Personagens")]
    public List<PersonagemData> personagensCriados = new List<PersonagemData>(); //lista de todos os personagens criados
    public PersonagemData personagemEmCriacao; //representa o personagem atual que est� sendo criado

    [Header("Armas")]
    public List<ArmaBase> armasGuerreiro = new List<ArmaBase>(); //lista de armas da classe "Guerreiro"
    public List<ArmaBase> armasArqueiro = new List<ArmaBase>(); //lista de armas da classe "Arqueiro"
    public List<ArmaBase> armasMago = new List<ArmaBase>(); //lista de armas da classe "Mago"

    #region Visual
    [Header("Tela Personagem")]
    [SerializeField]
    private GameObject _telaPersonagem; //tela do personagem espec�fico criado
    [SerializeField]
    private GameObject _telaPersonagens; //tela dos personagens criados 
    [SerializeField]
    private GameObject _telaSelecaoClasse; //tela de sele��o de classes
    [SerializeField]
    private GameObject _telaPreferenciaHatributo; //tela de preferencias de atributo do personagem
    [SerializeField]
    private InputField _apelido; //texto do apelido do personagem
    [SerializeField]
    private Text _codigoIDTexto; //texto do c�digoID do personagem
    [SerializeField]
    private Image _personagemImagem; //imagem do personagem criado
    [SerializeField]
    private Text _classeTexto; //texto da classe do personagem
    [SerializeField]
    private Text _armaTexto; //texto do nome da arma do personagem
    [SerializeField]
    private Text _nivelTexto; //texto do n�vel do personagem
    [SerializeField]
    private Text _forcaTexto; //texto da for�a do personagem
    [SerializeField]
    private Text _agilidadeTexto; //texto da agilidade do personagem
    [SerializeField]
    private Text _destrezaTexto; //texto da destreza do personagem
    [SerializeField]
    private Text _constituicaoTexto; //texto da constituicao do personagem
    [SerializeField]
    private Text _inteligenciaTexto; //texto da inteligencia do personagem
    [SerializeField]
    private Text _sabedoriaTexto; //texto da sabedoria do personagem

    [Header("Tela Arma")]
    [SerializeField]
    private Text _nomeArmaTexto; //texto do nome da arma
    [SerializeField]
    private Text _danoMeleeArmaTexto; //texto do dano melee da arma do personagem
    [SerializeField]
    private Text _danoDistanciaArmaTexto; //texto do dano � dist�ncia da arma do personagem
    [SerializeField]
    private Text _danoMagicoArmaTexto; //texto do dano m�gico da arma do personagem
    [SerializeField]
    private Text _velocidadeAtaqueArmaTexto; //texto da velocidade de ataque da arma do personagem

    [Header("Tela Habilidades")]
    [SerializeField]
    private Image[] _runasImagem; //imagem das runas
    [SerializeField]
    private Button _botaoHabilidadesClasse; //bot�o habilidades de classe
    [SerializeField]
    private GameObject _visualHabilidadeClasse; //visual da habilidade de classe equipada
    [SerializeField]
    private Image _habilidadeClasseImagem; //imagem da habilidade de classe equipada
    [SerializeField]
    private Text _habilidadeClasseTituloTexto; //nome da habilidade de classe equipada
    [SerializeField]
    private Text _habilidadeClasseDescricaoTexto; //descri��o da habilidade de classe equipada
    [SerializeField]
    private Text _habilidadeClasseTipoTexto; //tipo da habilidade de calsse equipada
    [SerializeField]
    private Button _botaoHabilidadesArma; //bot�o habilidades de arma
    [SerializeField]
    private GameObject _visualHabilidadeArma; //visual da habilidade de arma equipada
    [SerializeField]
    private Image _habilidadeArmaImagem; //imagem da habilidade de arma equipada
    [SerializeField]
    private Text _habilidadeArmaTituloTexto; //nome da habilidade de arma equipada
    [SerializeField]
    private Text _habilidadeArmaDescricaoTexto; //descri��o da habilidade de arma equipada
    [SerializeField]
    private Text _habilidadeArmaTipoTexto; //tipo da habilidade de arma equipada

    [Header("Tela Equipamento")]
    [SerializeField]
    private Image _personagemEquipamentoImagem; //imagem do personagem da tela de equipamentos
    [SerializeField]
    private Image[] _equipamentoImagem; //imagem do �cone do equipamento
    [SerializeField]
    private Text[] _equipamentoNomeTexto; //texto do nome do equipamento

    [Header("Tela Modificadores")]
    [SerializeField]
    private Text _hpTexto; //texto do hp do personagem
    [SerializeField]
    private Text _danoMeleeTexto; //texto do dano melee do personagem
    [SerializeField]
    private Text _danoDistanciaTexto; //texto do dano � dist�ncia do personagem
    [SerializeField]
    private Text _danoMagicoTexto; //texto do dano m�gico do personagem
    [SerializeField]
    private Text _defesaTexto; //texto da defesa m�gica do personagem
    [SerializeField]
    private Text _defesaMagicaTexto; //texto da defesa m�gica do personagem
    [SerializeField]
    private Text _velocidadeAtaqueTexto; //texto da velocidade de ataque do personagem
    [SerializeField]
    private Text _esquivaTexto; //texto da esquiva do personagem
    [SerializeField]
    private Text _precisaoTexto; //texto da precis�p do personagem
    [SerializeField]
    private Text _suporteTexto; //texto do suporte do personagem
    [SerializeField]
    private Text _pontosHabilidadeTexto; //texto de pontos de habilidade do personagem

    [Header("Sprites")]
    [SerializeField]
    private Sprite[] _personagensSprites; //sprites dos personagens
    [SerializeField]
    private Sprite[] _runasSprites; //sprites das runas
    [SerializeField]
    private Sprite[] _equipamentoSprites;
    
    [Header("Tela Sele��o de Armas")]
    [SerializeField]
    private GameObject[] _telasArmas; //telas de sele��o de armas baseadas na classe selecionada
    #endregion

    [HideInInspector]
    public int _imagemClasseAtual; //vari�vel que determina a imagem do jogador a depender de sua classe
    private HashSet<string> _codigosGerados = new HashSet<string>(); //lista de c�digos gerados
    [SerializeField]
    private GerenciadorDeSlots _gerenciadorDeSlots; //gerenciador de slots

    public void CarregarSave(GameData data) //fun��o de carregar os dados salvos do sistema de cria��o de personagens
    {
        personagensCriados = data.personagens;

        for (int i = 0; i < personagensCriados.Count; i++)
        {
            personagensCriados[i].listaDeHabilidadesDeClasse = new List<HabilidadeBase>();
            personagensCriados[i].listaDeHabilidadesDeArma = new List<HabilidadeBase>();

            if (personagensCriados[i].classe == Classe.Guerreiro)
            {
                personagensCriados[i].arma = armasGuerreiro[personagensCriados[i].armaID];

                foreach (string iDSalvo in personagensCriados[i].idsHabilidadesDeClasse)
                {
                    HabilidadeBase habilidade = GerenciadorDeInventario.instancia.habilidadesClasseGuerreiro.Find(h => h.idHabilidade == iDSalvo);
                    if (habilidade != null)
                    {
                        personagensCriados[i].listaDeHabilidadesDeClasse.Add(habilidade);
                    }
                }

                foreach (string iDSalvo in personagensCriados[i].idsHabilidadesDeArma)
                {
                    switch (personagensCriados[i].arma.nome)
                    {
                        case "Espada":
                            HabilidadeBase habilidadeEspada = GerenciadorDeInventario.instancia.habilidadesEspada.Find(h => h.idHabilidade == iDSalvo);
                            if (habilidadeEspada != null)
                            {
                                personagensCriados[i].listaDeHabilidadesDeArma.Add(habilidadeEspada);
                                personagensCriados[i].habilidadeArma = GerenciadorDeInventario.instancia.habilidadesEspada.Find(h => h.idHabilidade == personagensCriados[i].habilidadeArmaID);
                            }
                            break;
                        case "Lan�a":
                            HabilidadeBase habilidadeLanca = GerenciadorDeInventario.instancia.habilidadesLanca.Find(h => h.idHabilidade == iDSalvo);
                            if (habilidadeLanca != null)
                            {
                                personagensCriados[i].listaDeHabilidadesDeArma.Add(habilidadeLanca);
                                personagensCriados[i].habilidadeArma = GerenciadorDeInventario.instancia.habilidadesLanca.Find(h => h.idHabilidade == personagensCriados[i].habilidadeArmaID);
                            }
                            break;
                        case "Machado":
                            HabilidadeBase habilidadeMachado = GerenciadorDeInventario.instancia.habilidadesMachado.Find(h => h.idHabilidade == iDSalvo);
                            if (habilidadeMachado != null)
                            {
                                personagensCriados[i].listaDeHabilidadesDeArma.Add(habilidadeMachado);
                                personagensCriados[i].habilidadeArma = GerenciadorDeInventario.instancia.habilidadesMachado.Find(h => h.idHabilidade == personagensCriados[i].habilidadeArmaID);
                            }
                            break;
                    }
                }

                personagensCriados[i].habilidadeClasse = GerenciadorDeInventario.instancia.habilidadesClasseGuerreiro.Find(h => h.idHabilidade == personagensCriados[i].habilidadeClasseID);
            }
            else if (personagensCriados[i].classe == Classe.Arqueiro)
            {
                personagensCriados[i].arma = armasArqueiro[personagensCriados[i].armaID];

                foreach (string iDSalvo in personagensCriados[i].idsHabilidadesDeClasse)
                {
                    HabilidadeBase habilidade = GerenciadorDeInventario.instancia.habilidadesClasseArqueiro.Find(h => h.idHabilidade == iDSalvo);
                    if (habilidade != null)
                    {
                        personagensCriados[i].listaDeHabilidadesDeClasse.Add(habilidade);
                    }
                }

                personagensCriados[i].habilidadeClasse = GerenciadorDeInventario.instancia.habilidadesClasseArqueiro.Find(h => h.idHabilidade == personagensCriados[i].habilidadeClasseID);
            }
            else if (personagensCriados[i].classe == Classe.Mago)
            {
                personagensCriados[i].arma = armasMago[personagensCriados[i].armaID];

                foreach (string iDSalvo in personagensCriados[i].idsHabilidadesDeClasse)
                {
                    HabilidadeBase habilidade = GerenciadorDeInventario.instancia.habilidadesClasseMago.Find(h => h.idHabilidade == iDSalvo);
                    if (habilidade != null)
                    {
                        personagensCriados[i].listaDeHabilidadesDeClasse.Add(habilidade);
                    }
                }

                personagensCriados[i].habilidadeClasse = GerenciadorDeInventario.instancia.habilidadesClasseMago.Find(h => h.idHabilidade == personagensCriados[i].habilidadeClasseID);
            }
        }

        _gerenciadorDeSlots.AtualizarSlots();
    }

    public void SalvarSave(GameData data) //fun��o de salvar os dados do sistema de cria��o de personagens
    {
        data.personagens = personagensCriados;
    }

    public void CriarPersonagem() //fun��o que inicia a cria��o do personagem
    {
        personagemEmCriacao = new PersonagemData();
        _telaPersonagens.SetActive(false); //desativa a tela de personagens
        _telaSelecaoClasse.SetActive(true); //ativa a tela de sele��o de classe
        ResetarTelaPersonagem();
    }
    public void DefinirClasse(int valorClasse) //fun��o para definir a classe do personagem
    {
        switch(valorClasse)
        {
            case 0:
                personagemEmCriacao.classe = Classe.Guerreiro;
                personagemEmCriacao.apelido = "Guerreiro";
                _imagemClasseAtual = valorClasse;
                _telasArmas[valorClasse].SetActive(true); //ativa a tela de sele��o de arma � depender da classe do personagem
                break;
            case 1:
                personagemEmCriacao.classe = Classe.Arqueiro;
                personagemEmCriacao.apelido = "Arqueiro";
                _imagemClasseAtual = valorClasse;
                _telasArmas[valorClasse].SetActive(true); //ativa a tela de sele��o de arma � depender da classe do personagem
                break;
            case 2:
                personagemEmCriacao.classe = Classe.Mago;
                personagemEmCriacao.apelido = "Mago";
                _imagemClasseAtual = valorClasse;
                _telasArmas[valorClasse].SetActive(true); //ativa a tela de sele��o de arma � depender da classe do personagem
                break;
        }
        _telaSelecaoClasse.SetActive(false); //desativa a tela de sele��o de classe
    }

    public void DefinirArma(int valorArma) //fun��o para definir a arma inicial do personagem
    {
        if (personagemEmCriacao.classe == Classe.Guerreiro)
        {
            personagemEmCriacao.arma = armasGuerreiro[valorArma];
        }
        else if(personagemEmCriacao.classe == Classe.Arqueiro)
        {
            personagemEmCriacao.arma = armasArqueiro[valorArma];
        }
        else if(personagemEmCriacao.classe == Classe.Mago)
        {
            personagemEmCriacao.arma = armasMago[valorArma];
        }

        for(int i = 0; i < _telasArmas.Length; i++)
        {
            _telasArmas[i].SetActive(false); //desativa todas as telas de sele��o de armas
        }

        _telaPreferenciaHatributo.SetActive(true); //ativa a tela de sele��o de atributos de prefer�ncia
    }

    public void DefinirPreferenciaDeAtributo(int valorAtributoPreferencia) //fun��o que define as prefer�ncias de atributo do personagem
    {
        int atributos = personagemEmCriacao.atributosDePreferencia.Count; //vari�vel que verifica os atributos selecionados pelo jogador

        //define as prefer�ncias de atributo do personagem
        switch (valorAtributoPreferencia)
        {
            case 0:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Forca))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Forca);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
            case 1:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Agilidade))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Agilidade);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
            case 2:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Destreza))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Destreza);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
            case 3:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Constituicao))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Constituicao);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
            case 4:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Inteligencia))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Inteligencia);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
            case 5:
                if(!personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Sabedoria))
                {
                    personagemEmCriacao.atributosDePreferencia.Add(PreferenciaAtributo.Sabedoria);
                    atributos = personagemEmCriacao.atributosDePreferencia.Count;
                }
                break;
        }

        if (atributos >= 2)
        {
            _telaPreferenciaHatributo.SetActive(false); //desativa a tela de prefer�ncias de atributo
            PersonagemCriado();
            _telaPersonagem.SetActive(true); //ativa a tela do personagem
            AtualizarTelaPersonagem();
        }
    }

    public void DefinirApelido() //fun��o para definir o apelido do personagem
    {
        personagemEmCriacao.apelido = _apelido.text;
        _gerenciadorDeSlots.AtualizarSlots();
    }

    public void DefinirHabilidade(HabilidadeBase habilidade) //fun��o para definir as habilidades do personagem
    {
        if(habilidade.tipoDeHabilidade == TipoDeHabilidade.Classe)
        {
            if(habilidade.nivel == 1)
            {
                if (!personagemEmCriacao.runaNivel1)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa n�vel 1 n�o equipada, necess�ria para utilizar habilidade");
                }
            }
            else if(habilidade.nivel == 2)
            {
                if (!personagemEmCriacao.runaNivel2)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa n�vel 2 n�o equipada, necess�ria para utilizar habilidade");
                }
            }
            else if (habilidade.nivel == 3)
            {
                if (!personagemEmCriacao.runaNivel3)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa n�vel 3 n�o equipada, necess�ria para utilizar habilidade");
                }
            }
            personagemEmCriacao.habilidadeClasse = habilidade;
            personagemEmCriacao.habilidadeClasseID = habilidade.idHabilidade;
            ResetarTelaPersonagem();
            AtualizarTelaPersonagem();
        }
        else if(habilidade.tipoDeHabilidade == TipoDeHabilidade.Arma)
        {
            if (habilidade.nivel == 1)
            {
                if (!personagemEmCriacao.runaNivel1)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa n�vel 1 n�o equipada, necess�ria para utilizar habilidade");
                }
            }
            else if (habilidade.nivel == 2)
            {
                if (!personagemEmCriacao.runaNivel2)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa n�vel 2 n�o equipada, necess�ria para utilizar habilidade");
                }
            }
            else if (habilidade.nivel == 3)
            {
                if (!personagemEmCriacao.runaNivel3)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa n�vel 3 n�o equipada, necess�ria para utilizar habilidade");
                }
            }
            personagemEmCriacao.habilidadeArma = habilidade;
            personagemEmCriacao.habilidadeArmaID = habilidade.idHabilidade;
            ResetarTelaPersonagem();
            AtualizarTelaPersonagem();
        }
    }

    public void DefinirEquipamento(EquipamentoBase equipamento)
    {
        switch (equipamento.tipoEquipamento)
        {
            case TipoEquipamento.CabecaAcessorio:
                if(personagemEmCriacao.equipamentoCabecaAcessorio == null)
                {
                    personagemEmCriacao.equipamentoCabecaAcessorio = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    personagemEmCriacao.AplicarEfeitoEquipamento(1);
                    GerenciadorDeInventario.instancia.equipamentosCabecaAcessorio.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoCabecaAcessorio = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosCabecaAcessorio.Add(equipamento);
                }
                break;
            case TipoEquipamento.CabecaTopo:
                if (personagemEmCriacao.equipamentoCabecaTopo == null)
                {
                    personagemEmCriacao.equipamentoCabecaTopo = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    personagemEmCriacao.AplicarEfeitoEquipamento(2);
                    GerenciadorDeInventario.instancia.equipamentosCabecaTopo.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoCabecaTopo = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosCabecaTopo.Add(equipamento);
                }
                break;
            case TipoEquipamento.CabecaMedio:
                if (personagemEmCriacao.equipamentoCabecaMedio == null)
                {
                    personagemEmCriacao.equipamentoCabecaMedio = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosCabecaMedio.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoCabecaMedio = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosCabecaMedio.Add(equipamento);
                }
                break;
            case TipoEquipamento.CabecaBaixo:
                if (personagemEmCriacao.equipamentoCabecaBaixo == null)
                {
                    personagemEmCriacao.equipamentoCabecaBaixo = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosCabecaBaixo.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoCabecaBaixo = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosCabecaBaixo.Add(equipamento);
                }
                break;
            case TipoEquipamento.Armadura:
                if (personagemEmCriacao.equipamentoArmadura == null)
                {
                    personagemEmCriacao.equipamentoArmadura = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosArmadura.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoArmadura = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosArmadura.Add(equipamento);
                }
                break;
            case TipoEquipamento.Bracadeira:
                if (personagemEmCriacao.equipamentoBracadeira == null)
                {
                    personagemEmCriacao.equipamentoBracadeira = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosBracadeira.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoBracadeira = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosBracadeira.Add(equipamento);
                }
                break;
            case TipoEquipamento.MaoEsquerda:
                if (personagemEmCriacao.equipamentoMaoEsquerda == null)
                {
                    personagemEmCriacao.equipamentoMaoEsquerda = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosMaoEsquerda.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoMaoEsquerda = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosMaoEsquerda.Add(equipamento);
                }
                break;
            case TipoEquipamento.MaoDireita:
                if (personagemEmCriacao.equipamentoMaoDireita == null)
                {
                    personagemEmCriacao.equipamentoMaoDireita = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosMaoDireita.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoMaoDireita = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosMaoDireita.Add(equipamento);
                }
                break;
            case TipoEquipamento.Botas:
                if (personagemEmCriacao.equipamentoBota == null)
                {
                    personagemEmCriacao.equipamentoBota = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosBota.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoBota = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosBota.Add(equipamento);
                }
                break;
            case TipoEquipamento.Acessorio1:
                if (personagemEmCriacao.equipamentoAcessorio1 == null)
                {
                    personagemEmCriacao.equipamentoAcessorio1 = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosAcessorio1.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoAcessorio1 = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosAcessorio1.Add(equipamento);
                }
                break;
            case TipoEquipamento.Acessorio2:
                if (personagemEmCriacao.equipamentoAcessorio2 == null)
                {
                    personagemEmCriacao.equipamentoAcessorio2 = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosAcessorio2.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoAcessorio2 = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosAcessorio2.Add(equipamento);
                }
                break;
            case TipoEquipamento.BuffTemporario:
                if (personagemEmCriacao.equipamentoBuffConsumivel == null)
                {
                    personagemEmCriacao.equipamentoBuffConsumivel = equipamento;
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosBuffConsumivel.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.equipamentoBuffConsumivel = null;
                    equipamento.RemoverEfeito();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosBuffConsumivel.Add(equipamento);
                }
                break;
        }
        ResetarTelaPersonagem();
        AtualizarTelaPersonagem();
    }

    private string GerarCodigoID() //fun��o que gera o C�digoID do personagem
    {
        //define os princ�pios para gerar o c�digo
        string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numeros = "0123456789";
        System.Random sortear = new System.Random();
        string codigo;

        //gera o c�digo
        do
        {
            string parte1 = new string(new char[] {
            letras[sortear.Next(26)], letras[sortear.Next(26)], letras[sortear.Next(26)],
        });
            string parte2 = new string(new char[] {
            numeros[sortear.Next(10)], numeros[sortear.Next(10)], numeros[sortear.Next(10)]
        });
            string parte3 = new string(new char[] {
            letras[sortear.Next(26)], letras[sortear.Next(26)], letras[sortear.Next(26)],
        });

            string parte4 = new string(new char[] {
            numeros[sortear.Next(10)], numeros[sortear.Next(10)], numeros[sortear.Next(10)]
        });

            codigo = parte1 + "." + parte2 + "." + parte3 + "." + parte4;

        }while(_codigosGerados.Contains(codigo)); //ir� repetir o c�digo caso j� exista

        //adiciona o c�digo ao personagem
        _codigosGerados.Add(codigo);
        return codigo;
    }

    public void PersonagemCriado() //fun��o que finaliza a cria��o do personagem e define suas caracter�sticas b�sicas
    {
        switch (personagemEmCriacao.classe)
        {
            case Classe.Guerreiro:
                personagemEmCriacao.apelido = "Guerreiro";
                _imagemClasseAtual = 0;
                break;
            case Classe.Arqueiro:
                personagemEmCriacao.apelido = "Arqueiro";
                _imagemClasseAtual = 1;
                break;
            case Classe.Mago:
                personagemEmCriacao.apelido = "Mago";
                _imagemClasseAtual = 2;
                break;
        }
        //define o n�vel e atributos iniciais do personagem
        personagemEmCriacao.nivel = 1;
        personagemEmCriacao.forca = 1;
        personagemEmCriacao.agilidade = 1;
        personagemEmCriacao.destreza = 1;
        personagemEmCriacao.constituicao = 1;
        personagemEmCriacao.inteligencia = 1;
        personagemEmCriacao.sabedoria = 1;
        personagemEmCriacao.expProximoN�vel = 50;

        personagemEmCriacao.DefinirPersonagem();

        personagemEmCriacao.codigoID = GerarCodigoID(); //gera o c�digo do personagem
        personagensCriados.Add(personagemEmCriacao); //adiciona o personagem criado � lista
        _gerenciadorDeSlots.AdicionarSlot();
        _gerenciadorDeSlots.AtualizarSlots();
    }

    public void EditarPersonagem() //fun��o que possibilita a edi��o do personagem
    {
        _telaPersonagem.SetActive(true); //ativa a tela de personagem
        _telaPersonagens.SetActive(false); //desativa a tela de personagens
    }

    public void VerificarRuna(int nivel) //fun��o que verifica se deve equipar ou desequipar runas no personagem
    {
        switch(nivel)
        {
            case 1:
                personagemEmCriacao.runaNivel1 = !personagemEmCriacao.runaNivel1;
                if(!personagemEmCriacao.runaNivel1)
                {
                    _runasImagem[0].sprite = _runasSprites[0];
                }
                else
                {
                    _runasImagem[0].sprite = _runasSprites[1];
                }
                break;
            case 2:
                personagemEmCriacao.runaNivel2 = !personagemEmCriacao.runaNivel2;
                if(!personagemEmCriacao.runaNivel2)
                {
                    _runasImagem[1].sprite = _runasSprites[0];
                }
                else
                {
                    _runasImagem[1].sprite = _runasSprites[2];
                }
                break;
            case 3:
                personagemEmCriacao.runaNivel3 = !personagemEmCriacao.runaNivel3;
                if(!personagemEmCriacao.runaNivel3)
                {
                    _runasImagem[2].sprite = _runasSprites[0];
                }
                else
                {
                    _runasImagem[2].sprite = _runasSprites[3];
                }
                break;
        }
    }

    public void DeletarPersonagemCriado(int indice) //fun��o de deletar um personagem criado
    {
        if(indice >= 0 && indice < personagensCriados.Count)
        {
            personagemEmCriacao = personagensCriados[indice];

            #region Desequipar Equipamentos
            if(personagensCriados[indice].equipamentoCabecaAcessorio != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoCabecaAcessorio);
            }
            if(personagensCriados[indice].equipamentoCabecaTopo != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoCabecaTopo);
            }
            if(personagensCriados[indice].equipamentoCabecaMedio != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoCabecaMedio);
            }
            if(personagensCriados[indice].equipamentoCabecaBaixo != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoCabecaBaixo);
            }
            if (personagensCriados[indice].equipamentoArmadura != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoArmadura);
            }
            if (personagensCriados[indice].equipamentoBracadeira != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoBracadeira);
            }
            if(personagensCriados[indice].equipamentoMaoEsquerda != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoMaoEsquerda);
            }
            if(personagensCriados[indice].equipamentoMaoDireita != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoMaoDireita);
            }
            if(personagensCriados[indice].equipamentoBota != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoBota);
            }
            if(personagensCriados[indice].equipamentoAcessorio1 != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoAcessorio1);
            }
            if(personagensCriados[indice].equipamentoAcessorio2 != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoAcessorio2);
            }
            if(personagensCriados[indice].equipamentoBuffConsumivel != null)
            {
                DefinirEquipamento(personagensCriados[indice].equipamentoBuffConsumivel);
            }
            #endregion

            personagensCriados.RemoveAt(indice);
            _gerenciadorDeSlots.AtualizarSlots();
        }
    }

    public void DeletarTodosOsPersonagens() //fun��o para deletar todos os personagens criados
    {
        for (int i = personagensCriados.Count - 1; i >= 0; i--)
        {
            DeletarPersonagemCriado(i);
        }
    }

    #region Atualiza��o Tela de Personagem
    public void AtualizarTelaPersonagem() //fun��o da tela do personagem 
    {
        //atualiza os visuais da tela de acordo com os dados do personagem
        _apelido.text = personagemEmCriacao.apelido;
        _codigoIDTexto.text = personagemEmCriacao.codigoID;
        _personagemImagem.sprite = _personagensSprites[_imagemClasseAtual];
        _classeTexto.text += (" " + personagemEmCriacao.classe.ToString());
        _armaTexto.text = personagemEmCriacao.arma.nome;
        _nivelTexto.text += (" " + personagemEmCriacao.nivel);
        _forcaTexto.text += (" " + personagemEmCriacao.forca);
        _agilidadeTexto.text += (" " + personagemEmCriacao.agilidade);
        _destrezaTexto.text += (" " + personagemEmCriacao.destreza);
        _constituicaoTexto.text += (" " + personagemEmCriacao.constituicao);
        _inteligenciaTexto.text += (" " + personagemEmCriacao.inteligencia);
        _sabedoriaTexto.text += (" " + personagemEmCriacao.sabedoria);

        _hpTexto.text = ("HP: " + personagemEmCriacao.hp.ToString());
        _danoMeleeTexto.text = ("Dano Melee: " + personagemEmCriacao.ataque);
        _danoDistanciaTexto.text = ("Dano � Dist�ncia: " + personagemEmCriacao.ataqueDistancia);
        _danoMagicoTexto.text = ("Dano M�gico: " + personagemEmCriacao.ataqueMagico);
        _defesaTexto.text = ("Defesa: " + personagemEmCriacao.defesa);
        _defesaMagicaTexto.text = ("Defesa M�gica: " + personagemEmCriacao.defesaMagica);
        _velocidadeAtaqueTexto.text = ("Velocidade de Ataque: " + personagemEmCriacao.velocidadeAtaque);
        _esquivaTexto.text = ("Esquiva: " + personagemEmCriacao.esquiva);
        _precisaoTexto.text = ("Precis�o: " + personagemEmCriacao.precisao);
        _suporteTexto.text = ("Suporte: " + personagemEmCriacao.suporte);
        _pontosHabilidadeTexto.text = ("Pontos de Habilidade: " + personagemEmCriacao.pontosDeHabilidade);

        _nomeArmaTexto.text = ("Nome: " + personagemEmCriacao.arma.nome);
        _danoMeleeArmaTexto.text = ("Dano Melee: " + (personagemEmCriacao.arma.dano + 1));
        _danoDistanciaArmaTexto.text = ("Dano � Dist�ncia: " + (personagemEmCriacao.arma.danoDistancia + 1));
        _danoMagicoArmaTexto.text = ("Dano M�gico: " + (personagemEmCriacao.arma.danoMagico + 1));
        _velocidadeAtaqueArmaTexto.text = ("Velocidade de Ataque: " + (personagemEmCriacao.arma.velocidadeDeAtaque - 0.01f));

        _personagemEquipamentoImagem.sprite = _personagensSprites[_imagemClasseAtual];
        if(personagemEmCriacao.equipamentoCabecaAcessorio != null)
        {
            _equipamentoImagem[0].sprite = personagemEmCriacao.equipamentoCabecaAcessorio.icone;
            _equipamentoNomeTexto[0].text = personagemEmCriacao.equipamentoCabecaAcessorio.nome;
        }

        if (personagemEmCriacao.equipamentoCabecaTopo != null)
        {
            _equipamentoImagem[1].sprite = personagemEmCriacao.equipamentoCabecaTopo.icone;
            _equipamentoNomeTexto[1].text = personagemEmCriacao.equipamentoCabecaTopo.nome;
        }

        if (personagemEmCriacao.equipamentoCabecaMedio != null)
        {
            _equipamentoImagem[2].sprite = personagemEmCriacao.equipamentoCabecaMedio.icone;
            _equipamentoNomeTexto[2].text = personagemEmCriacao.equipamentoCabecaMedio.nome;
        }

        if (personagemEmCriacao.equipamentoCabecaBaixo != null)
        {
            _equipamentoImagem[3].sprite = personagemEmCriacao.equipamentoCabecaBaixo.icone;
            _equipamentoNomeTexto[3].text = personagemEmCriacao.equipamentoCabecaBaixo.nome;
        }

        if (personagemEmCriacao.equipamentoArmadura != null)
        {
            _equipamentoImagem[4].sprite = personagemEmCriacao.equipamentoArmadura.icone;
            _equipamentoNomeTexto[4].text = personagemEmCriacao.equipamentoArmadura.nome;
        }

        if (personagemEmCriacao.equipamentoBracadeira != null)
        {
            _equipamentoImagem[5].sprite = personagemEmCriacao.equipamentoBracadeira.icone;
            _equipamentoNomeTexto[5].text = personagemEmCriacao.equipamentoBracadeira.nome;
        }

        if (personagemEmCriacao.equipamentoMaoEsquerda != null)
        {
            _equipamentoImagem[6].sprite = personagemEmCriacao.equipamentoMaoEsquerda.icone;
            _equipamentoNomeTexto[6].text = personagemEmCriacao.equipamentoMaoEsquerda.nome;
        }

        if (personagemEmCriacao.equipamentoMaoDireita != null)
        {
            _equipamentoImagem[7].sprite = personagemEmCriacao.equipamentoMaoDireita.icone;
            _equipamentoNomeTexto[7].text = personagemEmCriacao.equipamentoMaoDireita.nome;
        }

        if (personagemEmCriacao.equipamentoBota != null)
        {
            _equipamentoImagem[8].sprite = personagemEmCriacao.equipamentoBota.icone;
            _equipamentoNomeTexto[8].text = personagemEmCriacao.equipamentoBota.nome;
        }

        if (personagemEmCriacao.equipamentoAcessorio1 != null)
        {
            _equipamentoImagem[9].sprite = personagemEmCriacao.equipamentoAcessorio1.icone;
            _equipamentoNomeTexto[9].text = personagemEmCriacao.equipamentoAcessorio1.nome;
        }

        if (personagemEmCriacao.equipamentoAcessorio2 != null)
        {
            _equipamentoImagem[10].sprite = personagemEmCriacao.equipamentoAcessorio2.icone;
            _equipamentoNomeTexto[10].text = personagemEmCriacao.equipamentoAcessorio2.nome;
        }

        if (personagemEmCriacao.equipamentoBuffConsumivel != null)
        {
            _equipamentoImagem[11].sprite = personagemEmCriacao.equipamentoBuffConsumivel.icone;
            _equipamentoNomeTexto[11].text = personagemEmCriacao.equipamentoBuffConsumivel.nome;
        }

        //muda a cor do texto para identificar o atributo de prefer�ncia do personagem
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Forca))
        {
            _forcaTexto.color = Color.green;
        }
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Agilidade))
        {
            _agilidadeTexto.color = Color.green;
        }
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Destreza))
        {
            _destrezaTexto.color = Color.green;
        }
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Constituicao))
        {
            _constituicaoTexto.color = Color.green;
        }
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Inteligencia))
        {
            _inteligenciaTexto.color = Color.green;
        }
        if (personagemEmCriacao.atributosDePreferencia.Contains(PreferenciaAtributo.Sabedoria))
        {
            _sabedoriaTexto.color = Color.green;
        }

        //atualiza as imagens das runas
        if (!personagemEmCriacao.runaNivel1)
        {
            _runasImagem[0].sprite = _runasSprites[0];
        }
        else
        {
            _runasImagem[0].sprite = _runasSprites[1];
        }

        if (!personagemEmCriacao.runaNivel2)
        {
            _runasImagem[1].sprite = _runasSprites[0];
        }
        else
        {
            _runasImagem[1].sprite = _runasSprites[2];
        }

        if (!personagemEmCriacao.runaNivel3)
        {
            _runasImagem[2].sprite = _runasSprites[0];
        }
        else
        {
            _runasImagem[2].sprite = _runasSprites[3];
        }

        //atualiza as habilidades
        if(personagemEmCriacao.habilidadeClasse != null)
        {
            _botaoHabilidadesClasse.gameObject.SetActive(false);
            _visualHabilidadeClasse.SetActive(true);
            _habilidadeClasseImagem.sprite = personagemEmCriacao.habilidadeClasse.spriteHabilidade;
            _habilidadeClasseTituloTexto.text = personagemEmCriacao.habilidadeClasse.nome;
            _habilidadeClasseDescricaoTexto.text = personagemEmCriacao.habilidadeClasse.descricao;
            _habilidadeClasseTipoTexto.text = ("Habilidade de " + personagemEmCriacao.habilidadeClasse.tipoDeHabilidade.ToString());
        }
        else
        {
            _visualHabilidadeClasse.SetActive(false);
            _botaoHabilidadesClasse.gameObject.SetActive(true);
        }

        if (personagemEmCriacao.habilidadeArma != null)
        {
            _botaoHabilidadesArma.gameObject.SetActive(false);
            _visualHabilidadeArma.SetActive(true);
            _habilidadeArmaImagem.sprite = personagemEmCriacao.habilidadeArma.spriteHabilidade;
            _habilidadeArmaTituloTexto.text = personagemEmCriacao.habilidadeArma.nome;
            _habilidadeArmaDescricaoTexto.text = personagemEmCriacao.habilidadeArma.descricao;
            _habilidadeArmaTipoTexto.text = ("Habilidade de " + personagemEmCriacao.habilidadeArma.tipoDeHabilidade.ToString());
        }
        else
        {
            _visualHabilidadeArma.SetActive(false);
            _botaoHabilidadesArma.gameObject.SetActive(true);
        }

        SistemaDeSalvamento.instancia.SalvarJogo();
    }

    public void ResetarTelaPersonagem() //fun��o que reseta os dados visuais da tela de personagem
    {
        //reseta todos os dados visuais
        _apelido.text = "";
        _codigoIDTexto.text = "";
        _personagemImagem.sprite = null;
        _classeTexto.text = ("Classe:");
        _armaTexto.text = ("Arma");
        _nivelTexto.text = ("N�vel:");
        _forcaTexto.text = ("For�a:");
        _agilidadeTexto.text = ("Agilidade:");
        _destrezaTexto.text = ("Destreza:");
        _constituicaoTexto.text = ("Constitui��o:");
        _inteligenciaTexto.text = ("Intelig�ncia:");
        _sabedoriaTexto.text = ("Sabedoria:");

        _hpTexto.text = "";
        _danoMeleeTexto.text = "";
        _danoDistanciaTexto.text = "";
        _danoMagicoTexto.text = "";
        _defesaTexto.text = "";
        _defesaMagicaTexto.text = "";
        _velocidadeAtaqueTexto.text = "";
        _esquivaTexto.text = "";
        _precisaoTexto.text = "";
        _suporteTexto.text = "";
        _pontosHabilidadeTexto.text = "";

        _nomeArmaTexto.text = "";
        _danoMeleeArmaTexto.text = "";
        _danoDistanciaArmaTexto.text = "";
        _danoMagicoArmaTexto.text = "";
        _velocidadeAtaqueArmaTexto.text = "";

        for(int i = 0; i < _equipamentoNomeTexto.Length; i++)
        {
            _equipamentoNomeTexto[i].text = "";
            _equipamentoImagem[i].sprite = _equipamentoSprites[0];
        }
        

        //reseta as cores dos textos
        _forcaTexto.color = Color.white;
        _agilidadeTexto.color = Color.white;
        _destrezaTexto.color = Color.white;
        _constituicaoTexto.color = Color.white;
        _inteligenciaTexto.color = Color.white;
        _sabedoriaTexto.color = Color.white;
    }
    #endregion

    #region Tempor�rio

    public void SubirNivelPersonagem() //fun��o tempor�ria que sobe o n�vel do personagem atual
    {
        personagemEmCriacao.EscolherAtributo();
        personagemEmCriacao.SubirDeNivel();
        ResetarTelaPersonagem();
        AtualizarTelaPersonagem();
        _gerenciadorDeSlots.AtualizarSlots();
    }
    #endregion
}
