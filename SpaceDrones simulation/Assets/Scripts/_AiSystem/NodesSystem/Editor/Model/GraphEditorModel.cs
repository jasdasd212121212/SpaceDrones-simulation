using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GraphEditorModel : IReadOnlyNodeEditorModel
{
    private NodeGraphSave _graph;
    private IGraphsPresistanceble _graphFiles;

    public IReadOnlyGraph Graph => _graph;
    public IGraphsPresistanceble GraphFiles => _graphFiles;

    public event Action<IReadOnlyGraphNode[]> graphChanged;

    public GraphEditorModel()
    {
        _graphFiles = new GraphsPresistanceModel();
    }

    public void Load(string name)
    {
        if (!_graphFiles.GraphExist(name))
        {
            _graph = _graphFiles.CreateGraph(name);
        }
        else
        {
            _graph = _graphFiles.Load(name);
        }

        CallChangeEvent();
    }

    public void AddNode(NodeBase node, Vector2 position)
    {
        float zoom = _graph.NodesModels.Count > 0 ? _graph.NodesModels[0].ZoomScale : 1;

        NodeBase nodeModel = _graphFiles.GetNodeInstance(Guid.NewGuid().ToString(), node, _graph);
        _graph.AddNode(new GraphNodeData(position, nodeModel, zoom));

        nodeModel.Editor.SetZoom(zoom);

        CallChangeEvent();
    }

    public void RemoveNode(NodeBase node)
    {
        GraphNodeData[] data = _graph.NodesModels.Where(currentNode => currentNode.NodeModel == node).ToArray();
        _graph.RemoveNode(data[0]);

        CallChangeEvent();
    }

    public void Connect(NodeBase from, NodeBase to)
    {
        if (from == to && from != null && to != null)
        {
            Debug.LogError($"Critical error -> can`t connect node to self");
            return;
        }

        from.AddConnection(IndexOfNode(to), to);

        CallChangeEvent();
    }

    public void RemoveConnection(NodeBase from, NodeBase to)
    {
        if (from == to && from != null && to != null)
        {
            Debug.LogError($"Critical error -> can`t disconnect node to self; FROM: {from} TO: {to}");
            return;
        }

        from.RemoveConnection(IndexOfNode(to), to);

        CallChangeEvent();
    }

    public void RebuildNodeAssociations(NodeBase from)
    {
        NodeBase[] connections = from.GetAssociations();
        Dictionary<NodeBase, int> actualAssoiations = new();

        foreach (NodeBase connection in connections)
        {
            actualAssoiations.Add(connection, IndexOfNode(connection));
        }

        from.RebuildAssociations(actualAssoiations);
    }

    public NodeBase FindById(int id)
    {
        if (id < 0 || id >= _graph.Nodes.Count)
        {
            Debug.LogError($"Critical error -> can`t get node by index outside of bounds; Bounds: (0, {_graph.Nodes.Count - 1})");
            return null;
        }

        return _graph.NodesModels[id].NodeModel;
    }

    public int IndexOfNode(NodeBase node)
    {
        if (node == null)
        {
            Debug.LogError($"Critical error -> can`t find index of NULL node");
            return 0;
        }

        for (int i = 0; i < _graph.NodesModels.Count; i++)
        {
            if (_graph.NodesModels[i].NodeModel == node)
            {
                return i;
            }
        }

        Debug.LogError($"Ciritical error -> Unable to find index of node: {node}");
        return 0;
    } 

    private void CallChangeEvent()
    {
        graphChanged?.Invoke(_graph.Nodes.ToArray());
        _graphFiles.SaveCurrent();
    }
}