using UnityEngine;
using Zenject;

public class MarkebleObject : MonoBehaviour
{
    [SerializeField] private ObjectMark _mark;

    [Inject] private MarkebleObkectsHolderModel _holder;

    public ObjectMark Mark => _mark;
    public Transform CachedTransform { get; private set; }

    private void OnValidate()
    {
        if (Application.isPlaying == true)
        {
            return;
        }

        if (GetComponent<ZenAutoInjecter>() == null)
        {
            gameObject.AddComponent<ZenAutoInjecter>();
        }
    }

    private void Awake()
    {
        CachedTransform = transform;
        _holder.Register(this);
    }

    private void OnDestroy()
    {
        _holder.Unregister(this);
    }
}