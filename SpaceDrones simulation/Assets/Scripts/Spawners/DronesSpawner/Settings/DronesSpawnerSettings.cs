using UnityEngine;

[CreateAssetMenu(fileName = "DronesSpawnerSettings", menuName = "Game design/DronesSpawner")]
public class DronesSpawnerSettings : ScriptableObject
{
    [SerializeField] private AiCharacter _dronePrefab;

    [Space]

    [SerializeField][Min(1)] private int _maximalDronesCountPerFraction = 5;
    [SerializeField][Min(0.01f)] private float _spawnRadius = 2f;

    [Space]

    [SerializeField] private DroneFractionSettings[] _fractions;

    public AiCharacter DronePrefab => _dronePrefab;
    public int MaxDronesCountPerFraction => _maximalDronesCountPerFraction;
    public float SpawnRadius => _spawnRadius;
    public DroneFractionSettings[] Fractions => _fractions;
}