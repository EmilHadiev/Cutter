using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(TriggerObserver))]
[RequireComponent(typeof(TrapAnimator))]
public class Trap : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;

    private const int Damage = 1;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
    }

    private void OnEnable()
    {
        _observer.Entered += OnPlayerEntered;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnPlayerEntered;
    }

    private void OnPlayerEntered(Collider collider)
    {
        if (collider.TryGetComponent(out IHealth health))
            health.TakeDamage(Damage);
    }
}