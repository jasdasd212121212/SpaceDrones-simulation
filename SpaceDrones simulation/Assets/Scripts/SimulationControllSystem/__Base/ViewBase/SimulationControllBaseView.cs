using System;
using UnityEngine;

public abstract class SimulationControllBaseView : MonoBehaviour
{
    public event Action dataChanged;
    
    protected void CallDataChangedEvent() => dataChanged?.Invoke();
}