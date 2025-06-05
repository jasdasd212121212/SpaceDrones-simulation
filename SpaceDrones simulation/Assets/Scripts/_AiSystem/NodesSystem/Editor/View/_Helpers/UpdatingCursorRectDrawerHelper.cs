using UnityEngine;

public class UpdatingCursorRectDrawerHelper
{
    private Rect _cursorRect;

    public Rect CursorRect => _cursorRect;

    public UpdatingCursorRectDrawerHelper()
    {
        _cursorRect = new Rect(Vector2.zero, new Vector2(10, 10));
    }

    public void Draw()
    {
        Vector2 curcorPoint = Event.current.mousePosition;
        _cursorRect.position = new Vector2(curcorPoint.x - _cursorRect.size.x / 2, curcorPoint.y - _cursorRect.size.y / 2);
        GUI.Button(_cursorRect, "");
    }
}