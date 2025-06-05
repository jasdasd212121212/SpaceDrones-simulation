using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Idel", menuName = "Ai/Behaviours/Idle")]
public class IdelBehaviourSettings : BehaviourSettingsBase
{
    public override Type AssociatedProviderType => typeof(IdleBehaviour);
}