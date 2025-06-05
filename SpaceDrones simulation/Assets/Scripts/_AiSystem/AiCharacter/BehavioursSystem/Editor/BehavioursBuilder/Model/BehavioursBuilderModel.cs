using UnityEngine;

public class BehavioursBuilderModel
{
    private BehavioursBuilderGraphsInteractor _graph;

    public BehavioursBuilderModel(BehavioursBuilderGraphsInteractor graph)
    {
        _graph = graph;
    }

    public void Build(string graphName)
    {
        if (!_graph.Load(graphName))
        {
            Debug.LogError($"Critical error -> can`t build behaviours from graph: '{graphName}' because graph is not exist");
            return;
        }

        for (int i = 0; i < _graph.GraphNodesCount; i++)
        {
            BehaviourNode node = _graph.GetNode(i);

            if (node.Behaviour == null)
            {
                continue;
            }

            node.Behaviour.SetUpConnections(_graph.GetConnectedBehaviours(node));
            TrySetupPredicates(node.Behaviour, node.Predicates);
        }
    }

    private void TrySetupPredicates(BehaviourSettingsBase behaviour, AiPredicateBase[] predicates)
    {
        if (behaviour is PredicatedBehaviourSettingsBase)
        {
            PredicatedBehaviourSettingsBase predicatedBehaviour = behaviour as PredicatedBehaviourSettingsBase;

            predicatedBehaviour.SetUpPredicates(predicates);
        }
    }
}