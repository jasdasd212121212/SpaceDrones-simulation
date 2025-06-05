using Zenject;

public abstract class SimulationControllBaseInstaller<TModel, TTargetSystem> : MonoInstaller 
    where TModel : SimulationControllBaseModel<TTargetSystem> 
    where TTargetSystem : class
{
    [Inject] private TTargetSystem _targetSystem;

    public override void InstallBindings()
    {
        Container.Bind<TModel>().FromInstance(GetModelInstance(_targetSystem)).AsSingle().Lazy();
    }

    protected abstract TModel GetModelInstance(TTargetSystem targetSystem);
}