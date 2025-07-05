using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SistemaDeCriacaoDePersonagens : MonoBehaviour, Salvamento
{
    [Header("Personagens")]
    public List<PersonagemData> personagensCriados = new List<PersonagemData>(); //lista de todos os personagens criados
    public PersonagemData personagemEmCriacao; //representa o personagem atual que está sendo criado
    public PersonagemAtributosIniciais personagemAtributosIniciais; //prefab dos atributos iniciais do personagem

    [Header("Armas")]
    public List<ArmaBase> armas = new List<ArmaBase>(); //lista de armas equipáveis

    #region Visual
    [Header("Tela Personagem")]
    [SerializeField]
    private GameObject _telaPersonagem; //tela do personagem específico criado
    [SerializeField]
    private GameObject _telaPersonagens; //tela dos personagens criados 
    [SerializeField]
    private GameObject _telaSelecaoClasse; //tela de seleção de classes
    [SerializeField]
    private GameObject _telaPreferenciaHatributo; //tela de preferencias de atributo do personagem
    [SerializeField]
    private InputField _apelido; //texto do apelido do personagem
    [SerializeField]
    private Text _codigoIDTexto; //texto do códigoID do personagem
    [SerializeField]
    private GameObject[] _personagemImagem; //imagem do personagem criado
    [SerializeField]
    private Text _classeTexto; //texto da classe do personagem
    [SerializeField]
    private Text _armaTexto; //texto do nome da arma do personagem
    [SerializeField]
    private Text _nivelTexto; //texto do nível do personagem
    [SerializeField]
    private Text _forcaTexto; //texto da força do personagem
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
    private GameObject[] _armaImagem; //imagem do personagem criado

    [Header("Tela Habilidades")]
    [SerializeField]
    private Image[] _runasImagem; //imagem das runas
    [SerializeField]
    private Button _botaoHabilidadesClasse; //botão habilidades de classe
    [SerializeField]
    private GameObject _visualHabilidadeClasse; //visual da habilidade de classe equipada
    [SerializeField]
    private Image _habilidadeClasseImagem; //imagem da habilidade de classe equipada
    [SerializeField]
    private Text _habilidadeClasseTituloTexto; //nome da habilidade de classe equipada
    [SerializeField]
    private Text _habilidadeClasseDescricaoTexto; //descrição da habilidade de classe equipada
    [SerializeField]
    private Text _habilidadeClasseTipoTexto; //tipo da habilidade de calsse equipada
    [SerializeField]
    private Button _botaoHabilidadesArma; //botão habilidades de arma
    [SerializeField]
    private GameObject _visualHabilidadeArma; //visual da habilidade de arma equipada
    [SerializeField]
    private Image _habilidadeArmaImagem; //imagem da habilidade de arma equipada
    [SerializeField]
    private Text _habilidadeArmaTituloTexto; //nome da habilidade de arma equipada
    [SerializeField]
    private Text _habilidadeArmaDescricaoTexto; //descrição da habilidade de arma equipada
    [SerializeField]
    private Text _habilidadeArmaTipoTexto; //tipo da habilidade de arma equipada

    [Header("Tela Equipamento")]
    [SerializeField]
    private Image[] _equipamentoImagem; //imagem do ícone do equipamento
    [SerializeField]
    private Text[] _equipamentoNomeTexto; //texto do nome do equipamento

    [Header("Tela Modificadores")]
    [SerializeField]
    private Text _hpTexto; //texto do hp do personagem
    [SerializeField]
    private Text _spTexto; //texo do sp do personagem
    [SerializeField]
    private Text _hpRegeneracaoTexto; //texto do hp do personagem
    [SerializeField]
    private Text _spRegeneracaoTexto; //texo do sp do personagem
    [SerializeField]
    private Text _ataqueTexto; //texto do dano do personagem
    [SerializeField]
    private Text _defesaTexto; //texto da defesa mágica do personagem
    [SerializeField]
    private Text _defesaMagicaTexto; //texto da defesa mágica do personagem
    [SerializeField]
    private Text _velocidadeAtaqueTexto; //texto da velocidade de ataque do personagem
    [SerializeField]
    private Text _esquivaTexto; //texto da esquiva do personagem
    [SerializeField]
    private Text _precisaoTexto; //texto da precisãp do personagem
    [SerializeField]
    private Text _chanceCriticoTexto; //texto da precisãp do personagem
    [SerializeField]
    private Text _expProximoNivelTexto; //texto de exp para o próximo nível do personagem

    [Header("Sprites")]
    [SerializeField]
    private Sprite[] _runasSprites; //sprites das runas
    [SerializeField]
    private Sprite[] _equipamentoSprites;
    
    [Header("Tela Seleção de Armas")]
    [SerializeField]
    private GameObject _telasArmas; //tela de seleção de armas
    #endregion

    [HideInInspector]
    public int _imagemClasseAtual; //variável que determina a imagem do jogador a depender de sua classe
    private HashSet<string> _codigosGerados = new HashSet<string>(); //lista de códigos gerados
    [SerializeField]
    private GerenciadorDeSlots _gerenciadorDeSlots; //gerenciador de slots

    #region Salvamento
    public void CarregarSave(GameData data) //função de carregar os dados salvos do sistema de criação de personagens
    {
        personagensCriados = data.personagens;

        for (int i = 0; i < personagensCriados.Count; i++)
        {
            personagensCriados[i].listaDeHabilidadesDeClasse = new List<HabilidadeBase>();
            personagensCriados[i].listaDeHabilidadesDeArma = new List<HabilidadeBase>();

            personagensCriados[i].arma = armas[personagensCriados[i].armaID];

            foreach (DadosHabilidade dadosHabilidade in personagensCriados[i].habilidadesDeArmaSalvas)
            {
                switch (personagensCriados[i].arma.nome)
                {
                    case "Espada":
                        HabilidadeBase habilidadeEspada = GerenciadorDeInventario.instancia.habilidadesEspada.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade);
                        if (habilidadeEspada != null)
                        {
                            habilidadeEspada.nivel = personagensCriados[i].habilidadesDeArmaSalvas.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade).nivel;
                            personagensCriados[i].listaDeHabilidadesDeArma.Add(habilidadeEspada);
                            personagensCriados[i].habilidadeArma = GerenciadorDeInventario.instancia.habilidadesEspada.Find(h => h.idHabilidade == personagensCriados[i].habilidadeArmaID);
                        }
                        break;
                    case "Arco":
                        HabilidadeBase habilidadeArco = GerenciadorDeInventario.instancia.habilidadesArco.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade);
                        if (habilidadeArco != null)
                        {
                            habilidadeArco.nivel = personagensCriados[i].habilidadesDeArmaSalvas.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade).nivel;
                            personagensCriados[i].listaDeHabilidadesDeArma.Add(habilidadeArco);
                            personagensCriados[i].habilidadeArma = GerenciadorDeInventario.instancia.habilidadesArco.Find(h => h.idHabilidade == personagensCriados[i].habilidadeArmaID);
                        }
                        break;
                    case "Livro":
                        HabilidadeBase habilidadeLivro = GerenciadorDeInventario.instancia.habilidadesLivro.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade);
                        if (habilidadeLivro != null)
                        {
                            habilidadeLivro.nivel = personagensCriados[i].habilidadesDeArmaSalvas.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade).nivel;
                            personagensCriados[i].listaDeHabilidadesDeArma.Add(habilidadeLivro);
                            personagensCriados[i].habilidadeArma = GerenciadorDeInventario.instancia.habilidadesLivro.Find(h => h.idHabilidade == personagensCriados[i].habilidadeArmaID);
                        }
                        break;
                }
            }

            if (personagensCriados[i].classe == Classe.Guerreiro)
            {
                foreach(DadosHabilidade dadosHabilidade in personagensCriados[i].habilidadesDeClasseSalvas)
                {
                    HabilidadeBase habilidade = GerenciadorDeInventario.instancia.habilidadesClasseGuerreiro.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade);
                    if (habilidade != null)
                    {
                        habilidade.nivel = personagensCriados[i].habilidadesDeClasseSalvas.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade).nivel;
                        personagensCriados[i].listaDeHabilidadesDeClasse.Add(habilidade);
                    }
                }

                personagensCriados[i].habilidadeClasse = GerenciadorDeInventario.instancia.habilidadesClasseGuerreiro.Find(h => h.idHabilidade == personagensCriados[i].habilidadeClasseID);
            }
            else if (personagensCriados[i].classe == Classe.Ladino)
            {
                foreach (DadosHabilidade dadosHabilidade in personagensCriados[i].habilidadesDeClasseSalvas)
                {
                    HabilidadeBase habilidade = GerenciadorDeInventario.instancia.habilidadesClasseLadino.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade);
                    if (habilidade != null)
                    {
                        habilidade.nivel = personagensCriados[i].habilidadesDeClasseSalvas.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade).nivel;
                        personagensCriados[i].listaDeHabilidadesDeClasse.Add(habilidade);
                    }
                }

                personagensCriados[i].habilidadeClasse = GerenciadorDeInventario.instancia.habilidadesClasseLadino.Find(h => h.idHabilidade == personagensCriados[i].habilidadeClasseID);
            }
            else if (personagensCriados[i].classe == Classe.Elementalista)
            {
                foreach (DadosHabilidade dadosHabilidade in personagensCriados[i].habilidadesDeClasseSalvas)
                {
                    HabilidadeBase habilidade = GerenciadorDeInventario.instancia.habilidadesClasseElementalista.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade);
                    if (habilidade != null)
                    {
                        habilidade.nivel = personagensCriados[i].habilidadesDeClasseSalvas.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade).nivel;
                        personagensCriados[i].listaDeHabilidadesDeClasse.Add(habilidade);
                    }
                }

                personagensCriados[i].habilidadeClasse = GerenciadorDeInventario.instancia.habilidadesClasseElementalista.Find(h => h.idHabilidade == personagensCriados[i].habilidadeClasseID);
            }
            else if (personagensCriados[i].classe == Classe.Sacerdote)
            {
                foreach (DadosHabilidade dadosHabilidade in personagensCriados[i].habilidadesDeClasseSalvas)
                {
                    HabilidadeBase habilidade = GerenciadorDeInventario.instancia.habilidadesClasseSacerdote.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade);
                    if (habilidade != null)
                    {
                        habilidade.nivel = personagensCriados[i].habilidadesDeClasseSalvas.Find(h => h.idHabilidade == dadosHabilidade.idHabilidade).nivel;
                        personagensCriados[i].listaDeHabilidadesDeClasse.Add(habilidade);
                    }
                }

                personagensCriados[i].habilidadeClasse = GerenciadorDeInventario.instancia.habilidadesClasseSacerdote.Find(h => h.idHabilidade == personagensCriados[i].habilidadeClasseID);
            }
        }

        _gerenciadorDeSlots.AtualizarSlots();
    }

    public void SalvarSave(GameData data) //função de salvar os dados do sistema de criação de personagens
    {
        data.personagens = personagensCriados;
    }

    #endregion

    public void CriarPersonagem() //função que inicia a criação do personagem
    {
        personagemEmCriacao = new PersonagemData();
        _telaPersonagens.SetActive(false); //desativa a tela de personagens
        _telaSelecaoClasse.SetActive(true); //ativa a tela de seleção de classe
        ResetarTelaPersonagem();
    }
    public void DefinirClasse(int valorClasse) //função para definir a classe do personagem
    {
        switch(valorClasse)
        {
            case 0:
                personagemEmCriacao.classe = Classe.Guerreiro;
                personagemEmCriacao.apelido = "Guerreiro";
                _imagemClasseAtual = valorClasse;
                _telasArmas.SetActive(true); //ativa a tela de seleção de arma
                break;
            case 1:
                personagemEmCriacao.classe = Classe.Ladino;
                personagemEmCriacao.apelido = "Ladino";
                _imagemClasseAtual = valorClasse;
                _telasArmas.SetActive(true); //ativa a tela de seleção de arma
                break;
            case 2:
                personagemEmCriacao.classe = Classe.Elementalista;
                personagemEmCriacao.apelido = "Elementalista";
                _imagemClasseAtual = valorClasse;
                _telasArmas.SetActive(true); //ativa a tela de seleção de arma
                break;
            case 3:
                personagemEmCriacao.classe = Classe.Sacerdote;
                personagemEmCriacao.apelido = "Sacerdote";
                _imagemClasseAtual = valorClasse;
                _telasArmas.SetActive(true); //ativa a tela de seleção de arma
                break;
        }
        _telaSelecaoClasse.SetActive(false); //desativa a tela de seleção de classe
    }

    public void DefinirArma(int valorArma) //função para definir a arma inicial do personagem
    {
        personagemEmCriacao.arma = armas[valorArma];

        personagemEmCriacao.armaID = armas[valorArma].id;

        _telasArmas.SetActive(false); //desativa a telas de seleção de armas

        ResetarAtributosDePreferência();

        _telaPreferenciaHatributo.SetActive(true); //ativa a tela de seleção de atributos de preferência
    }

    public void ResetarAtributosDePreferência() //função que reseta os atributos de preferência do personagem em criação
    {
        personagemEmCriacao.atributosDePreferencia.Clear();
        personagemEmCriacao.listaSortearAtributo.Clear();
    }

    public void DefinirPreferenciaDeAtributo(int valorAtributoPreferencia) //função que define as preferências de atributo do personagem
    {
        int atributos = personagemEmCriacao.atributosDePreferencia.Count; //variável que verifica os atributos selecionados pelo jogador

        //define as preferências de atributo do personagem
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
            _telaPreferenciaHatributo.SetActive(false); //desativa a tela de preferências de atributo
            if(personagemEmCriacao.nivel == 0)
            {
                PersonagemCriado();
            }
            else
            {
                personagemEmCriacao.DefinicoesAtributos();
            }
            _telaPersonagem.SetActive(true); //ativa a tela do personagem
            ResetarTelaPersonagem();
            AtualizarTelaPersonagem();
        }
    }

    public void DefinirApelido() //função para definir o apelido do personagem
    {
        personagemEmCriacao.apelido = _apelido.text;
        _gerenciadorDeSlots.AtualizarSlots();
    }

    public void DefinirHabilidade(HabilidadeBase habilidade) //função para definir as habilidades do personagem
    {
        if(habilidade.tipoDeHabilidade == TipoDeHabilidade.Classe)
        {
            if(habilidade.nivel == 1)
            {
                if (!personagemEmCriacao.runaNivel1)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa nível 1 não equipada, necessária para utilizar habilidade");
                }
            }
            else if(habilidade.nivel == 2)
            {
                if (!personagemEmCriacao.runaNivel2)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa nível 2 não equipada, necessária para utilizar habilidade");
                }
            }
            else if (habilidade.nivel == 3)
            {
                if (!personagemEmCriacao.runaNivel3)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa nível 3 não equipada, necessária para utilizar habilidade");
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
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa nível 1 não equipada, necessária para utilizar habilidade");
                }
            }
            else if (habilidade.nivel == 2)
            {
                if (!personagemEmCriacao.runaNivel2)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa nível 2 não equipada, necessária para utilizar habilidade");
                }
            }
            else if (habilidade.nivel == 3)
            {
                if (!personagemEmCriacao.runaNivel3)
                {
                    GerenciadorDeInventario.instancia.MostrarMensagem("Runa nível 3 não equipada, necessária para utilizar habilidade");
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    personagemEmCriacao.AplicarEfeitoEquipamento(1);
                    GerenciadorDeInventario.instancia.equipamentosCabecaAcessorio.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
                    personagemEmCriacao.equipamentoCabecaAcessorio = null;
                    equipamento.RemoverEfeito();
                    personagemEmCriacao.DefinicoesBatalha();
                    equipamento.personagem = null;
                    GerenciadorDeInventario.instancia.equipamentosCabecaAcessorio.Add(equipamento);
                }
                break;
            case TipoEquipamento.CabecaTopo:
                if (personagemEmCriacao.equipamentoCabecaTopo == null)
                {
                    personagemEmCriacao.equipamentoCabecaTopo = equipamento;
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    personagemEmCriacao.AplicarEfeitoEquipamento(2);
                    GerenciadorDeInventario.instancia.equipamentosCabecaTopo.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosCabecaMedio.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosCabecaBaixo.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosArmadura.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosBracadeira.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosMaoEsquerda.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosMaoDireita.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosBota.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosAcessorio1.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosAcessorio2.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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
                    personagemEmCriacao.idsEquipamentosEquipados.Add(equipamento.id);
                    equipamento.personagem = personagemEmCriacao;
                    equipamento.AplicarEfeito();
                    GerenciadorDeInventario.instancia.equipamentosBuffConsumivel.Remove(equipamento);
                    GerenciadorDeInventario.instancia.equipouEquipamento = true;
                }
                else
                {
                    personagemEmCriacao.idsEquipamentosEquipados.Remove(equipamento.id);
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

    private string GerarCodigoID() //função que gera o CódigoID do personagem
    {
        //define os princípios para gerar o código
        string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string numeros = "0123456789";
        System.Random sortear = new System.Random();
        string codigo;

        //gera o código
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

        }while(_codigosGerados.Contains(codigo)); //irá repetir o código caso já exista

        //adiciona o código ao personagem
        _codigosGerados.Add(codigo);
        return codigo;
    }

    public void PersonagemCriado() //função que finaliza a criação do personagem e define suas características básicas
    {
        switch (personagemEmCriacao.classe)
        {
            case Classe.Guerreiro:
                personagemEmCriacao.apelido = "Guerreiro";
                _imagemClasseAtual = 0;
                break;
            case Classe.Ladino:
                personagemEmCriacao.apelido = "Ladino";
                _imagemClasseAtual = 1;
                break;
            case Classe.Elementalista:
                personagemEmCriacao.apelido = "Elementalista";
                _imagemClasseAtual = 2;
                break;
            case Classe.Sacerdote:
                personagemEmCriacao.apelido = "Sacerdote";
                _imagemClasseAtual = 3;
                break;
        }
        //define o nível e atributos iniciais do personagem
        personagemEmCriacao.nivel = 1;
        personagemEmCriacao.forca = 1;
        personagemEmCriacao.agilidade = 1;
        personagemEmCriacao.destreza = 1;
        personagemEmCriacao.constituicao = 1;
        personagemEmCriacao.inteligencia = 1;
        personagemEmCriacao.sabedoria = 1;
        personagemEmCriacao.expProximoNível = 500;

        personagemEmCriacao.DefinirPersonagem(personagemAtributosIniciais);

        personagemEmCriacao.codigoID = GerarCodigoID(); //gera o código do personagem
        personagensCriados.Add(personagemEmCriacao); //adiciona o personagem criado à lista
        _gerenciadorDeSlots.AdicionarSlot();
        _gerenciadorDeSlots.AtualizarSlots();
    }

    public void EditarPersonagem() //função que possibilita a edição do personagem
    {
        _telaPersonagem.SetActive(true); //ativa a tela de personagem
        _telaPersonagens.SetActive(false); //desativa a tela de personagens
    }

    public void VerificarRuna(int nivel) //função que verifica se deve equipar ou desequipar runas no personagem
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

    public void DeletarPersonagemCriado(int indice) //função de deletar um personagem criado
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

    public void DeletarTodosOsPersonagens() //função para deletar todos os personagens criados
    {
        for (int i = personagensCriados.Count - 1; i >= 0; i--)
        {
            DeletarPersonagemCriado(i);
        }
    }

    #region Atualização Tela de Personagem
    public void AtualizarTelaPersonagem() //função da tela do personagem 
    {
        if(personagemEmCriacao.codigoID != "")
        {
            //atualiza os dados do personagem
            _hpTexto.text = ("HP: " + personagemEmCriacao.hp);
            _spTexto.text = ("SP: " + personagemEmCriacao.sp);

            _hpRegeneracaoTexto.text = ("Regeneração de HP: " + personagemEmCriacao.hpRegeneracao + "/s");
            _spRegeneracaoTexto.text = ("Regeneração de SP: " + personagemEmCriacao.spRegeneracao + "/s");

            switch (personagemEmCriacao.arma.armaDano)
            {
                case TipoDeDano.DANO_MELEE:
                    _ataqueTexto.text = ("Dano Melee: " + personagemEmCriacao.dano);
                    break;
                case TipoDeDano.DANO_RANGED:
                    _ataqueTexto.text = ("Dano Ranged: " + personagemEmCriacao.arma.dano);
                    break;
                case TipoDeDano.DANO_MAGICO:
                    _ataqueTexto.text = ("Dano Mágico: " + personagemEmCriacao.arma.dano);
                    break;
            }
            _precisaoTexto.text = ("Precisão: " + personagemEmCriacao.precisao);

            _velocidadeAtaqueTexto.text = ("Velocidade de Ataque: " + personagemEmCriacao.velocidadeDeAtaque);

            _esquivaTexto.text = ("Esquiva: " + personagemEmCriacao.esquiva);

            _defesaTexto.text = ("Defesa: " + personagemEmCriacao.defesa);
            _defesaMagicaTexto.text = ("Defesa Mágica: " + personagemEmCriacao.defesaMagica);

            _chanceCriticoTexto.text = ("Chance Crítica: " + personagemEmCriacao.chanceCritico + "%");

            _expProximoNivelTexto.text = ("EXP para o próximo Nível: " + ((int)personagemEmCriacao.expProximoNível - personagemEmCriacao.expAtual));

            _apelido.text = personagemEmCriacao.apelido;
            _codigoIDTexto.text = personagemEmCriacao.codigoID;

            for(int i = 0; i < _personagemImagem.Length; i++)
            {
                if(i == _imagemClasseAtual)
                {
                    _personagemImagem[i].SetActive(true);
                }
                else
                {
                    _personagemImagem[i].SetActive(false);
                }
            }
            
            _classeTexto.text += (" " + personagemEmCriacao.classe.ToString());
            _armaTexto.text = personagemEmCriacao.arma.nome;
            _nivelTexto.text += (" " + personagemEmCriacao.nivel);
            _forcaTexto.text += (" " + personagemEmCriacao.forca);
            _agilidadeTexto.text += (" " + personagemEmCriacao.agilidade);
            _destrezaTexto.text += (" " + personagemEmCriacao.destreza);
            _constituicaoTexto.text += (" " + personagemEmCriacao.constituicao);
            _inteligenciaTexto.text += (" " + personagemEmCriacao.inteligencia);
            _sabedoriaTexto.text += (" " + personagemEmCriacao.sabedoria);

            for (int i = 0; i < _armaImagem.Length; i++)
            {
                if (i == personagemEmCriacao.armaID)
                {
                    _armaImagem[i].SetActive(true);
                }
                else
                {
                    _armaImagem[i].SetActive(false);
                }
            }

            if (personagemEmCriacao.equipamentoCabecaAcessorio != null)
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

            //muda a cor do texto para identificar o atributo de preferência do personagem
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
            if (personagemEmCriacao.habilidadeClasse != null)
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
        }
    }

    public void ResetarTelaPersonagem() //função que reseta os dados visuais da tela de personagem
    {
        //reseta todos os dados visuais
        _apelido.text = "";
        _codigoIDTexto.text = "";
        for(int i = 0; i < _personagemImagem.Length; i++)
        {
            _personagemImagem[i].SetActive(false);
        }
        _classeTexto.text = ("Classe:");
        _armaTexto.text = ("Arma");
        _nivelTexto.text = ("Nível:");
        _forcaTexto.text = ("Força:");
        _agilidadeTexto.text = ("Agilidade:");
        _destrezaTexto.text = ("Destreza:");
        _constituicaoTexto.text = ("Constituição:");
        _inteligenciaTexto.text = ("Inteligência:");
        _sabedoriaTexto.text = ("Sabedoria:");

        _hpTexto.text = "";
        _hpRegeneracaoTexto.text = "";
        _spTexto.text = "";
        _spRegeneracaoTexto.text = "";
        _ataqueTexto.text = "";
        _defesaTexto.text = "";
        _defesaMagicaTexto.text = "";
        _velocidadeAtaqueTexto.text = "";
        _esquivaTexto.text = "";
        _precisaoTexto.text = "";
        _expProximoNivelTexto.text = "";

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

    #region Temporário

    public void SubirNivelPersonagem() //função temporária que sobe o nível do personagem atual
    {
        personagemEmCriacao.EscolherAtributo();
        personagemEmCriacao.SubirDeNivel();
        ResetarTelaPersonagem();
        AtualizarTelaPersonagem();
        _gerenciadorDeSlots.AtualizarSlots();
    }
    #endregion
}
