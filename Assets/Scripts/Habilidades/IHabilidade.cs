using UnityEngine;

public interface IHabilidade
{
    string Nome { get; }
    string Descricao { get; }
    TipoDeHabilidade Tipo { get; }
    ModoDeAtivacao Modo { get; }
    int Nivel { get; }
    string Id { get; }
    Sprite Icone { get; }

    void AtivarEfeito(IAPersonagemBase personagem);
    void RemoverEfeito(IAPersonagemBase personagem);
}
