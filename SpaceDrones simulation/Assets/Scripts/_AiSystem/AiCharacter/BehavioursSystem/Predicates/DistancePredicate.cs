using UnityEngine;

[CreateAssetMenu(fileName = "DistancePredicate", menuName = "Ai/Predicates/Distance")]
public class DistancePredicate : AiPredicateBase
{
    [SerializeField] private ObjectMark _targetObjectMark;
    [SerializeField][Min(0.01f)] private float _acticvatingDistance;
    [SerializeField][Min(0.001f)] private float _deactivatingDistance;

    private void OnValidate()
    {
        _deactivatingDistance = Mathf.Clamp(_deactivatingDistance, 0.001f, _acticvatingDistance);
    }

    public override bool IsValid(Vector3 selfPosition)
    {
        MarkebleObject markebleObject = MarkebleObjectsPresenter.FindNearestObject(selfPosition, _targetObjectMark);

        if (markebleObject == null)
        {
            return false;
        }

        float distance = Vector3.Distance(selfPosition, markebleObject.CachedTransform.position);
        return distance < _acticvatingDistance && distance > _deactivatingDistance;
    }
}