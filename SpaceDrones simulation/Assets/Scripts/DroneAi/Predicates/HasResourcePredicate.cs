using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HasResourcePredicate", menuName = "Ai/Predicates/HasResource")]
public class HasResourcePredicate : AiPredicateBase
{
    [SerializeField] private ObjectMark _resourceMark;

    [NonSerialized] private ResourcesModel _resourceModel;

    public override bool IsValid(Vector3 selfPosition)
    {
        if (_resourceModel == null)
        {
            _resourceModel = AiComponentsContainer.Pop<ResourcesModel>();
        }

        ResourceObject target = MarkebleObjectsPresenter.FindNearestObject
            (
                selfPosition,
                _resourceMark,
                (obj) => (obj as ResourceObject).BookerId == _resourceModel.ResourceBookerId || (obj as ResourceObject).BookerId == default
            ) as ResourceObject;

        return target != null;
    }
}