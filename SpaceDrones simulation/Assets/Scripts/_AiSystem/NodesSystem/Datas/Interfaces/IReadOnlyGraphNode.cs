using UnityEngine;

public interface IReadOnlyGraphNode
{
    public Vector2 Position { get; set; }
    public float ZoomScale { get; set; }
    public IReadOnlyNode Node { get; }
}