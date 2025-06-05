using UnityEngine;
using Zenject;

public class ResourcesSpawnerInstaller : MonoInstaller
{
    [SerializeField] private ResourcesSpawnerSettings _settings;
    [SerializeField] private Transform _spawnerOrigin;

    public override void InstallBindings()
    {
        Container.Bind<ResourcesSpawnerModel>().FromInstance(new ResourcesSpawnerModel(_settings, _spawnerOrigin)).AsSingle().NonLazy();
    }
}