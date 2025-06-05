public class DronesSpeedPresenter : SimulationControllPresenterBase<DronesSpeedModel, DronesSpawnerModel, SliderCommonView>
{
    protected override void OnModelInitialized()
    {
        View.SetBorders(Model.DronesMinimalSpeed, Model.DronesMaximalSpeed);
        View.SetValue(Model.DronesMaximalSpeed);
    }

    protected override void OnViewChange()
    {
        Model.SetSpeed(View.CurrentSliderValue);
    }
}