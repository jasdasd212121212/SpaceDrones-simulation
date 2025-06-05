using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class PredicatedBehaviourSettingsBase : BehaviourSettingsBase
{
    [SerializeField] private AiPredicateBase[] _predicates;

    public AiPredicateBase[] Predicates => _predicates;

#if UNITY_EDITOR
    public void SetUpPredicates(AiPredicateBase[] predicates)
    {
        Undo.RecordObject(this, $"Seted up predicates of {name}");

        _predicates = predicates;

        SetDirty();
        EditorUtility.SetDirty(this);
    }
#endif
}