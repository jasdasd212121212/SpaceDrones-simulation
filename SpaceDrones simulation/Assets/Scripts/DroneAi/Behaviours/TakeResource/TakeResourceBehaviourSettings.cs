using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TakeResource", menuName = "Ai/Behaviours/TakeResource")]
public class TakeResourceBehaviourSettings : PredicatedBehaviourSettingsBase
{
    [SerializeField] private ObjectMark _resourceObjectMark;

    public override Type AssociatedProviderType => typeof(TakeResourceBehaviour);
    public ObjectMark ResourceMark => _resourceObjectMark;
}