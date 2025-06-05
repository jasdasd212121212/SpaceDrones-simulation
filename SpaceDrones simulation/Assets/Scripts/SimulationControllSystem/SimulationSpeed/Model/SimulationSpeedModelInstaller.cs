public class SimulationSpeedModelInstaller : SimulationControllBaseInstaller<SimulationSpeedModel, SimulationTimeSpeedModel>
{
    protected override SimulationSpeedModel GetModelInstance(SimulationTimeSpeedModel targetSystem)
    {
        return new SimulationSpeedModel(targetSystem);
    }
}