public class CombatSession : ICombatSession
{
    private readonly IPlayer _player;

    private int _countEnemies;

    public CombatSession(IPlayer player)
    {
        _player = player;
    }

    public void StartFight()
    {
        _player.Movable.StopMove();
        _countEnemies++;
    }

    public void RemoveEnemy()
    {
        if (_countEnemies == 0)
            return;

        _countEnemies -= 1;

        if (_countEnemies == 0)
            StopFight();
    }

    private void StopFight()
    {
        _player.Movable.StartMove();
    }
}