public class TakeResourceBehaviour : PredicatedBehaviourBase<TakeResourceBehaviourSettings>
{
    private ResourcesCollectingPresenter _collectingPresenter;
    private ResourcesModel _resourcesModel;

    protected override void OnStart()
    {
        _collectingPresenter = AiComponentsContainer.Pop<ResourcesCollectingPresenter>();
        _resourcesModel = AiComponentsContainer.Pop<ResourcesModel>();
    }

    public override void Enter()
    {
        _collectingPresenter.Collect(MarkebleObjectsPresenter.FindNearestObject(CachedTransform.position, Settings.ResourceMark, (obj) => (obj as ResourceObject).BookerId == _resourcesModel.ResourceBookerId) as ResourceObject);
    }
}