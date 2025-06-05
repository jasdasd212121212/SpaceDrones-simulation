using Cysharp.Threading.Tasks;
using UnityEngine;

public class CollectingFromAiResourcesPresenter : BaseResourcesInteractionPresenter
{
    [SerializeField][Min(0.001f)] private float _takingDelay = 0.5f;

    public void LoadOut(ResourcesModel resourceModel)
    {
        if (resourceModel != null)
        {
            resourceModel.StartTaking();
            TakeAsync(resourceModel).Forget();
        }
    }

    private async UniTask TakeAsync(ResourcesModel resource)
    {
        await UniTask.WaitForSeconds(_takingDelay);

        Model.Evaluate(resource.ResourcesCount);
        resource.ResetAll();
    }
}