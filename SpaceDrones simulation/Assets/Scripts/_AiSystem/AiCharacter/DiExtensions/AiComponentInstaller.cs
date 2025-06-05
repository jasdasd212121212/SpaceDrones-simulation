using System;
using UnityEngine;
using Zenject;

public abstract class AiComponentInstaller : MonoBehaviour
{
    private Type _installedServiceType;
    private AiComponentsContainer _aiComponentsContainer;

    public void InstallBindings(DiContainer container, AiComponentsContainer aiComponentsContainer)
    {
        _aiComponentsContainer = aiComponentsContainer;
        
        _installedServiceType = Install(container, out object installed);
        aiComponentsContainer.Bind(_installedServiceType, installed);
    }

    private void OnDestroy()
    {
        _aiComponentsContainer.Remove(_installedServiceType);
    }

    protected abstract Type Install(DiContainer container, out object installed); 
}