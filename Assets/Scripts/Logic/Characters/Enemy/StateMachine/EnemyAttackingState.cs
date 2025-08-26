using UnityEngine;

public class EnemyAttackingState : IEnemyState
{
    private readonly IAttackable _attackable;

    public EnemyAttackingState(IAttackable attackable)
    {
        _attackable = attackable;
    }

    public void Enter()
    {
        Debug.Log($"Enter {nameof(EnemyWalkingState)}");
        _attackable.StartAttack();
    }

    public void Exit()
    {
        Debug.Log($"Exit {nameof(EnemyWalkingState)}");
        _attackable.StopAttack();
    }
}