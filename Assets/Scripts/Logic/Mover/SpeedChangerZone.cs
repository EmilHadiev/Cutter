using UnityEngine;

[RequireComponent(typeof(TriggerObserver))]
public class SpeedChangerZone : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;
    [SerializeField] private float _speed = 1;

    private void OnValidate()
    {
        _observer ??= GetComponent<TriggerObserver>();
    }

    private void OnEnable()
    {
        _observer.Entered += PlayerEntered;
        _observer.Exited += PlayerExited;
    }

    private void OnDisable()
    {
        _observer.Entered -= PlayerEntered;
        _observer.Exited -= PlayerExited;
    }

    private void PlayerEntered(Collider collider)
    {
        if (collider.TryGetComponent(out ISpeedChangable speed))
            speed.SpeedUp(_speed);
    }

    private void PlayerExited(Collider collider)
    {
        if (collider.TryGetComponent(out ISpeedChangable speed))
            speed.SetDefaultSpeed();
    } 
}