using System;
using UnityEngine;

public class DroneFractionModel
{
    public DroneFractionSettings CurrentFraction { get; private set; }

    public event Action fractionChanged;

    public void ChangeFraction(DroneFractionSettings fraction)
    {
        if (fraction == null)
        {
            Debug.LogError($"Critical error -> can`t set null fraction");
            return;
        }

        CurrentFraction = fraction;

        fractionChanged?.Invoke();
    }
}