using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveBehaviour", menuName = "Ai/Behaviours/Move")]
public class MoveBehaviourSettings : PredicatedBehaviourSettingsBase
{
    [SerializeField][Min(0.001f)] private float _forwardSpeed;
    [SerializeField][Min(0.001f)] private float _backwardSpeed;
    [SerializeField] private ObjectMark _mark;

    public float ForwardSpeed => _forwardSpeed;
    public float BackwardSpeed => _backwardSpeed;
    public ObjectMark Mark => _mark;

    public override Type AssociatedProviderType => GetAssociation();

    protected virtual Type GetAssociation()
    {
        return typeof(MoveBehaviour);
    }
}