using TMPro;
using UnityEngine;

public class ResourcesCountView : ResourcesViewBase
{
    [SerializeField] private TextMeshProUGUI _displayText;

    protected override void DisplayResourcesCount(int resourcesCount)
    {
        _displayText.text = resourcesCount.ToString();
    }
}