using System.Linq;
using UnityEngine;

public class NodePresentsLoaderModel
{
    private const string PRESENTS_PATH = "__GraphsNodePresents";

    public NodeBase[] LoadNodes()
    {
        return Resources.LoadAll(PRESENTS_PATH, typeof(NodeBase)).Select(node => node as NodeBase).ToArray();
    }
}