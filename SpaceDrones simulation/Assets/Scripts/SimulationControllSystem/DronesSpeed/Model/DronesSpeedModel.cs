public class DronesSpeedModel : SimulationControllBaseModel<DronesSpawnerModel>
{
    public float DronesMaximalSpeed { get; private set; }
    public float DronesMinimalSpeed { get; private set; }

    public DronesSpeedModel(DronesSpawnerModel targetSystem) : base(targetSystem)
    {
        TargetSystem.spawned += OnDronesSpawned;
    }

    private void OnDronesSpawned()
    {
        IEnemyMovable mover = TargetSystem.Drones[0][0].AiComponentsContainer.Pop<IEnemyMovable>();

        DronesMaximalSpeed = mover.Speed;
        DronesMinimalSpeed = mover.MinSpeed;

        CallModelInitializationEvent();
    }

    public void SetSpeed(float speed)
    {
        for (int i = 0; i < TargetSystem.Drones.Count; i++)
        {
            for (int j = 0; j < TargetSystem.Drones[i].Count; j++)
            {
                TargetSystem.Drones[i][j].AiComponentsContainer.Pop<IEnemyMovable>().BorderSpeed(speed);
            }
        }
    }
}