public class DronesSpeedModelInstaller : SimulationControllBaseInstaller<DronesSpeedModel, DronesSpawnerModel>
{
    protected override DronesSpeedModel GetModelInstance(DronesSpawnerModel targetSystem)
    {
        return new DronesSpeedModel(targetSystem);
    }
}