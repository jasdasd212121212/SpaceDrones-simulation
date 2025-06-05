using UnityEngine;

public interface IEnemyMovable
{
    float Speed { get; }
    float MinSpeed { get; }
    public void MoveTo(Vector3 target, float speed);
    public void SetPosition(Vector3 target, float speed);
    public void BorderSpeed(float speed);
    public void SetPrioricy(int prioricy);
    public void Stop();
}