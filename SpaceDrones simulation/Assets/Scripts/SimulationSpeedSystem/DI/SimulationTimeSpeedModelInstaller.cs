using UnityEngine;
using Zenject;

public class SimulationTimeSpeedModelInstaller : MonoInstaller
{
    [SerializeField] private SimulationTimeSpeedSettings _settings;

    public override void InstallBindings()
    {
        Container.Bind<SimulationTimeSpeedModel>().FromInstance(new SimulationTimeSpeedModel(_settings)).AsSingle().NonLazy();
    }
}