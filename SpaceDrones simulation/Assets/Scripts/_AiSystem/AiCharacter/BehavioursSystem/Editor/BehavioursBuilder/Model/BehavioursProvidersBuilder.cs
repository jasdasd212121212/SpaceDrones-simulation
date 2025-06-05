using UnityEditor;
using UnityEngine;

public class BehavioursProvidersBuilder
{
    private BehavioursBuilderGraphsInteractor _graph;

    public BehavioursProvidersBuilder(BehavioursBuilderGraphsInteractor graph)
    {
        _graph = graph;
    }

    public void Build(string graphName, BehavioursStateMachine enemyStateMachine)
    {
        if (!_graph.Load(graphName))
        {
            Debug.LogError($"Critical error -> can`t build behaviours PROVIDERS from graph: '{graphName}' because graph is not exist");
            return;
        }

        DestroyCurrentBehaviourProviders(enemyStateMachine.Behaviours);

        BehaviourBaseGeneric[] providers = CreateBehaviours(_graph.GetUniqueBehaviours());

        for (int i = 0; i < providers.Length; i++)
        {
            providers[i].transform.SetParent(enemyStateMachine.transform);
            providers[i].transform.localPosition = Vector3.zero;
            providers[i].transform.localScale = Vector3.one;

            EditorUtility.SetDirty(providers[i]);
        }

        Undo.RegisterFullObjectHierarchyUndo(enemyStateMachine);

        enemyStateMachine.SetUpBehaviours(providers);
    }

    private BehaviourBaseGeneric[] CreateBehaviours(BehaviourSettingsBase[] behaviours)
    {
        BehaviourBaseGeneric[] result = new BehaviourBaseGeneric[behaviours.Length];

        for (int i = 0; i < behaviours.Length; i++)
        {
            result[i] = new GameObject($"{behaviours[i].name}-provider").AddComponent(behaviours[i].AssociatedProviderType).GetComponent<BehaviourBaseGeneric>();
            result[i].SetSettings(behaviours[i]);

            Undo.RegisterCreatedObjectUndo(result[i].gameObject, $"Created behaviour provider: {result[i].gameObject.name}");
            Undo.RecordObject(result[i], $"Setted settings of {result[i]}");

            EditorUtility.SetDirty(result[i]);  
        }

        return result;
    }

    private void DestroyCurrentBehaviourProviders(BehaviourBaseGeneric[] behaviours)
    {
        for (int i = 0; i < behaviours.Length; i++)
        {
            Undo.DestroyObjectImmediate(behaviours[i].gameObject);
        }
    }
}