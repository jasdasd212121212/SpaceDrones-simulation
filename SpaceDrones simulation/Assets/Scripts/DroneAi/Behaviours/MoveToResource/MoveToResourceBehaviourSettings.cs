using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToResourceBehaviour", menuName = "Ai/Behaviours/MoveToResource")]
public class MoveToResourceBehaviourSettings : MoveBehaviourSettings 
{
    protected override Type GetAssociation()
    {
        return typeof(MoveToResourceBehaviour);
    }
}