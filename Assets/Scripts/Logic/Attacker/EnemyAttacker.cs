using System;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour, IAttackable
{
    [SerializeField] private TriggerObserver _observer;

    private IEnemyAnimator _animator;
    private IEnemyStateMachine _stateMachine;

    private EnemyAttackLogic _attackLogic;

    private Action[] _startAttackAnimations;
    private Action[] _stopAttackAnimations;

    private void OnValidate()
    {
        _observer ??= GetComponentInChildren<TriggerObserver>();
    }

    private void Start()
    {
        IEnemy enemy = GetComponent<IEnemy>();

        _animator = enemy.Animator;
        _stateMachine = enemy.StateMachine;

        _attackLogic = new EnemyAttackLogic(enemy.Data, transform);

        _startAttackAnimations = new Action[] { _animator.PlayAttacking1, _animator.PlayAttacking2 };
        _stopAttackAnimations = new Action[] { _animator.StopAttacking1, _animator.StopAttacking2 };
    }

    private void OnEnable()
    {
        _observer.Entered += OnEntered;
        _observer.Exited += OnExited;
    }

    private void OnDisable()
    {
        _observer.Entered -= OnEntered;
        _observer.Exited -= OnExited;
    }

    public void StartAttack()
    {
        PlayAttackAnimation();
    }

    public void StopAttack()
    {
        StopAnimations();
    }

    private void StopAnimations()
    {
        foreach (var animation in _stopAttackAnimations)
            animation?.Invoke();
    }

    private void OnEntered(Collider collider)
    {
        enabled = true;
        _stateMachine.SwitchState<EnemyAttackingState>();
    }

    private void OnExited(Collider collider)
    {
        enabled = false;
        _stateMachine.SwitchState<EnemyWalkingState>();
    }

    private void PlayAttackAnimation()
    {
        int index = UnityEngine.Random.Range(0, _startAttackAnimations.Length);
        _startAttackAnimations[index]?.Invoke();
    }

    private void AttackStarted()
    {
        _attackLogic.Attack();
    }

    private void AttackEnded()
    {
        ResetAttackAnimation();
    }

    private void ResetAttackAnimation()
    {
        StopAnimations();
        StartAttack();
    }
}