using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IHealth
{
    private IGameOverService _gameOverService;
    private Camera _camera;

    private int _currentHealth;
    private int _maxHealth;

    public event Action Died;
    public event Action<int> HealthChanged;

    private void Awake()
    {
        _camera = Camera.main;
    }

    [Inject]
    private void Constructor(PlayerData data, IGameOverService gameOverService)
    {
        _maxHealth = data.Health;
        _currentHealth = data.Health;
        _gameOverService = gameOverService;
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
        HealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
            Kill();
    }

    public void Kill()
    {
        Died?.Invoke();
        _camera.transform.parent = null;
        _gameOverService.Lose();
        gameObject.SetActive(false);
    }
}