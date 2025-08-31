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
        
    }
}