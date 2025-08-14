using System;

public interface IHealth
{
    event Action Died;
    event Action<float, float> HealthChanged;

    void AddHealth();
    void Kill();
    void TakeDamage();
}