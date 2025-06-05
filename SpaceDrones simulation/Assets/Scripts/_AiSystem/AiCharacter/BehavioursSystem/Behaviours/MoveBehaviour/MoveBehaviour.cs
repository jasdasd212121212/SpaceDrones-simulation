using UnityEngine;
using Zenject;

public class MoveBehaviour : PredicatedBehaviourBase<MoveBehaviourSettings>
{
    [Inject] private IEnemyMovable _startegy;

    protected override void OnConstruct()
    {
        _startegy.BorderSpeed(Settings.ForwardSpeed);
    }

    public override void FixedTick()
    {
        MarkebleObject obj = GetTargetObject();

        if (obj == null)
        {
            SetInvalid();
            return;
        }

        _startegy.MoveTo(obj.CachedTransform.position, Settings.ForwardSpeed);
    }

    public override void Exit()
    {
        _startegy.Stop();
    }

    protected virtual ObjectMark GetObjectMark()
    {
        return Settings.Mark;
    }

    protected virtual MarkebleObject GetTargetObject()
    {
        return MarkebleObjectsPresenter.FindNearestObject(CachedTransform.position, GetObjectMark());
    }
}