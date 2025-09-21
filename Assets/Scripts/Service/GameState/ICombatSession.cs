using System;

public interface ICombatSession
{
    public event Action EnemyDied;

    void RemoveEnemy();
    void StartFight();
    void TryContinueCombat();
}