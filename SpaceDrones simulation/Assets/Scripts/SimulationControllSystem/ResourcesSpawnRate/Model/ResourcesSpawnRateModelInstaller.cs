public class ResourcesSpawnRateModelInstaller : SimulationControllBaseInstaller<ResourcesSpawnRateModel, ResourcesSpawnerModel>
{
    protected override ResourcesSpawnRateModel GetModelInstance(ResourcesSpawnerModel targetSystem)
    {
        return new ResourcesSpawnRateModel(targetSystem);
    }
}