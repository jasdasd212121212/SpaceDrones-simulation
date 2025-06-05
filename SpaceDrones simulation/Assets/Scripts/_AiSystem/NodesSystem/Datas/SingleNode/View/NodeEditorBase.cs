#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System;

public abstract class NodeEditorBase
{
    private NodeBase _node;

    private float _zoomScale = 1;
    private NodeActionEnum _currentAction;

    public Vector2 Size => TrueSize * ZoomScale;
    public float ZoomScale => _zoomScale;
    protected NodeBase Node => _node;

    protected abstract Vector2 TrueSize { get; }

    public event Action<NodeBase> connectionStarted;
    public event Action<NodeBase> disconnectionStarted;
    public event Action<NodeBase> remove;

    public NodeEditorBase(NodeBase node)
    {
        _node = node;
    }

    public void SetAction(NodeActionEnum nodeAction)
    {
        _currentAction = nodeAction;
    }

    public void SetZoom(float zoom)
    {
        if (zoom <= 0)
        {
            Debug.LogError($"Can`t set zoom <= 0");
            return;
        }

        _zoomScale = zoom;
    }

#if UNITY_EDITOR
    public void Draw(int id)
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);

        buttonStyle.fixedHeight = 25 * _zoomScale;
        buttonStyle.fontSize = (int)(12 * _zoomScale);

        if (_currentAction == NodeActionEnum.Connect || _currentAction == NodeActionEnum.None)
        {
            if (GUILayout.Button("Add connect", buttonStyle))
            {
                connectionStarted?.Invoke(_node);
            }
        }

        EditorGUILayout.Space(5 * _zoomScale);

        if (_currentAction == NodeActionEnum.Disconnect || _currentAction == NodeActionEnum.None)
        {
            if (GUILayout.Button("Remove connection", buttonStyle))
            {
                disconnectionStarted?.Invoke(_node);
            }
        }

        EditorGUILayout.Space(15 * _zoomScale);

        if (_currentAction == NodeActionEnum.None)
        {
            if (GUILayout.Button("Remove", buttonStyle))
            {
                remove?.Invoke(_node);
            }
        }

        EditorGUILayout.Space(30 * _zoomScale);

        OnDraw();
        GUI.DragWindow();
    }

    protected abstract void OnDraw();
#endif
}