using TMPro;
using UnityEngine;

public class DroneStateView : MonoBehaviour
{
    [SerializeField] private BehavioursStateMachine _behavioursStateMachine;
    [SerializeField] private TextMeshProUGUI _displayText;

    [Space]

    [SerializeField] private DroneStateViewSettingsNode[] _settings;

    private void Start()
    {
        OnChangeBehaviour();
        _behavioursStateMachine.behaviourChanged += OnChangeBehaviour;
    }

    private void OnDestroy()
    {
        _behavioursStateMachine.behaviourChanged -= OnChangeBehaviour;
    }

    private void OnChangeBehaviour()
    {
        foreach (DroneStateViewSettingsNode node in _settings)
        {
            if (node.BehaviourName == _behavioursStateMachine.CurrentBehaviourName)
            {
                _displayText.text = node.DisplayName;
                return;
            }
        }

        _displayText.text = $"Not found display name for: {_behavioursStateMachine.CurrentBehaviourName}";
    }
}

[System.Serializable]
public class DroneStateViewSettingsNode
{
    [SerializeField] private string _behaviourName;
    [SerializeField] private string _displayName;

    public string BehaviourName => _behaviourName;
    public string DisplayName => _displayName;
}