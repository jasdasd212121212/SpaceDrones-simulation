public class DronesCountModel : SimulationControllBaseModel<DronesSpawnerModel>
{
    public int MinDronesCount => 1;
    public int MaxDronesCount => TargetSystem.Settings.MaxDronesCountPerFraction;

    public DronesCountModel(DronesSpawnerModel targetSystem) : base(targetSystem)
    {
        TargetSystem.spawned += CallModelInitializationEvent;
    }

    ~DronesCountModel()
    {
        if (TargetSystem != null)
        {
            TargetSystem.spawned -= CallModelInitializationEvent;
        }
    }

    public void SetCount(int count)
    {
        TargetSystem.SetActiveDronesCount(count);
    }
}