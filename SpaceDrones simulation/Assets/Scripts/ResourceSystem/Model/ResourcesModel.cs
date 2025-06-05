using System;
using UnityEngine;

public class ResourcesModel
{
    public int ResourcesCount { get; private set; }
    public Guid ResourceBookerId { get; private set; }

    public event Action resourcesChanged;
    public event Action takingStarted;
    public event Action takingCompleted;

    public ResourcesModel()
    {
        ResourceBookerId = Guid.NewGuid();
    }

    public void Evaluate(int count)
    {
        if (count < 0)
        {
            Debug.LogError($"Critical error -> can`t evalueate resources with {nameof(count)} < 0");
            return;
        }

        ResourcesCount += count;

        resourcesChanged?.Invoke();
    }

    public void StartTaking()
    {
        takingStarted?.Invoke();
    }

    public void ResetAll()
    {
        ResourcesCount = 0;

        resourcesChanged?.Invoke();
        takingCompleted?.Invoke();
    }
}