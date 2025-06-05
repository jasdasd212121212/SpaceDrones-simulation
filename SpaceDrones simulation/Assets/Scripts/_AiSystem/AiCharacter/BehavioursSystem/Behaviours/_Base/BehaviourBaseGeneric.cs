using UnityEngine;

public abstract class BehaviourBaseGeneric : MonoBehaviour
{
    public abstract BehaviourSettingsBase GetSettings();
    public abstract void SetSettings(BehaviourSettingsBase settings);
    public abstract bool IsCanEnter();

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
    public virtual void FixedTick() { }
}