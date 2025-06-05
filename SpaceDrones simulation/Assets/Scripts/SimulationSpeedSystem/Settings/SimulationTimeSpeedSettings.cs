using UnityEngine;

[CreateAssetMenu(fileName = "SimulationTimeSpeedSettings", menuName = "Game design/SimulationTimeSpeed")]
public class SimulationTimeSpeedSettings : ScriptableObject
{
    [SerializeField][Min(0.0001f)] private float _defaultTimeSpeed = 1;
    [SerializeField][Min(0.0001f)] private float _minTimeSpeed = 0.1f;
    [SerializeField][Min(0.0001f)] private float _maxTimeSpeed = 10f;

    public float DefaultTimeSpeed => _defaultTimeSpeed;
    public float MinTimeSpeed => _minTimeSpeed;
    public float MaxTimeSpeed => _maxTimeSpeed;

    private void OnValidate()
    {
        _defaultTimeSpeed = Mathf.Clamp(_defaultTimeSpeed, _minTimeSpeed, _maxTimeSpeed);

        _minTimeSpeed = Mathf.Clamp(_minTimeSpeed, 0.1f, _maxTimeSpeed);
        _maxTimeSpeed = Mathf.Clamp(_maxTimeSpeed, _minTimeSpeed, 100f);
    }
}