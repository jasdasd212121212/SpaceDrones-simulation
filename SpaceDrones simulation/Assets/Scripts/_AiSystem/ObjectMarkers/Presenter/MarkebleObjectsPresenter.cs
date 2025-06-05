using System;
using System.Collections.Generic;
using UnityEngine;

public class MarkebleObjectsPresenter
{
    private MarkebleObkectsHolderModel _model;

    public MarkebleObjectsPresenter(MarkebleObkectsHolderModel model)
    {
        _model = model;
    }

    public MarkebleObject[] FindByMark(ObjectMark mark)
    {
        List<MarkebleObject> finded = new List<MarkebleObject>();

        foreach (MarkebleObject obj in _model.Objects)
        {
            if (obj.Mark == mark)
            {
                finded.Add(obj);
            }
        }

        return finded.ToArray();
    }

    public MarkebleObject FindNearestObject(Vector3 selfPosition, ObjectMark objectMark)
    {
        return FindNearestObject(selfPosition, objectMark, (obj) => true);
    }

    public MarkebleObject FindNearestObject(Vector3 selfPosition, ObjectMark objectMark, Predicate<MarkebleObject> isValidObjectPredicate)
    {
        MarkebleObject[] markebleObjects = FindByMark(objectMark); 

        MarkebleObject nearestObject = null;
        float minimalDistance = float.MaxValue;

        foreach (MarkebleObject obj in markebleObjects)
        {
            float distance = Vector3.Distance(selfPosition, obj.CachedTransform.position);

            if (distance < minimalDistance && isValidObjectPredicate(obj))
            {
                nearestObject = obj;
                minimalDistance = distance;
            }
        }

        return nearestObject;
    }
}