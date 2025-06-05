using Zenject;

public class MarkebleObkectsHolderInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MarkebleObkectsHolderModel>().FromInstance(new MarkebleObkectsHolderModel()).AsSingle().Lazy();
    }
}