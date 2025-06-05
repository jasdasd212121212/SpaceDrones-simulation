using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class NodeBase : ScriptableObject, IReadOnlyNode
{
    [SerializeField] private List<int> _connectedNodes = new();
    [SerializeField] private List<NodeBase> _associatedConnection = new();

    private NodeEditorBase _nodeEditor;

    public IReadOnlyList<int> ConnectedNodes => _connectedNodes;
    protected abstract NodeEditorBase SelfEditor { get; }

    public NodeEditorBase Editor 
    {
        get
        {
            if (_nodeEditor == null)
            {
                _nodeEditor = SelfEditor;
            }

            return _nodeEditor;
        }
    }

    public event Action<NodeBase> connectionStarted;
    public event Action<NodeBase> disconnectionStarted;
    public event Action<NodeBase> remove;

    public void AddConnection(int id, NodeBase node)
    {
        if (node == null || _connectedNodes.Contains(id))
        {
            Debug.LogError("Can`t add connection tu null node or existed connection");
            return;
        }

        if (!IsValidNodeForConnection(node))
        {
            return;
        }

#if UNITY_EDITOR
        SetDirty();
        EditorUtility.SetDirty(this);
        Undo.RecordObject(this, $"AddenConnection for node: {name}");
        EditorUtility.SetDirty(this);
#endif

        _connectedNodes.Add(id);
        _associatedConnection.Add(node);
    }

    public void RemoveConnection(int id, NodeBase node)
    {
        if (node == null || !_connectedNodes.Contains(id))
        {
            Debug.LogError("Can`t remove not existed node connection");
            return;
        }

        if (!IsValidNodeForConnection(node))
        {
            return;
        }

#if UNITY_EDITOR
        SetDirty();
        EditorUtility.SetDirty(this);
        Undo.RecordObject(this, $"RemovedConnection for node: {name}");
        EditorUtility.SetDirty(this);
#endif

        _connectedNodes.Remove(id);
        _associatedConnection.Remove(node);
    }

    public void RebuildAssociations(Dictionary<NodeBase, int> actualAssociations)
    {
#if UNITY_EDITOR
        SetDirty();
        EditorUtility.SetDirty(this);
        Undo.RecordObject(this, $"Associations rebuilded for node: {name}");
        EditorUtility.SetDirty(this);
#endif

        for (int i = 0; i < _connectedNodes.Count; i++)
        {
            _connectedNodes[i] = actualAssociations[_associatedConnection[i]];
        }
    }

    protected void RecordSelf(string message)
    {
#if UNITY_EDITOR
        SetDirty();
        EditorUtility.SetDirty(this);
        Undo.RecordObject(this, message);
        EditorUtility.SetDirty(this);
#endif

    }

    public NodeBase[] GetAssociations()
    {
        return _associatedConnection.ToArray();
    }

    protected virtual bool IsValidNodeForConnection(NodeBase node)
    {
        return true;
    }
}