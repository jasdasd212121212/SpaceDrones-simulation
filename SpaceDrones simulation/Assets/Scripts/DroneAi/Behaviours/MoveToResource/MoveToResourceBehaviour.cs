public class MoveToResourceBehaviour : MoveBehaviour
{
    private ResourcesModel _resourcesModel;
    private ResourceObject _targetResource;

    protected override void OnStart()
    {
        _resourcesModel = AiComponentsContainer.Pop<ResourcesModel>();
    }

    public override void Enter()
    {
        _targetResource = MarkebleObjectsPresenter.FindNearestObject
            (
                CachedTransform.position,
                GetObjectMark(),
                (obj) => (obj as ResourceObject).BookerId == _resourcesModel.ResourceBookerId || (obj as ResourceObject).BookerId == default
        ) as ResourceObject;

        _targetResource.BookResource(_resourcesModel.ResourceBookerId);
    }

    protected override MarkebleObject GetTargetObject()
    {
        return _targetResource;
    }
}