using System;
using UnityEngine;

[Serializable]
public class GraphNodeData : IReadOnlyGraphNode
{
    [SerializeField] private Vector2 _position;
    [SerializeField] private float _currentZoomScale = 1;
    [SerializeField] private NodeBase _node;

    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public float ZoomScale 
    { 
        get => _currentZoomScale; 
        set => _currentZoomScale = value; 
    }

    public IReadOnlyNode Node => _node;
    public NodeBase NodeModel => _node;

    public GraphNodeData(Vector2 position, NodeBase node, float zoomScale)
    {
        _position = position;
        _node = node;
    }
}