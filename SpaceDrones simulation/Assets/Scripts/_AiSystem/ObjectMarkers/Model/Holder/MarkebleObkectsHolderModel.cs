using System.Collections.Generic;
using UnityEngine;

public class MarkebleObkectsHolderModel
{
    private List<MarkebleObject> _objects = new List<MarkebleObject>();

    public IReadOnlyList<MarkebleObject> Objects => _objects;

    public void Register(MarkebleObject obj)
    {
        if (_objects.Contains(obj))
        {
            Debug.LogError($"Critical error -> can`t register object twice");
            return;
        }

        _objects.Add(obj);
    }

    public void Unregister(MarkebleObject obj)
    {
        if (!_objects.Contains(obj))
        {
            Debug.LogError($"Critical error -> can`t unregister not registred object");
            return;
        }

        _objects.Remove(obj);
    }
}