using System;

public class CombatSession : ICombatSession
{
    private readonly IPlayer _player;

    private int _countEnemies;

    public event Action EnemyDied;

    public CombatSession(IPlayer player)
    {
        _player = player;
    }

    public void StartFight()
    {
        StopPlayer();
        _countEnemies++;
    }

    public void TryContinueCombat()
    {
        if (_countEnemies > 0)
            StopPlayer();
    }

    public void RemoveEnemy()
    {
        if (_countEnemies == 0)
            return;

        _countEnemies -= 1;
        EnemyDied?.Invoke();

        if (_countEnemies == 0)
            StopFight();
    }

    private void StopFight()
    {
        _player.Movable.StartMove();
    }

    private void StopPlayer()
    {
        _player.Movable.StopMove();
    }
}