using System.Collections.Generic;

public interface IReadOnlyGraph
{
    public IReadOnlyList<IReadOnlyGraphNode> Nodes { get; }
}