using UnityEngine;
using Zenject;

public class DronesSpawnerModelInstaller : MonoInstaller
{
    [SerializeField] private DronesSpawnerSettings _settings;
    [SerializeField] private Transform[] _fractionsSpawnPoints;

    private DronesSpawnerModel _spawner;

    public override void InstallBindings()
    {
        _spawner = new DronesSpawnerModel(_settings, _fractionsSpawnPoints);
        Container.Bind<DronesSpawnerModel>().FromInstance(_spawner).AsSingle().NonLazy();
    }

    private void Start()
    {
        _spawner.Spawn();
    }
}