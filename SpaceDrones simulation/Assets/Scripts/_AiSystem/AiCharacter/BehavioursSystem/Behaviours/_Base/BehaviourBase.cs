using UnityEngine;
using Zenject;

public abstract class BehaviourBase<TSettings> : BehaviourBaseGeneric where TSettings : BehaviourSettingsBase
{
    [SerializeField] private TSettings _settings;

    [HideInInspector][SerializeField] private bool _isSettedUp;

    protected TSettings Settings => _settings;

    protected void OnValidate()
    {
        if (_settings == null)
        {
            Debug.LogWarning($"Validation warning -> {nameof(_settings)} can`t be = null");
        }
        else if (_settings.AssociatedProviderType != GetType())
        {
            Debug.LogError($"Validation error -> wrong associated type of settings: {_settings.name} and Provider: {GetType()} on GameObject: {gameObject.name}");
            _settings = null;
        }

        OnValidated();
    }

    [Inject]
    private void Construct()
    {
        OnConstruct();
    }

    protected virtual void OnValidated() { }
    protected virtual void OnConstruct() { }

    public override BehaviourSettingsBase GetSettings()
    {
        return _settings;
    }

    public override void SetSettings(BehaviourSettingsBase settings)
    {
        _settings = settings as TSettings;
    }
}