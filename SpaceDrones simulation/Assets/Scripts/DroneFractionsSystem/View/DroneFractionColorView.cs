using UnityEngine;

public class DroneFractionColorView : DroneFractionViewBase
{
    [SerializeField] private DroneFractionColorViewSettingsNode[] _settings;
    [SerializeField] private MeshRenderer _meshRenderer;

    protected override void Display(DroneFractionSettings fraction)
    {
        foreach (DroneFractionColorViewSettingsNode node in _settings)
        {
            if (node.Fraction == fraction)
            {
                _meshRenderer.material = node.BodyMaterial;
            }
        }
    }
}

[System.Serializable]
public class DroneFractionColorViewSettingsNode
{
    [SerializeField] private DroneFractionSettings _fraction;
    [SerializeField] private Material _bodyMaterial;

    public DroneFractionSettings Fraction => _fraction;
    public Material BodyMaterial => _bodyMaterial;
}