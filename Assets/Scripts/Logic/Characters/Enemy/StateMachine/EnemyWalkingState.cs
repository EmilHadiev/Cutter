using UnityEngine;

public class EnemyWalkingState : IEnemyState
{
    private readonly IMovable _mover;

    public EnemyWalkingState(IMovable mover)
    {
        _mover = mover;
    }

    public void Enter()
    {
        Debug.Log($"Exit {nameof(EnemyWalkingState)}");
        _mover.StartMove();
    }

    public void Exit()
    {
        Debug.Log($"Exit {nameof(EnemyWalkingState)}");
        _mover.StopMove();
    }
}