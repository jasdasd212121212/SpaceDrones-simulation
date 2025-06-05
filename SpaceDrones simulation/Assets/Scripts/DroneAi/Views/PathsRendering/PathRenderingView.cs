using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
public class PathRenderingView : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private LineRenderer _lineRenderer;

    private bool _isActive;

    public bool IsActive => _isActive;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        _lineRenderer.enabled = true;
        _lineRenderer.SetPositions(new Vector3[0]);
    }

    private void OnDisable()
    {
        _lineRenderer.enabled = false;
    }

    public void SetViewActive(bool isActive)
    {
        _isActive = isActive;
    }

    private void FixedUpdate()
    {
        if (_agent.hasPath && _isActive)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPositions(_agent.path.corners);
        }
        else
        {
            _lineRenderer.enabled = false;
        }
    }
}