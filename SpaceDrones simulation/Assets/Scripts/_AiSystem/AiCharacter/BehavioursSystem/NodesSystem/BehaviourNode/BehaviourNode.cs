using UnityEditor;
using UnityEngine;

public class BehaviourNode : NodeBase
{
    [SerializeField] private int _weigth;
    [SerializeField] private BehaviourSettingsBase _behaviour;
    [SerializeField] private AiPredicateBase[] _predicates;

    public int Weigth => _weigth;
    public BehaviourSettingsBase Behaviour => _behaviour;
    public AiPredicateBase[] Predicates => _predicates;

    protected override NodeEditorBase SelfEditor => new BehaviourNodeEditor(this);

#if UNITY_EDITOR
    public void SetBehaviour(BehaviourSettingsBase behaviour)
    {
        RecordSelf($"Changed behaviour of {this} at {GUID.Generate()}");

        _behaviour = behaviour;
    }

    public void SetPredicates(AiPredicateBase[] predicates)
    {
        RecordSelf($"Changed predicates of {this} at {GUID.Generate()}");

        _predicates = predicates;
    }

    public void SetWeigth(int newWaigth)
    {
        RecordSelf($"Change weigth of {this} at {GUID.Generate()}");

        _weigth = Mathf.Clamp(newWaigth, 0, int.MaxValue);
    }
#endif
}