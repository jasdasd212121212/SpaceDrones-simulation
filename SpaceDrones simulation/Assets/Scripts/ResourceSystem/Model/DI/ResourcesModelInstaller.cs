using System;
using Zenject;

public class ResourcesModelInstaller : AiComponentInstaller
{
    protected override Type Install(DiContainer container, out object installed)
    {
        ResourcesModel model = new ResourcesModel();
        container.Bind<ResourcesModel>().FromInstance(model).AsSingle().Lazy();

        installed = model;
        return typeof(ResourcesModel);
    }
}