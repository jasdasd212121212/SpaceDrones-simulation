using System.Collections.Generic;
using System.Linq;

public class BehavioursBuilderGraphsInteractor
{
    private IGraphsPresistanceble _graphsLoader;

    private NodeGraphSave _graph;

    public int GraphNodesCount => _graph.Nodes.Count;

    public BehavioursBuilderGraphsInteractor(IGraphsPresistanceble graphsLoader)
    {
        _graphsLoader = graphsLoader;
    }

    public bool Load(string name)
    {
        _graph = _graphsLoader.Load(name);

        return _graph != null;
    }

    public BehaviourSettingsBase[] GetConnectedBehaviours(BehaviourNode node)
    {
        Dictionary<BehaviourNode, BehaviourSettingsBase> behaviours = BuildNodesAssociatedDictionary(node);
        return SortConnectionsByWeigths(behaviours).Select(current => current.Value).ToArray();
    }

    public BehaviourSettingsBase[] GetUniqueBehaviours()
    {
        List<BehaviourSettingsBase> result = new List<BehaviourSettingsBase>();

        for (int i = 0; i < _graph.Nodes.Count; i++)
        {
            BehaviourNode node = GetNode(i);
            BehaviourSettingsBase settings = node.Behaviour;

            if (!result.Contains(settings))
            {
                result.Add(settings);
            }
        }

        return result.ToArray();
    }

    public BehaviourNode GetNode(int index) => _graph.Nodes[index].Node as BehaviourNode;

    private BehaviourSettingsBase[] GetConnectedBehavioursRaw(BehaviourNode node) => node.GetAssociations().
            Where(current => current.GetType() == typeof(BehaviourNode)).
            Select(current => current as BehaviourNode).
            Select(current => current.Behaviour).
            ToArray();

    private Dictionary<BehaviourNode, BehaviourSettingsBase> BuildNodesAssociatedDictionary(BehaviourNode node)
    {
        Dictionary<BehaviourNode, BehaviourSettingsBase> behaviours = new();
        BehaviourSettingsBase[] settings = GetConnectedBehavioursRaw(node);
        BehaviourNode[] nodes = node.GetAssociations().Select(current => current as BehaviourNode).ToArray();

        for (int i = 0; i < settings.Length; i++)
        {
            behaviours.Add(nodes[i], settings[i]);
        }

        return behaviours;
    }

    private Dictionary<BehaviourNode, BehaviourSettingsBase> SortConnectionsByWeigths(Dictionary<BehaviourNode, BehaviourSettingsBase> input)
    {
        Dictionary<BehaviourNode, BehaviourSettingsBase> source = CopyDictionary(input);
        Dictionary<BehaviourNode, BehaviourSettingsBase> result = new();

        foreach (KeyValuePair<BehaviourNode, BehaviourSettingsBase> current in input)
        {
            KeyValuePair<BehaviourNode, BehaviourSettingsBase> currentMaximal = GetMaximal(source);
            result.Add(currentMaximal.Key, currentMaximal.Value);

            source.Remove(currentMaximal.Key, out BehaviourSettingsBase v);
        }

        return result;
    }

    private KeyValuePair<BehaviourNode, BehaviourSettingsBase> GetMaximal(Dictionary<BehaviourNode, BehaviourSettingsBase> input)
    {
        int maximalWeigth = int.MinValue;
        KeyValuePair<BehaviourNode, BehaviourSettingsBase> result = default;

        foreach (KeyValuePair<BehaviourNode, BehaviourSettingsBase> current in input)
        {
            if (current.Key.Weigth > maximalWeigth)
            {
                maximalWeigth = current.Key.Weigth;
                result = current;
            }
        }

        return result;
    }

    private Dictionary<BehaviourNode, BehaviourSettingsBase> CopyDictionary(Dictionary<BehaviourNode, BehaviourSettingsBase> source)
    {
        Dictionary<BehaviourNode, BehaviourSettingsBase> result = new();

        foreach (KeyValuePair<BehaviourNode, BehaviourSettingsBase> pair in source)
        {
            result.Add(pair.Key, pair.Value);
        }

        return result;
    }
}