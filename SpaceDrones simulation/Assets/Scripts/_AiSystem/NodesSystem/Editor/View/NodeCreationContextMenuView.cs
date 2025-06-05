using System;
using UnityEditor;
using UnityEngine;

public class NodeCreationContextMenuView
{
    private EditorWindow _parentWindow;

    private NodeBase[] _preset;

    private Rect _window;
    private bool _isCreration;

    private string _nameFilter;
    private Vector2 _scrollRectPosition;

    public event Action<NodeBase, Vector2> nodeCreationRequired;

    public NodeCreationContextMenuView(NodePresentsLoaderModel loader, EditorWindow parentWindow)
    {
        _window = new Rect(Vector2.zero, new Vector2(200, 300));
        _preset = loader.LoadNodes();
        _parentWindow = parentWindow;
    }

    public void Draw()
    {
        if (Event.current.isMouse)
        {
            if (Event.current.button == 1)
            {
                _isCreration = !_isCreration;

                _window.position = Event.current.mousePosition;
                _parentWindow.Repaint();
            }
        }

        if (_isCreration)
        {   
            GUI.Window(int.MaxValue, _window, DrawWindow, "Create Node");
        }
    }

    private void DrawWindow(int id)
    {
        _nameFilter = EditorGUILayout.TextField(_nameFilter);
        _nameFilter = _nameFilter == null ? "" : _nameFilter;

        EditorGUILayout.Space(15);

        _scrollRectPosition = EditorGUILayout.BeginScrollView(_scrollRectPosition);

        for (int i = 0; i < _preset.Length; i++)
        {
            string name = _preset[i].name;

            if (name.Contains(_nameFilter) || _nameFilter == "")
            {
                if (GUILayout.Button(_preset[i].name))
                {
                    nodeCreationRequired?.Invoke(_preset[i], _window.position);
                    _isCreration = false;
                }
            }
        }

        EditorGUILayout.EndScrollView();
    }
}