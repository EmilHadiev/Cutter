using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private int _currentHealth;
    private int _maxHealth;

    public event Action Died;
    public event Action<float, float> HealthChanged;

    [Inject]
    private void Constructor(PlayerData data)
    {
        _maxHealth = data.Health;
        _currentHealth = data.Health;
    }

    public void AddHealth(int health)
    {
        if (health <= 0)
            return;

        _currentHealth += health;

        if (_currentHealth >= _maxHealth)
            _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Kill();
    }

    public void Kill()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}