using System;
using UnityEngine;

public class ShieldHealth : MonoBehaviour, IHealth
{
    private int _health;
    private int _maxHealth;

    public event Action Died;
    public event Action<int> HealthChanged;

    public void SetHealth(int shieldHealth)
    {
        _health = shieldHealth;
        _maxHealth = shieldHealth;
        HealthChanged?.Invoke(_health);
    }

    public void AddHealth(int health)
    {
        _health += health;

        if (_health > _maxHealth)
            _health = _maxHealth;

        HealthChanged?.Invoke(_health);
    }

    public void Kill()
    {
        _health = _maxHealth;
        Died?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Kill();

        HealthChanged?.Invoke(_health);
    }
}