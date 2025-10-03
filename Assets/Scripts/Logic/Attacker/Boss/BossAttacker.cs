using UnityEngine;

[RequireComponent(typeof(BossProjectileSpawner))]
public class BossAttacker : MonoBehaviour, IAttackable
{
    [SerializeField] private TriggerObserver _observer;

    private IBossAnimator _animator;

    private void Awake()
    {
        Boss boss = GetComponent<Boss>();
        _animator = boss.Animator;
    }

    private void OnEnable()
    {
        _observer.Entered += OnPlayerEntered;
        _observer.Exited += OnPlayerExited;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnPlayerEntered;
        _observer.Exited -= OnPlayerExited;
    }

    public void StartAttack()
    {
        _animator.PlayAttack();
    }

    public void StopAttack()
    {
        _animator.StopAttack();
    }

    private void OnPlayerEntered(Collider collider) => StartAttack();
    private void OnPlayerExited(Collider collider) => StopAttack();
}