using UnityEngine;

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
        Debug.Log("Враг добавлен!");
        _player.Movable.StopMove();
        _countEnemies++;
    }

    public void RemoveEnemy()
    {
        if (_countEnemies == 0)
            return;

        Debug.Log("ВЫЧЕТ!");

        _countEnemies -= 1;

        Debug.Log(_countEnemies);

        if (_countEnemies == 0)
            StopFight();
    }

    private void StopFight()
    {
        Debug.Log("Продолжаю двигаться!");
        _player.Movable.StartMove();
    }
}