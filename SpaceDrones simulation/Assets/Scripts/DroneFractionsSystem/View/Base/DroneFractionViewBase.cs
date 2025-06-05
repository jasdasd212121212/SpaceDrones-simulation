using UnityEngine;
using Zenject;

public abstract class DroneFractionViewBase : MonoBehaviour
{
    [Inject] private DroneFractionModel _model;

    private void Start()
    {
        OnChangeFraction();
        _model.fractionChanged += OnChangeFraction;
    }

    private void OnDestroy()
    {
        _model.fractionChanged -= OnChangeFraction;
    }

    private void OnChangeFraction()
    {
        Display(_model.CurrentFraction);
    }

    protected abstract void Display(DroneFractionSettings fraction);
}