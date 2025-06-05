using UnityEngine;
using UnityEditor;
using System.Linq;

public class GraphNodesWindowsView
{
    private GraphsLinksView _linksView;
    private GraphNodeInteractionsView _interactionView;

    private Rect[] _rects;
    private IReadOnlyNode[] _nodes;
    private IReadOnlyGraphNode[] _graphNodes;

    private float _zoomScale = 1f;
    private float _zoomDelta;

    private EditorWindow _selfWindow;

    private const float ZOOM_DELTA_MULTIPLIER = 100;

    private const float ZOOM_MINIMAL = 0.1f;
    private const float ZOOM_MAXIMAL = 10f;

    public Rect[] Rects => _rects;

    public GraphNodesWindowsView(UpdatingCursorRectDrawerHelper cursor, GraphEditorPresenter presenter, EditorWindow window)
    {
        _selfWindow = window;
        _linksView = new(cursor);
        _interactionView = new(_linksView, presenter);
    }

    public void SetNodes(IReadOnlyGraphNode[] nodes)
    {
        if (nodes == null || nodes.Length == 0)
        {
            _rects = new Rect[0];
            _nodes = new IReadOnlyNode[0];
            _graphNodes = new IReadOnlyGraphNode[0];

            return;
        }

        _rects = new Rect[nodes.Length];
        _nodes = nodes.Select(node => node.Node).ToArray();
        _graphNodes = nodes;

        _zoomScale = nodes[0].ZoomScale;

        for (int i = 0; i < _nodes.Length; i++)
        {
            _rects[i] = new Rect(nodes[i].Position, nodes[i].Node.Editor.Size);
            nodes[i].Node.Editor.SetZoom(_zoomScale);
        }

        _linksView.SetNodes(_rects, nodes);
        _interactionView.SetNodes(nodes);
    }

    public void SaveNodesPositions()
    {
        if (_rects == null || _rects.Length == 0)
        {
            return;
        }

        for (int i = 0; i < _graphNodes.Length; i++)
        {
            _graphNodes[i].Position = _rects[i].position;
            _graphNodes[i].ZoomScale = _zoomScale;
        }
    }

    public void Draw()
    {
        if (_rects == null || _rects.Length == 0)
        {
            return;
        }

        if (Event.current.isScrollWheel)
        {
            _zoomDelta = Event.current.delta.normalized.y / ZOOM_DELTA_MULTIPLIER;

            _zoomScale += _zoomDelta;
            _zoomScale = Mathf.Clamp(_zoomScale, ZOOM_MINIMAL, ZOOM_MAXIMAL);
        }

        DrawEditor();
        HandleFieldMovement();
    }

    private void DrawEditor()
    {
        GUIStyle style = new GUIStyle(GUI.skin.window);

        style.fontSize = (int)(12 * _nodes[0].Editor.ZoomScale);

        for (int i = 0; i < _nodes.Length; i++)
        {
            if (_nodes[i].Editor.ZoomScale != _zoomScale)
            {
                SaveNodesPositions();

                _nodes[i].Editor.SetZoom(_zoomScale);

                _graphNodes[i].Position = _zoomDelta > 0 ? 
                    _graphNodes[i].Position + (_graphNodes[i].Position * (_zoomScale / ZOOM_DELTA_MULTIPLIER * 2)) :
                    _graphNodes[i].Position - (_graphNodes[i].Position * (_zoomScale / ZOOM_DELTA_MULTIPLIER * 2));

                _rects[i] = new Rect(_graphNodes[i].Position, _nodes[i].Editor.Size);

                _selfWindow.Repaint();
            }

            _rects[i] = GUI.Window(i, _rects[i], _nodes[i].Editor.Draw, _nodes[i].GetType().Name, style);
        }

        _linksView.Draw();
    }

    private void HandleFieldMovement()
    {
        if (Event.current.isMouse)
        {
            int mouseButton = Event.current.button;

            if (mouseButton == 2)
            {
                MoveAllNodes(Event.current.delta);
            }

            _selfWindow.Repaint();
        }
    }

    private void MoveAllNodes(Vector2 delta)
    {
        for (int i = 0; i < _rects.Length; i++)
        {
            _rects[i].position += delta.normalized * 2;
        }
    }
}