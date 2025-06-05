using System;
using UnityEngine;
using Zenject;

public class DroneFractionModelInstaller : AiComponentInstaller
{
    [SerializeField] private DroneFractionSettings _defaultFraction;

    protected override Type Install(DiContainer container, out object installed)
    {
        DroneFractionModel model = new DroneFractionModel();
        model.ChangeFraction(_defaultFraction);

        container.Bind<DroneFractionModel>().FromInstance(model).AsSingle().Lazy();

        installed = model;
        return typeof(DroneFractionModel);
    }
}