using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class DecimalInputFieldCommonView : SimulationControllBaseView
{
    private TMP_InputField _inputField;

    private float _min;
    private float _max;

    public float Value => Mathf.Clamp(float.Parse(_inputField.text), _min, _max);

    private void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();

        _inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
        _inputField.onValueChanged.AddListener(OnValueChanged);
        _inputField.onEndEdit.AddListener(OnEndEdit);
    }

    private void OnDestroy()
    {
        _inputField.onValueChanged.RemoveListener(OnValueChanged);
        _inputField.onEndEdit.RemoveListener(OnEndEdit);
    }

    public void SetBorders(float min, float max)
    {
        if (min > max)
        {
            Debug.LogError($"Critical error -> min value can`t be > max");
            return;
        }

        _min = min;
        _max = max;
    }

    public void SetValue(float value)
    {
        _inputField.text = Mathf.Clamp(value, _min, _max).ToString();
    }

    private void OnValueChanged(string value)
    {
       // _inputField.text = Value.ToString();
    }

    private void OnEndEdit(string value)
    {
        CallDataChangedEvent();
    }
}