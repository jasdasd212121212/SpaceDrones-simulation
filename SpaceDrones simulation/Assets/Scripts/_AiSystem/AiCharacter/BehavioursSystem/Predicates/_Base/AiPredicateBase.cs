using System;
using UnityEngine;

public abstract class AiPredicateBase : ScriptableObject
{
    [NonSerialized] private bool _initialized;

    protected AiComponentsContainer AiComponentsContainer { get; private set; }
    protected MarkebleObjectsPresenter MarkebleObjectsPresenter { get; private set; }

    public void Initialize(AiComponentsContainer aiComponentsContainer, MarkebleObjectsPresenter markebleObjects)
    {
        if (_initialized)
        {
            return;
        }

        AiComponentsContainer = aiComponentsContainer;
        MarkebleObjectsPresenter = markebleObjects;

        _initialized = true;
    }

    public abstract bool IsValid(Vector3 selfPosition);
    
    public AiPredicateBase GetInstance()
    {
        return Instantiate(this);
    }
}