public class SimulationSpeedModel : SimulationControllBaseModel<SimulationTimeSpeedModel>
{
    public SimulationTimeSpeedSettings Settings => TargetSystem.Settings;

    public SimulationSpeedModel(SimulationTimeSpeedModel targetSystem) : base(targetSystem)
    {
        TargetSystem.initialized += CallModelInitializationEvent;
    }

    ~SimulationSpeedModel()
    {
        if (TargetSystem != null)
        {
            TargetSystem.initialized -= CallModelInitializationEvent;
        }
    }

    public void SetSimulationSpeed(float speed)
    {
        TargetSystem.SetTimeSpeed(speed);
    }
}