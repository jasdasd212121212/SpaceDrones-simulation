using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToBaseBehaviour", menuName = "Ai/Behaviours/MoveToBase")]
public class MovingToBaseBehaviourSettings : MoveBehaviourSettings
{
    protected override Type GetAssociation()
    {
        return typeof(MovingToBaseBehaviour);
    }
}