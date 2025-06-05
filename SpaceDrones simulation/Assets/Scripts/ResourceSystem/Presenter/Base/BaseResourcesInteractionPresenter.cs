using UnityEngine;
using Zenject;

public abstract class BaseResourcesInteractionPresenter : MonoBehaviour
{
    [Inject] private ResourcesModel _model;

    protected ResourcesModel Model => _model;
}