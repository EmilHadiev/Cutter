public class EnemyStateStun : IEnemyState
{
    private readonly IDefensible _defender;

    public EnemyStateStun(IDefensible defensible)
    {
        _defender = defensible;
    }

    public void Enter()
    {
        _defender.Deactivate();
    }

    public void Exit()
    {
        _defender.Deactivate();
    }
}