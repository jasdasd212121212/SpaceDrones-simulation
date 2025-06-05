using UnityEngine;

[CreateAssetMenu(fileName = "DroneFraction", menuName = "Ai/Fractions/Drone")]
public class DroneFractionSettings : ScriptableObject
{
    [SerializeField] private ObjectMark _fractionBaseMark;

    public ObjectMark FractionBaseMark => _fractionBaseMark;
}