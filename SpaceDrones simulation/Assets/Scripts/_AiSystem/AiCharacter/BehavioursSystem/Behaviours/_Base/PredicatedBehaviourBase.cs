using System.Linq;
using UnityEngine;
using Zenject;

public abstract class PredicatedBehaviourBase<TSettings> : BehaviourBase<TSettings> where TSettings : PredicatedBehaviourSettingsBase
{
    [Inject] private AiComponentsContainer _aiComponentsContainer;
    [Inject] private MarkebleObjectsPresenter _markebleObjects;

    [Inject] private AiCharacter _aiCharacter;

    private AiPredicateBase[] _predicates;

    private bool _invalidedByNotReacheble;

    protected Transform CachedTransform { get; private set; }
    protected MarkebleObjectsPresenter MarkebleObjectsPresenter => _markebleObjects;
    protected AiComponentsContainer AiComponentsContainer => _aiComponentsContainer;

    private void Start()
    {
        CachedTransform = _aiCharacter.transform;

        _predicates = Settings.Predicates.Select(predicate => predicate.GetInstance()).ToArray();

        foreach (AiPredicateBase predicate in _predicates)
        {
            predicate.Initialize(_aiComponentsContainer, _markebleObjects);
        }

        OnStart();
    }

    public override bool IsCanEnter()
    {
        if (_predicates == null)
        {
            return false;
        }

        if (_invalidedByNotReacheble)
        {
            _invalidedByNotReacheble = false;
            return false;
        }

        foreach (AiPredicateBase predicate in _predicates)
        {
            if (!predicate.IsValid(CachedTransform.position))
            {
                return false;
            }
        }

        return true;
    }

    protected virtual void OnStart() { }

    protected void SetInvalid()
    {
        _invalidedByNotReacheble = true;
    }
}