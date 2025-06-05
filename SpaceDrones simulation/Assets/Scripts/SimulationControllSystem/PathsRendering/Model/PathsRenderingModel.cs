public class PathsRenderingModel : SimulationControllBaseModel<DronesSpawnerModel>
{
    public bool DefaultPathsActivity { get; private set; }

    public PathsRenderingModel(DronesSpawnerModel targetSystem) : base(targetSystem)
    {
        TargetSystem.spawned += OnDronesSpawned;
    }

    ~PathsRenderingModel()
    {
        if (TargetSystem != null)
        {
            TargetSystem.spawned -= OnDronesSpawned;
        }
    }

    public void SetPathsActive(bool active)
    {
        for (int i = 0; i < TargetSystem.Drones.Count; i++)
        {
            for (int j = 0; j < TargetSystem.Drones[i].Count; j++)
            {
                TargetSystem.Drones[i][j].AiComponentsContainer.Pop<PathRenderingView>().SetViewActive(active);
            }
        }
    }

    private void OnDronesSpawned()
    {
        DefaultPathsActivity = TargetSystem.Drones[0][0].AiComponentsContainer.Pop<PathRenderingView>().IsActive;
        CallModelInitializationEvent();
    }
}