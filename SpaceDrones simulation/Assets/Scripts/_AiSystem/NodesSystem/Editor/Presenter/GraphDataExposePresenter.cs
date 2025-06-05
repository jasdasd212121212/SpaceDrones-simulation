using UnityEngine;

public class GraphDataExposePresenter : MonoBehaviour
{
    private GraphEditorModel _model;

    public GraphDataExposePresenter(GraphEditorModel model)
    {
        _model = model;
    }

    public NodeGraphSave ExposeDataGraph(string name)
    {
        if (!_model.GraphFiles.GraphExist(name))
        {
            Debug.LogError($"Critical errpr -> can`t expose not existed graph by name: {name}");
            return null;
        }

        return _model.GraphFiles.Load(name);
    }
}