using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DropResourceBehaviour", menuName = "Ai/Behaviours/DropResource")]
public class DropResourceBehaviourSettings : PredicatedBehaviourSettingsBase
{
    public override Type AssociatedProviderType => typeof(DropResourceBehaviour);
}