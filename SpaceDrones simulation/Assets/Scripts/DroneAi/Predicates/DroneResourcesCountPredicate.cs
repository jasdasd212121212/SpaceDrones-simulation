using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesPredicate", menuName = "Ai/Predicates/Resources")]
public class DroneResourcesCountPredicate : AiPredicateBase
{
    [SerializeField] private int _minResourcesToActivation;
    [SerializeField] private int _maxResourcesToActivation;

    private void OnValidate()
    {
        _minResourcesToActivation = Mathf.Clamp(_minResourcesToActivation, 0, _maxResourcesToActivation);
        _maxResourcesToActivation = Mathf.Clamp(_maxResourcesToActivation, _minResourcesToActivation, int.MaxValue);
    }

    public override bool IsValid(Vector3 selfPosition)
    {
        int currentResources = AiComponentsContainer.Pop<ResourcesModel>().ResourcesCount;

        return currentResources >= _minResourcesToActivation && currentResources < _maxResourcesToActivation;
    }
}