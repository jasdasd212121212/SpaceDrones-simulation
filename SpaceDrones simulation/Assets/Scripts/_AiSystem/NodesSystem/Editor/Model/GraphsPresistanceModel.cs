using System;
using UnityEditor;
using UnityEngine;

public class GraphsPresistanceModel : IGraphsPresistanceble
{
    private string _grpahName;
    private const string GRAPHS_FOLDER_NAME = "__Graphs";

    public NodeGraphSave Load(string name) 
    {
        _grpahName = name;

        NodeGraphSave graph = Resources.Load<NodeGraphSave>(GetPathForResources(name, name));
        graph.SetDirty();

        return graph;
    }

    public void SaveCurrent()
    {
        foreach(ScriptableObject obj in Resources.LoadAll(GetPathForResourcesGraphRootFolder(_grpahName)))
        {
            obj.SetDirty();
            EditorUtility.SetDirty(obj);
        }
    }
    
    public NodeGraphSave CreateGraph(string name)
    {
        CreateAsset(name, name, typeof(NodeGraphSave), true, null);
        return Load(name);
    }

    public NodeBase GetNodeInstance(string name, NodeBase node, NodeGraphSave graph)
    {
        return CreateAsset(_grpahName, name, node.GetType(), false, graph) as NodeBase;
    }

    public bool GraphExist(string name)
    {
        try
        {
            Load(name);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private ScriptableObject CreateAsset(string folder, string name, Type type, bool doCreateFolder, NodeGraphSave graph)
    {
        ScriptableObject result = ScriptableObject.CreateInstance(type);

        if (graph == null)
        {
            if (doCreateFolder)
            {
                AssetDatabase.CreateFolder(GetPathForAssetsFolder(), name);
            }

            AssetDatabase.CreateAsset(result, GetPathForAssets(name, folder));
        }
        else
        {
            result.name = name;
            AssetDatabase.AddObjectToAsset(result, graph);

            graph.SetDirty();
        }

        result.SetDirty();

        return result;
    }

    private string GetPathForResources(string name, string folder) => $"{GRAPHS_FOLDER_NAME}\\{folder}\\{name}";
    private string GetPathForResourcesGraphRootFolder(string folderName) => $"{GRAPHS_FOLDER_NAME}\\{folderName}";
    private string GetPathForAssetsFolder() => $"Assets\\Resources\\{GRAPHS_FOLDER_NAME}";
    private string GetPathForAssets(string name, string folder) => $"Assets\\Resources\\{GRAPHS_FOLDER_NAME}\\{folder}\\{name}.asset";
}