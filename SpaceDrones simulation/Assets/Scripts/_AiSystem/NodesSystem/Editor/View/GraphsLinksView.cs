using UnityEditor;
using UnityEngine;

public class GraphsLinksView
{
    private UpdatingCursorRectDrawerHelper _cursor;

    private Rect[] _rects;
    private IReadOnlyGraphNode[] _nodes;

    private int _currentIndex;
    private bool _isDrawingToCursor;
    private Color _temporatyCurveColor;

    private const float ARROW_HEIGTH = 20;
    private const float ARROW_WIDTH = 5;

    public GraphsLinksView(UpdatingCursorRectDrawerHelper cursor)
    { 
        _cursor = cursor;
    }

    public void SetNodes(Rect[] rects, IReadOnlyGraphNode[] nodes)
    {
        _rects = rects;
        _nodes = nodes;
    }

    public void Draw()
    {
        try
        {
            DrawLinks();
        }
        catch { }
        
        DrawCursorTemporaryLink();
    }

    public void StartDrawingLineToCursor(Color color, int nodeIndex)
    {
        _isDrawingToCursor = true;
        _temporatyCurveColor = color;
        _currentIndex = nodeIndex;
    }

    public void StopDrawingTemporaryCurve()
    {
        _isDrawingToCursor = false;
    }

    private void DrawLinks()
    {
        for (int i = 0; i < _nodes.Length; i++)
        {
            Rect start = _rects[i];

            for (int j = 0; j < _nodes[i].Node.ConnectedNodes.Count; j++)
            {
                DrawLink(_rects[_nodes[i].Node.ConnectedNodes[j]], start, Color.white, 3);
            }
        }
    }

    private void DrawCursorTemporaryLink()
    {
        if (_isDrawingToCursor)
        {
            _cursor.Draw();
            DrawLink(_cursor.CursorRect, _rects[_currentIndex], _temporatyCurveColor, 3);
        }
    }

    private void DrawLink(Rect end, Rect start, Color color, float offset)
    {
        Rect arrowStart = new Rect(new Vector2(end.position.x - offset, end.position.y + end.height / 2), Vector2.zero);

        Rect topEnd = new Rect(new Vector2(arrowStart.position.x - ARROW_HEIGTH, arrowStart.position.y - ARROW_WIDTH), Vector2.zero);
        Rect bottomEnd = new Rect(new Vector2(arrowStart.position.x - ARROW_HEIGTH, arrowStart.position.y + ARROW_WIDTH), Vector2.zero);

        DrawLine(arrowStart, start, color, 80);

        DrawLine(topEnd, arrowStart, color, 1);
        DrawLine(bottomEnd, arrowStart, color, 1);
    }

    private void DrawLine(Rect end, Rect start, Color color, float angle)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * angle;
        Vector3 endTan = endPos + Vector3.left * angle;
        Color shadowCol = new Color(0, 0, 0, 0.06f);

        for (int i = 0; i < 3; i++)
        {
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        }

        Handles.DrawBezier(startPos, endPos, startTan, endTan, color, null, 1);
    }
}