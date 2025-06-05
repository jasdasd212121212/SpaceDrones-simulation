public class DronesCountPresenter : SimulationControllPresenterBase<DronesCountModel, DronesSpawnerModel, SliderCommonView>
{
    protected override void OnModelInitialized()
    {
        View.SetBorders(Model.MinDronesCount, Model.MaxDronesCount);
        View.SetValue(Model.MaxDronesCount);
    }

    protected override void OnViewChange()
    {
        Model.SetCount(View.CurrentSliderWholeValue);
    }
}