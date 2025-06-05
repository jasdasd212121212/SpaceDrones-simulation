using UnityEngine;

public class ResourcesTakingParticlesView : ResourcesViewBase
{
    [SerializeField] private ParticleSystem _takingParticles;

    protected override void OnStartTaking()
    {
        _takingParticles.Play();
    }

    protected override void OnEndTaking()
    {
        _takingParticles.Stop();
    }
}