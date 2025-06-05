using Zenject;

public class ResourcesModelMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ResourcesModel>().FromInstance(new ResourcesModel()).AsSingle().Lazy();
    }
}