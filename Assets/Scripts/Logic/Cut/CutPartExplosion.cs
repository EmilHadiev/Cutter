using System;
using UnityEngine;
using Zenject;

public class CutPartExplosion : IInitializable, IDisposable
{
    private const int ExplosionForce = 50;
    private const int ExplosionRadius = 1;

    private readonly ICutPartContainer _container;

    public CutPartExplosion(ICutPartContainer cutPartContainer)
    {
        _container = cutPartContainer;
    }

    public void Initialize()
    {
        _container.Added += OnCutPartAdded;
    }

    public void Dispose()
    {
        _container.Added -= OnCutPartAdded;
    }

    private void OnCutPartAdded(GameObject cutPart)
    {
        if (cutPart.TryGetComponent(out Rigidbody rb))
            rb.AddExplosionForce(ExplosionForce, cutPart.transform.position, ExplosionRadius, 0, ForceMode.Impulse);
    }
}