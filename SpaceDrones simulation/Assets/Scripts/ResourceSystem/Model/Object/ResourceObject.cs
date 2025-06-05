using System;
using UnityEngine;

public class ResourceObject : MarkebleObject
{
    [SerializeField] private ResourceObjectSettings _settings;

    public ResourceObjectSettings Settings => _settings;
    public Guid BookerId { get; private set; } = default;
    public bool IsCollecting { get; private set; }

    public event Action collectingStarted;

    public void StartCollecting()
    {
        if (IsCollecting)
        {
            return;
        }

        IsCollecting = true;
        collectingStarted?.Invoke();
    }

    public void BookResource(Guid bookerId)
    {
        if (BookerId != default)
        {
            return;
        }

        BookerId = bookerId;
    }
}