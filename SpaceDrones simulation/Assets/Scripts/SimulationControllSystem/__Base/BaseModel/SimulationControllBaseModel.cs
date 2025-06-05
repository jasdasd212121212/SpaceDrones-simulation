using System;

public abstract class SimulationControllBaseModel<TTargetSystem> where TTargetSystem : class
{
    protected TTargetSystem TargetSystem { get; private set; }

    public event Action modelInitialized;

    public SimulationControllBaseModel(TTargetSystem targetSystem)
    {
        TargetSystem = targetSystem;
    }

    protected void CallModelInitializationEvent() => modelInitialized?.Invoke();
}