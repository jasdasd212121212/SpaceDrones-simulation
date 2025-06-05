public class ResourcesSpawnRateModel : SimulationControllBaseModel<ResourcesSpawnerModel>
{
    public float MinimalSpawnRate => TargetSystem.MinSpawnDelay;
    public float CurrentSpawnRate => TargetSystem.SpawnDelay;

    public ResourcesSpawnRateModel(ResourcesSpawnerModel targetSystem) : base(targetSystem)
    {
        TargetSystem.initialized += CallModelInitializationEvent;
    }

    ~ResourcesSpawnRateModel()
    {
        if (TargetSystem != null)
        {
            TargetSystem.initialized -= CallModelInitializationEvent;
        }
    }

    public void SetSpawnRate(float spawnRate)
    {
        TargetSystem.SetSpawnDelay(spawnRate);
    }
}