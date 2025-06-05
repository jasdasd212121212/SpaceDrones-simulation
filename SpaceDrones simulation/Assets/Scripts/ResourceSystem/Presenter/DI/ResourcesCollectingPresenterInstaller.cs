using System;
using UnityEngine;
using Zenject;

public class ResourcesCollectingPresenterInstaller : AiComponentInstaller
{
    [SerializeField] private ResourcesCollectingPresenter _presenter;

    protected override Type Install(DiContainer container, out object installed)
    {
        container.Bind<ResourcesCollectingPresenter>().FromInstance(_presenter).AsSingle().Lazy();

        installed = _presenter;
        return typeof(ResourcesCollectingPresenter);
    }
}