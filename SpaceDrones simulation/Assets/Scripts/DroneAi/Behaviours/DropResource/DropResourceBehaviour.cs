public class DropResourceBehaviour : PredicatedBehaviourBase<DropResourceBehaviourSettings>
{
    public override void Enter()
    {
        FractionBaseMark fractionBase = 
            MarkebleObjectsPresenter.FindNearestObject
            (
                CachedTransform.position, 
                AiComponentsContainer.Pop<DroneFractionModel>().CurrentFraction.FractionBaseMark
            ) as FractionBaseMark;

        if (fractionBase != null)
        {
            fractionBase.CollectingResourcesPresenter.LoadOut(AiComponentsContainer.Pop<ResourcesModel>());
        }
    }
}