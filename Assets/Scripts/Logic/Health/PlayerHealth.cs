using System;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private ParticleView _healthView;

    private IGameOverService _gameOverService;
    private Camera _camera;
    private IGameplaySoundContainer _soundContainer;

    private int _currentHealth;

    public event Action Died;
    public event Action<int> HealthChanged;

    private void OnEnable()
    {
        _camera = Camera.main;
        _healthView.Stop();
        SetParentToElement(transform);
    }

    [Inject]
    private void Constructor(PlayerData data, IGameOverService gameOverService, IGameplaySoundContainer gameplaySound, PlayerProgress progress)
    {
        if (progress.IsHardcoreMode)
            _currentHealth = data.HardcoreHealth;
        else
            _currentHealth = data.Health;

        if (_currentHealth == data.MaxHealth)
            _currentHealth = data.MaxHealth;

        _gameOverService = gameOverService;
        _soundContainer = gameplaySound;
    }

    public void AddHealth(int health)
    {
        if (health <= 0)
            return;

        _currentHealth += health;
        HealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        _soundContainer.Play(SoundsName.TakeDamage);

        _currentHealth -= damage;
        _healthView.Play();
        HealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
            Kill();
    }

    public void Kill()
    {
        Died?.Invoke();
        SetParentToElement(null);
        _gameOverService.Lose();
        gameObject.SetActive(false);
    }

    private void SetParentToElement(Transform parent)
    {
        _camera.transform.parent = parent;
        _healthView.transform.parent = parent;
    }
}