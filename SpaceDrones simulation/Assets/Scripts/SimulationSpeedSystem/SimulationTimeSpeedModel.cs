using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class SimulationTimeSpeedModel
{
    public SimulationTimeSpeedSettings Settings { get; private set; }

    public event Action initialized;

    public SimulationTimeSpeedModel(SimulationTimeSpeedSettings settings)
    {
        Settings = settings;

        Initialize().Forget();
    }

    public void SetTimeSpeed(float speed)
    {
        Time.timeScale = Mathf.Clamp(speed, Settings.MinTimeSpeed, Settings.MaxTimeSpeed);
    }

    private async UniTask Initialize()
    {
        await UniTask.WaitForFixedUpdate();

        SetTimeSpeed(Settings.DefaultTimeSpeed);
        initialized?.Invoke();
    }
}