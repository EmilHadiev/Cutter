using System;

public interface IHealth
{
    event Action Died;
    event Action<float, float> HealthChanged;

    void AddHealth(int health);
    void TakeDamage(int damage);
    void Kill();
}