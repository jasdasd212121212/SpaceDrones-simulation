using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphEditorPresenter
{
    private GraphEditorModel _model;

    public IReadOnlyNodeEditorModel Model => _model;
    public bool IsGraphLoaded => _model.Graph != null;

    public GraphEditorPresenter(GraphEditorModel model)
    {
        _model = model;
    }

    public void LoadOrCreate(string name) => _model.Load(name);
    public void SaveAll() => _model.GraphFiles.SaveCurrent();

    public void AddNode(NodeBase node, Vector2 position) => _model.AddNode(node, position);

    public void RemoveNode(NodeBase node)
    {
        int nodeIndex = _model.IndexOfNode(node);

        List<IReadOnlyNode> connected = new();

        foreach (IReadOnlyGraphNode currentNode in _model.Graph.Nodes)
        {
            if (currentNode.Node.ConnectedNodes.Contains(nodeIndex))
            {
                connected.Add(currentNode.Node);
            }
        }

        foreach (IReadOnlyNode current in connected)
        {
            _model.RemoveConnection(current as NodeBase, node);
        }

        _model.RemoveNode(node);

        foreach (IReadOnlyGraphNode currentNode in _model.Graph.Nodes)
        {
            _model.RebuildNodeAssociations(currentNode.Node as NodeBase);
        }
    }

    public void Connect(NodeBase from, NodeBase to) => _model.Connect(from, to);
    public void RemoveConnection(NodeBase from, NodeBase to) => _model.RemoveConnection(from, to);

    public int IndexOfNode(NodeBase node) => _model.IndexOfNode(node);
}