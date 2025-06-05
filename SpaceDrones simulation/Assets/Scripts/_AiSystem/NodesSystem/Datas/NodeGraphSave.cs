using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NodeGraphSave : ScriptableObject, IReadOnlyGraph
{
    [SerializeField] private List<GraphNodeData> _nodes = new();

    public IReadOnlyList<IReadOnlyGraphNode> Nodes => _nodes;
    public IReadOnlyList<GraphNodeData> NodesModels => _nodes;

    public void AddNode(GraphNodeData node)
    {
#if UNITY_EDITOR
        SetDirty();
        EditorUtility.SetDirty(this);
        Undo.RecordObject(this, $"Adden node: {node.NodeModel.name}");
        EditorUtility.SetDirty(this);
#endif

        _nodes.Add(node);
    }

    public void RemoveNode(GraphNodeData node)
    {
        if (node == null || !_nodes.Contains(node))
        {
            Debug.LogError("Can`t remove not existed node");
            return;
        }

#if UNITY_EDITOR
        SetDirty();
        EditorUtility.SetDirty(this);
        Undo.RecordObject(this, $"Removed node: {node.NodeModel.name}");
        EditorUtility.SetDirty(this);
#endif
        
        _nodes.Remove(node);
    }
}