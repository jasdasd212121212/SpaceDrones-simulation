using UnityEngine;

public class GraphNodeInteractionsView
{
    private GraphsLinksView _linksView;
    private GraphEditorPresenter _presenter;

    private NodeBase _currentNode;

    private IReadOnlyGraphNode[] _nodes;

    public GraphNodeInteractionsView(GraphsLinksView links, GraphEditorPresenter presenter)
    {
        _linksView = links;
        _presenter = presenter;
    }

    public void SetNodes(IReadOnlyGraphNode[] nodes)
    {
        UnsubscribeToNodes(_nodes);
        _nodes = nodes;
        SubscribeToNodes(_nodes);
    }

    private void SubscribeToNodes(IReadOnlyGraphNode[] nodes)
    {
        foreach (IReadOnlyGraphNode node in nodes)
        {
            node.Node.Editor.connectionStarted += OnNodeStartConnection;
            node.Node.Editor.disconnectionStarted += OnNodeStartRemoveConnection;
            node.Node.Editor.remove += OnNodeRemove;
        }
    }

    private void UnsubscribeToNodes(IReadOnlyGraphNode[] nodes)
    {
        if (nodes == null || nodes.Length == 0)
        {
            return;
        }

        foreach (IReadOnlyGraphNode node in nodes)
        {
            node.Node.Editor.connectionStarted -= OnNodeStartConnection;
            node.Node.Editor.disconnectionStarted -= OnNodeStartRemoveConnection;
            node.Node.Editor.remove -= OnNodeRemove;
        }
    }

    private void OnNodeStartConnection(NodeBase nodeBase)
    {
        if (_currentNode == null)
        {
            _currentNode = nodeBase;
            _linksView.StartDrawingLineToCursor(Color.green, _presenter.IndexOfNode(_currentNode));

            SetGlobalState(NodeActionEnum.Connect);
        }
        else
        {
            _presenter.Connect(_currentNode, nodeBase);
            _linksView.StopDrawingTemporaryCurve();
            _currentNode = null;

            SetGlobalState(NodeActionEnum.None);
        }
    }

    private void OnNodeStartRemoveConnection(NodeBase nodeBase)
    {
        if (_currentNode == null)
        {
            _currentNode = nodeBase;
            _linksView.StartDrawingLineToCursor(Color.red, _presenter.IndexOfNode(_currentNode));

            SetGlobalState(NodeActionEnum.Disconnect);
        }
        else
        {
            _presenter.RemoveConnection(_currentNode, nodeBase);
            _linksView.StopDrawingTemporaryCurve();
            _currentNode = null;

            SetGlobalState(NodeActionEnum.None);
        }
    }

    private void OnNodeRemove(NodeBase nodeBase)
    {
        _presenter.RemoveNode(nodeBase);
        _currentNode = null;
    }

    private void SetGlobalState(NodeActionEnum action)
    {
        foreach (IReadOnlyGraphNode node in _nodes)
        {
            node.Node.Editor.SetAction(action);
        }
    }
}