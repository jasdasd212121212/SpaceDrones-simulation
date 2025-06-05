using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesSpawnerSettings", menuName = "Game design/Resources/ResourcesSpawnerModel")]
public class ResourcesSpawnerSettings : ScriptableObject
{
    [SerializeField] private ResourceObject _resourcePrefab;
    [SerializeField][Min(0.01f)] private float _initialSpawnDelay = 1f;
    [SerializeField] private Vector2 _spawnRange;
    
    public ResourceObject ResourceObject => _resourcePrefab;
    public float InitialSpawnDelay => _initialSpawnDelay;
    public Vector2 SpawnRange => _spawnRange;

    private void OnValidate()
    {
        _spawnRange = new Vector2(Mathf.Abs(_spawnRange.x), Mathf.Abs(_spawnRange.y));
    }
}