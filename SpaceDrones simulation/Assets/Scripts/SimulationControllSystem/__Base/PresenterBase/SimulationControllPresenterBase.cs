using UnityEngine;
using Zenject;

public abstract class SimulationControllPresenterBase<TModel, TTargetSystem, TView> : MonoBehaviour
    where TModel : SimulationControllBaseModel<TTargetSystem>
    where TTargetSystem : class
    where TView : SimulationControllBaseView
{
    [SerializeField] private TView _view;
    [Inject] private TModel _model;

    protected TModel Model => _model;
    protected TView View => _view;

    [Inject]
    private void Construct()
    {
        _model.modelInitialized += OnModelInitialized;
    }

    protected void Awake()
    {
        _view.dataChanged += OnViewChange;

        OnAwake();
    }

    protected void OnDestroy()
    {
        _model.modelInitialized -= OnModelInitialized;
        _view.dataChanged -= OnViewChange;

        OnDestroyed();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnDestroyed() { }

    protected abstract void OnModelInitialized();
    protected abstract void OnViewChange();
}