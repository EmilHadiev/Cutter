using System;

public interface IHealth
{
    event Action Died;
    event Action<int> HealthChanged;

    void AddHealth(int health);
    void TakeDamage(int damage);
    void Kill();
}