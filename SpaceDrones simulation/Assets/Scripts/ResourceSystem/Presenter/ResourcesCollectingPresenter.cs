using Cysharp.Threading.Tasks;

public class ResourcesCollectingPresenter : BaseResourcesInteractionPresenter
{
    private bool _isCollecting;

    public void Collect(ResourceObject resource)
    {
        if (_isCollecting)
        {
            return;
        }

        if (!resource.IsCollecting)
        {
            _isCollecting = true;
            resource.StartCollecting();

            CollectAsync(resource).Forget();
        }
    }

    private async UniTask CollectAsync(ResourceObject resource)
    {
        await UniTask.WaitForSeconds(resource.Settings.CollectingTime);

        Model.Evaluate(resource.Settings.Cost);
        Destroy(resource.gameObject);

        _isCollecting = false;
    }
}