using Zenject;
using UnityEngine;

public abstract class ResourcesViewBase : MonoBehaviour
{
    [Inject] private ResourcesModel _model;

    private void Start()
    {
        OnChangeResourcesCount();

        _model.resourcesChanged += OnChangeResourcesCount;
        _model.takingStarted += OnStartTaking;
        _model.takingCompleted += OnEndTaking;
    }

    private void OnDestroy()
    {
        _model.resourcesChanged -= OnChangeResourcesCount;
        _model.takingStarted -= OnStartTaking;
        _model.takingCompleted -= OnEndTaking;
    }

    private void OnChangeResourcesCount()
    {
        DisplayResourcesCount(_model.ResourcesCount);
    }

    protected virtual void DisplayResourcesCount(int resourcesCount) { }
    protected virtual void OnStartTaking() { }
    protected virtual void OnEndTaking() { }
}