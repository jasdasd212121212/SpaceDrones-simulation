using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Toggle))]
public class CheckboxCommonView : SimulationControllBaseView
{
    private Toggle _toggle;

    public bool IsOn => _toggle.isOn;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _toggle.onValueChanged.AddListener(OnToggleClicked);
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleClicked);
    }

    public void SetState(bool isOn)
    {
        _toggle.isOn = isOn;
    }

    private void OnToggleClicked(bool value)
    {
        CallDataChangedEvent();
    }
}