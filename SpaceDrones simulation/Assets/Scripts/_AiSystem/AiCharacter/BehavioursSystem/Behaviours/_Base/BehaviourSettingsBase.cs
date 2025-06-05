using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class BehaviourSettingsBase : ScriptableObject 
{
    [SerializeField] private BehaviourSettingsBase[] _connectedBehaviours;

    public BehaviourSettingsBase[] ConnectedBehaviours => _connectedBehaviours;
    public abstract Type AssociatedProviderType { get; }

#if UNITY_EDITOR
    public void SetUpConnections(BehaviourSettingsBase[] connectedBehaviours)
    {
        Undo.RecordObject(this, $"Seted up connected behaviours of {name}");

        _connectedBehaviours = connectedBehaviours;

        SetDirty();
        EditorUtility.SetDirty(this);
    }
#endif
}