using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DronesSpawnerModel
{
    private DronesSpawnerSettings _settings;
    private Transform[] _fractionSpawnPoints;

    private GenericFactory<AiCharacter> _characterFactory;
    private List<List<AiCharacter>> _allDronesByFractions = new List<List<AiCharacter>>();

    private bool _isSpawned;
    private int _prioricy = 99;

    public DronesSpawnerSettings Settings => _settings;
    public IReadOnlyList<IReadOnlyList<AiCharacter>> Drones => _allDronesByFractions;

    public event Action spawned;    

    public DronesSpawnerModel(DronesSpawnerSettings settings, Transform[] fractionSpawnPoints)
    {
        _settings = settings;
        _fractionSpawnPoints = fractionSpawnPoints;

        _characterFactory = new GenericFactory<AiCharacter>(settings.DronePrefab);
    }

    public void SetActiveDronesCount(int activeDronesCount)
    {
        if (_isSpawned == false)
        {
            Debug.LogError($"Critical error -> can`t set active count while drones are not spawned");
            return;
        }

        if (activeDronesCount < 1 || activeDronesCount > _settings.MaxDronesCountPerFraction)
        {
            Debug.LogError($"Critical error -> {nameof(activeDronesCount)} outside of corrent bounds: (1, {_settings.MaxDronesCountPerFraction})");
            return;
        }

        DeactivateAll();

        for (int i = 0; i < _allDronesByFractions.Count; i++)
        {
            for (int j = 0; j < activeDronesCount; j++)
            {
                _allDronesByFractions[i][j].gameObject.SetActive(true);
            }
        }
    }

    public void Spawn()
    {
        if (_isSpawned)
        {
            return;
        }

        for (int i = 0; i < _settings.Fractions.Length; i++)
        {
            SpawnFraction(_settings.Fractions[i]);
        }

        _isSpawned = true;
        spawned?.Invoke();
    }

    private void DeactivateAll()
    {
        for (int i = 0; i < _allDronesByFractions.Count; i++)
        {
            for (int j = 0; j < _allDronesByFractions[i].Count; j++)
            {
                _allDronesByFractions[i][j].gameObject.SetActive(false);
            }
        }
    }

    private void SpawnFraction(DroneFractionSettings fraction)
    {
        _allDronesByFractions.Add(new List<AiCharacter>());
        int fractionIndex = _allDronesByFractions.Count - 1;

        for (int i = 0; i < _settings.MaxDronesCountPerFraction; i++)
        {
            Vector3 spawnPoint = _fractionSpawnPoints[fractionIndex].position;

            AiCharacter drone = _characterFactory.Create(_fractionSpawnPoints[fractionIndex]);

            drone.AiComponentsContainer.Pop<DroneFractionModel>().ChangeFraction(fraction);
            drone.AiComponentsContainer.Pop<IEnemyMovable>().SetPrioricy(_prioricy);

            drone.transform.position = new Vector3
                (
                    spawnPoint.x + Random.Range(-_settings.SpawnRadius, _settings.SpawnRadius), 
                    spawnPoint.y,
                    spawnPoint.z + Random.Range(-_settings.SpawnRadius, _settings.SpawnRadius)
                );

            _allDronesByFractions[fractionIndex].Add(drone);
            _prioricy--;

            if (_prioricy < 0)
            {
                _prioricy = 0;
            }
        }
    }
}