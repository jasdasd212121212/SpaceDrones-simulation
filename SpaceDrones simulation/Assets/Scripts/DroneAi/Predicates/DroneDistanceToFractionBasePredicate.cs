using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DistanceToBasePredicate", menuName = "Ai/Predicates/DistanceToBase")]
public class DroneDistanceToFractionBasePredicate : AiPredicateBase
{
    [SerializeField][Min(0.01f)] private float _acticvatingDistance;
    [SerializeField][Min(0.001f)] private float _deactivatingDistance;

    [NonSerialized] private DroneFractionModel _model;

    private void OnValidate()
    {
        _deactivatingDistance = Mathf.Clamp(_deactivatingDistance, 0.001f, _acticvatingDistance);
    }

    public override bool IsValid(Vector3 selfPosition)
    {
        if (_model == null)
        {
            _model = AiComponentsContainer.Pop<DroneFractionModel>();
        }

        MarkebleObject baseMark = MarkebleObjectsPresenter.FindNearestObject(selfPosition, _model.CurrentFraction.FractionBaseMark);

        if (baseMark == null)
        {
            return false;
        }

        float distance = Vector3.Distance(selfPosition, baseMark.CachedTransform.position);
        return distance < _acticvatingDistance && distance > _deactivatingDistance;
    }
}