using UnityEngine;

public class GenericFactory<TObject> where TObject : MonoBehaviour
{
    private TObject _prefab;
    private Transform _parent;

    public GenericFactory(TObject prefab)
    {
        _prefab = prefab;
    }

    public GenericFactory(Transform parent)
    {
        _parent = parent;
    }

    public GenericFactory(TObject prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public TObject Create()
    {
        if (_parent == null || _parent == null)
        {
            Debug.LogError($"Invalid data");
            return default;
        }

        return Create(_prefab, _parent);
    }
    
    public TObject Create(TObject prefab)
    {
        if (_parent == null)
        {
            Debug.LogError("Invalid data");
            return default;
        }

        return Create(prefab, _parent);
    }

    public TObject Create(Transform parent)
    {
        return GameObject.Instantiate(_prefab, parent);
    }

    public TObject Create(TObject prefab, Transform parent)
    {
        return GameObject.Instantiate(prefab, parent);
    }

    public TObject Create(Vector3 position)
    {
        return GameObject.Instantiate(_prefab, position, Quaternion.identity);
    }
}