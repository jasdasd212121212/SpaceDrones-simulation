public class SimulationSpeedPresenter : SimulationControllPresenterBase<SimulationSpeedModel, SimulationTimeSpeedModel, SliderCommonView>
{
    protected override void OnModelInitialized()
    {
        View.SetBorders(Model.Settings.MinTimeSpeed, Model.Settings.MaxTimeSpeed);
        View.SetValue(Model.Settings.DefaultTimeSpeed);
    }

    protected override void OnViewChange()
    {
        Model.SetSimulationSpeed(View.CurrentSliderValue);
    }
}