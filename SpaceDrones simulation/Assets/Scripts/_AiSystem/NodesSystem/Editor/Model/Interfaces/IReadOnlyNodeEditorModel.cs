using System;

public interface IReadOnlyNodeEditorModel
{
    IReadOnlyGraph Graph { get; }
    event Action<IReadOnlyGraphNode[]> graphChanged;
}