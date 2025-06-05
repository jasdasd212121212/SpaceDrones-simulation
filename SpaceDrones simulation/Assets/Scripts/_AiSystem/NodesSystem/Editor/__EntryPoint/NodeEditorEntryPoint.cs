using UnityEditor;
using UnityEngine;

public class NodeEditorEntryPoint : EditorWindow
{
    [MenuItem("Window/BehaviourTreeEditor/Edit")]
    public static void Open()
    {
        GraphEditorModel model = new();
        GraphEditorPresenter presenter = new(model);

        GetWindow<GraphEditorView>().Initialize(presenter);
    }
}