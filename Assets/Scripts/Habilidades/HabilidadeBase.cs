using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TipoDeHabilidade{ Classe, Arma }
public enum ModoDeAtivacao { Ativa, Passiva }
public abstract class HabilidadeBase : ScriptableObject, IHabilidade
{
    public string nome;
    [TextArea(10, 20)] 
    public string descricao;
    public TipoDeHabilidade tipoDeHabilidade;
    public ModoDeAtivacao modoDeAtivacao;
    public int nivel;
    public Sprite spriteHabilidade;
    public string idHabilidade;

    public HabilidadeBase habilidadeProximoNivel;

    public string Nome => nome;
    public string Descricao => descricao;
    public TipoDeHabilidade Tipo => tipoDeHabilidade;
    public ModoDeAtivacao Modo => modoDeAtivacao;
    public int Nivel => nivel;
    public string Id => idHabilidade;
    public Sprite Icone => spriteHabilidade;

    public abstract void AtivarEfeito(IAPersonagemBase personagem);
    public abstract void RemoverEfeito(IAPersonagemBase personagem);
}
