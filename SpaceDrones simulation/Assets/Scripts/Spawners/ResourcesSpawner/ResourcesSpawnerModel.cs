using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourcesSpawnerModel
{
    private GenericFactory<ResourceObject> _resourcesFactory;
    private Vector2 _range;
    private Vector3 _originPoint;

    private CancellationTokenSource _spawnLoopCancellationTokenSource;

    private bool _isSpawning;
    private float _spawnDelay;

    public float SpawnDelay => _spawnDelay;
    public float MinSpawnDelay => Time.fixedDeltaTime;

    public event Action initialized;

    public ResourcesSpawnerModel(ResourcesSpawnerSettings settings, Transform origin)
    {
        _resourcesFactory = new GenericFactory<ResourceObject>(settings.ResourceObject, origin);
        _range = settings.SpawnRange;
        _originPoint = origin.position;

        _isSpawning = true;
        _spawnLoopCancellationTokenSource = new CancellationTokenSource();

        SetSpawnDelay(settings.InitialSpawnDelay);

        SpawnLoop().Forget();
    }

    ~ResourcesSpawnerModel()
    {
        _spawnLoopCancellationTokenSource.Cancel();
        _isSpawning = false;
    }

    public void SetSpawnDelay(float spawnDelay)
    {
        if (spawnDelay < MinSpawnDelay)
        {
            Debug.LogError($"Ciritcal error -> spawn delay to small");
            return;
        }

        _spawnDelay = spawnDelay;
    }

    private async UniTask SpawnLoop()
    {
        await UniTask.WaitForFixedUpdate();
        initialized?.Invoke();

        while (_isSpawning)
        {
            await UniTask.WaitForSeconds(_spawnDelay, cancellationToken: _spawnLoopCancellationTokenSource.Token);

            Vector3 point = new Vector3
                (
                    _originPoint.x + Random.Range(-_range.x, _range.x), 
                    _originPoint.y, 
                    _originPoint.z + Random.Range(-_range.y, _range.y)
                );

            _resourcesFactory.Create(point);
        }
    }
}