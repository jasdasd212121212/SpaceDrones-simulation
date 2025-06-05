using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemyMoverStrategy : MonoBehaviour, IEnemyMovable
{
    [SerializeField] private NavMeshAgent _agent;

    private Transform _cachedTransform;

    private float _maximalSpeed;

    public float Speed => _agent.speed;
    public float MinSpeed => 0.01f;

    private void Awake()
    {
        _maximalSpeed = int.MaxValue;
        _cachedTransform = transform;
    }

    public void MoveTo(Vector3 target, float speed)
    {
        _agent.isStopped = false;
        _agent.speed = GetSpeed(speed);

        _agent.SetDestination(target);
    }

    public void BorderSpeed(float speed)
    {
        if (speed < MinSpeed)
        {
            Debug.LogError($"Critical error -> can`t set too low move speed");
            return;
        }
        
        _maximalSpeed = speed;
        _agent.speed = speed;
    }

    public void SetPosition(Vector3 target, float speed)
    {
        _cachedTransform.position = Vector3.Lerp(_cachedTransform.position, target, GetSpeed(speed));
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    private float GetSpeed(float speed) => Mathf.Clamp(speed, 0, _maximalSpeed);

    public void SetPrioricy(int prioricy)
    {
        _agent.avoidancePriority = prioricy;
    }
}