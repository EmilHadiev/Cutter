public class EnemyAttackingState : IEnemyState
{
    private readonly IAttackable _attackable;

    public EnemyAttackingState(IAttackable attackable)
    {
        _attackable = attackable;
    }

    public void Enter()
    {
        _attackable.StartAttack();
    }

    public void Exit()
    {
        _attackable.StopAttack();
    }
}