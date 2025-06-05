public class PathsRenderingModelInstaller : SimulationControllBaseInstaller<PathsRenderingModel, DronesSpawnerModel>
{
    protected override PathsRenderingModel GetModelInstance(DronesSpawnerModel targetSystem)
    {
        return new PathsRenderingModel(targetSystem);
    }
}