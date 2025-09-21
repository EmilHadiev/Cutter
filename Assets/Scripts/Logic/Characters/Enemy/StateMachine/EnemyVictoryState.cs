public class EnemyVictoryState : IEnemyState
{
    private readonly IEnemyAnimator _animator;

    public EnemyVictoryState(IEnemyAnimator animator)
    {
        _animator = animator;
    }

    public void Enter()
    {
        _animator.SetVictoryTrigger();
    }

    public void Exit()
    {
        _animator.StopAttacking2();
        _animator.StopAttacking1();
        _animator.StopRunning();

        _animator.StopVictory();
    }
}