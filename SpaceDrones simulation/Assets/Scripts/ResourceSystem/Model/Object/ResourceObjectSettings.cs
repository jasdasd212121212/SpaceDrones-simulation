using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Game design/Resources/CollectebleSettings")]
public class ResourceObjectSettings : ScriptableObject
{
    [SerializeField][Min(1)] private int _cost = 1;
    [SerializeField] private float _collectingTime = 2f;

    public int Cost => _cost;
    public float CollectingTime => _collectingTime;
}