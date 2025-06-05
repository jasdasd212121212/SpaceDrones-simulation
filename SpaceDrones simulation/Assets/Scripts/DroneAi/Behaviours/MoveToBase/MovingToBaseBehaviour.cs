public class MovingToBaseBehaviour : MoveBehaviour
{
    private DroneFractionModel _fraction;

    protected override void OnStart()
    {
        _fraction = AiComponentsContainer.Pop<DroneFractionModel>();
    }

    protected override ObjectMark GetObjectMark()
    {
        return _fraction.CurrentFraction.FractionBaseMark;
    }
}