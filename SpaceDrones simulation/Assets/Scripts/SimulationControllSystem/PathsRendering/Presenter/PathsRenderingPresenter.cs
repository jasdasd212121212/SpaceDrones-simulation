public class PathsRenderingPresenter : SimulationControllPresenterBase<PathsRenderingModel, DronesSpawnerModel, CheckboxCommonView>
{
    protected override void OnModelInitialized()
    {
        View.SetState(Model.DefaultPathsActivity);
    }

    protected override void OnViewChange()
    {
        Model.SetPathsActive(View.IsOn);
    }
}