using Zenject;

public class MarkebleObjectsPresenterInstaller : MonoInstaller
{
    [Inject] private MarkebleObkectsHolderModel _model;

    public override void InstallBindings()
    {
        Container.Bind<MarkebleObjectsPresenter>().FromInstance(new MarkebleObjectsPresenter(_model)).AsSingle().Lazy();
    }
}