using UnityEngine;

public class FractionBaseMark : MarkebleObject
{
    [SerializeField] private CollectingFromAiResourcesPresenter _collectingPresenter;

    public CollectingFromAiResourcesPresenter CollectingResourcesPresenter => _collectingPresenter;
}