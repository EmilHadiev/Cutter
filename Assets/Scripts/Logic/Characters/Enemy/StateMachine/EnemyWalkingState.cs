public class EnemyWalkingState : IEnemyState
{
    private readonly IMovable _mover;

    public EnemyWalkingState(IMovable mover)
    {
        _mover = mover;
    }

    public void Enter()
    {
        _mover.StartMove();
    }

    public void Exit()
    {
        _mover.StopMove();
    }
}