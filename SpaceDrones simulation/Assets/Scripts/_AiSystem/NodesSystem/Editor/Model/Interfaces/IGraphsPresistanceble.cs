public interface IGraphsPresistanceble
{
    NodeGraphSave Load(string name);
    void SaveCurrent();
    NodeGraphSave CreateGraph(string name);
    NodeBase GetNodeInstance(string name, NodeBase node, NodeGraphSave graph);
    bool GraphExist(string name);
}