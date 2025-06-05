using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Slider))]
public class SliderCommonView : SimulationControllBaseView
{
    private Slider _slider;

    public float CurrentSliderValue => _slider.value;
    public int CurrentSliderWholeValue => Mathf.RoundToInt(CurrentSliderValue);

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(OnSliderChangeValue);
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveListener(OnSliderChangeValue);
    }

    public void SetBorders(float min, float max)
    {
        if (min > max)
        {
            Debug.LogError($"Critical error -> min value can`t be > max");
            return;
        }

        _slider.minValue = min;
        _slider.maxValue = max;
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }

    private void OnSliderChangeValue(float currentValue)
    {
        CallDataChangedEvent();
    }
}