public class DronesCountModelInstaller : SimulationControllBaseInstaller<DronesCountModel, DronesSpawnerModel>
{
    protected override DronesCountModel GetModelInstance(DronesSpawnerModel targetSystem)
    {
        return new DronesCountModel(targetSystem);
    }
}