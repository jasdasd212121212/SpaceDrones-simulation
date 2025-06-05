using System;
using UnityEngine;
using Zenject;

public class PathRenderingViewInstaller : AiComponentInstaller
{
    [SerializeField] private PathRenderingView _view;

    protected override Type Install(DiContainer container, out object installed)
    {
        container.Bind<PathRenderingView>().FromInstance(_view).AsSingle().Lazy();

        _view.SetViewActive(false);

        installed = _view;
        return typeof(PathRenderingView);
    }
}