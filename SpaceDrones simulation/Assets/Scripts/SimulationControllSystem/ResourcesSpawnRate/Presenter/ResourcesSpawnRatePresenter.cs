using UnityEngine;

public class ResourcesSpawnRatePresenter : SimulationControllPresenterBase<ResourcesSpawnRateModel, ResourcesSpawnerModel, DecimalInputFieldCommonView>
{
    protected override void OnModelInitialized()
    {
        View.SetBorders(Model.MinimalSpawnRate, float.MaxValue);
        View.SetValue(Model.CurrentSpawnRate);
    }

    protected override void OnViewChange()
    {
        Model.SetSpawnRate(View.Value);
    }
}