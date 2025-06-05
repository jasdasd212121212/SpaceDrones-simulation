using UnityEngine;
using Zenject;

public class AiComponentsInstaller : MonoInstaller
{
    [SerializeField] private AiCharacter _selfCharacter;

    [Space]

    [SerializeField] private AiComponentInstaller[] _aiComponentInstallers;

    public override void InstallBindings()
    {
        AiComponentsContainer aiComponentsContainer = new AiComponentsContainer();

        Container.Bind<AiComponentsContainer>().FromInstance(aiComponentsContainer).AsSingle().NonLazy();
        Container.Bind<AiCharacter>().FromInstance(_selfCharacter).AsSingle().NonLazy();

        foreach (AiComponentInstaller installer in _aiComponentInstallers)
        {
            installer.InstallBindings(Container, aiComponentsContainer);
        }

        _selfCharacter.Initialize(aiComponentsContainer);
    }
}