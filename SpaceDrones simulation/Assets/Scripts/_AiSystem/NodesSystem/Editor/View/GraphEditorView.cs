using System.Linq;
using UnityEditor;
using UnityEngine;

public class GraphEditorView : EditorWindow
{
    private GraphEditorPresenter _presenter;
    private GraphNodesWindowsView _windowsView;
    private NodeCreationContextMenuView _creationContexMenu;
    private UpdatingCursorRectDrawerHelper _cursor;

    private string _graphName;

    private bool _initialized;

    public void Initialize(GraphEditorPresenter presenter)
    {
        if (_initialized)
        {
            return;
        }

        _cursor = new();
        _creationContexMenu = new(new(), this);

        _presenter = presenter;
        _windowsView = new(_cursor, _presenter, this);

        _presenter.Model.graphChanged += OnReactiveFetch;
        _creationContexMenu.nodeCreationRequired += OnCreationRequired;
        Undo.undoRedoPerformed += OnUndoFetch;

        _initialized = true;
    }

    private void OnDestroy()
    {
        _presenter.Model.graphChanged -= OnReactiveFetch;
        _creationContexMenu.nodeCreationRequired -= OnCreationRequired;
        Undo.undoRedoPerformed -= OnUndoFetch;

        _windowsView.SaveNodesPositions();
        _presenter.SaveAll();
    }

    private void OnGUI()
    {
        if (!_presenter.IsGraphLoaded)
        {
            DrawLoadMenu();
        }
        else
        {
            DrawEditor();
        }
    }

    private void DrawLoadMenu()
    {
        _graphName = EditorGUILayout.TextField(_graphName);

        if (GUILayout.Button("Load"))
        {
            _presenter.LoadOrCreate(_graphName);
            FetchWindows();
        }
    }

    private void OnCreationRequired(NodeBase node, Vector2 position)
    {
        _presenter.AddNode(node, position);
    }

    private void DrawEditor()
    {
        BeginWindows(); 

        _windowsView.Draw();
        _creationContexMenu.Draw();

        EndWindows();
    }

    private void FetchWindows()
    {
        _windowsView.SetNodes(_presenter.Model.Graph.Nodes.ToArray());
    }

    private void OnReactiveFetch(IReadOnlyGraphNode[] nodes)
    {
        _windowsView.SaveNodesPositions();
        _windowsView.SetNodes(nodes);
    }

    private void OnUndoFetch()
    {
        OnReactiveFetch(_presenter.Model.Graph.Nodes.ToArray());
        Repaint();
    }
}