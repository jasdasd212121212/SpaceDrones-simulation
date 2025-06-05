using System.Collections.Generic;

public interface IReadOnlyNode
{
    public IReadOnlyList<int> ConnectedNodes { get; }
    public NodeEditorBase Editor { get; }
}