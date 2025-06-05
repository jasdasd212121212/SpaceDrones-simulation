using System;
using UnityEngine;
using Zenject;

public class MovementStrategyInstaller : AiComponentInstaller
{
    [SerializeField] private GameObject _strategyGameObject;

    private IEnemyMovable _startegy;

    private void OnValidate()
    {
        if (_strategyGameObject != null)
        {
            if (_strategyGameObject.GetComponent<IEnemyMovable>() == null)
            {
                Debug.LogError($"Critical error -> can`t set {nameof(_strategyGameObject)} because gameObject {_strategyGameObject} are not contains any script realises {nameof(IEnemyMovable)}");
                _strategyGameObject = null;
            }
        }
    }

    protected override Type Install(DiContainer container, out object installed)
    {
        _startegy = _strategyGameObject.GetComponent<IEnemyMovable>();

        container.Bind<IEnemyMovable>().FromInstance(_startegy).AsSingle().NonLazy();

        installed = _startegy;
        return typeof(IEnemyMovable);
    }
}