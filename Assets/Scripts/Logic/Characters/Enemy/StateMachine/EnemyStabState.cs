using UnityEngine;

public class EnemyStabState : IEnemyState
{
    public void Enter()
    {
        Debug.Log($"Enter {nameof(EnemyStabState)}");
    }

    public void Exit()
    {
        Debug.Log($"Exit {nameof(EnemyStabState)}");
    }
}