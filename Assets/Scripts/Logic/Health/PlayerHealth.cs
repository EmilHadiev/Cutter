using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerHardcoreHealth))]
public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private ParticleView _healthView;
    [SerializeField] private Vector3 _cameraPosition;

    private IGameOverService _gameOverService;
    private IGameplaySoundContainer _soundContainer;
    private PlayerView _playerView;

    private int _currentHealth;
    private int _maxHealth;

    public event Action Died;
    public event Action<int> HealthChanged;

    private void Awake()
    {
        _playerView = new PlayerView(Camera.main, _healthView);
    }

    private void OnEnable()
    {        
        _healthView.Stop();
        _playerView.SetParent(transform);
        _playerView.SetCameraPosition(_cameraPosition);
    }

    private void Start()
    {
        HealthChanged?.Invoke(_currentHealth);
    }

    [Inject]
    private void Constructor(PlayerData data, IGameOverService gameOverService, IGameplaySoundContainer gameplaySound, PlayerProgress progress)
    {
        SetCurrentHealth(data, progress);

        if (_currentHealth == data.MaxHealth)
            _currentHealth = data.MaxHealth;

        _maxHealth = data.MaxHealth;

        _gameOverService = gameOverService;
        _soundContainer = gameplaySound;
    }

    private void SetCurrentHealth(PlayerData data, PlayerProgress progress)
    {
        if (progress.IsHardcoreMode)
        {
            if (data.MaxHealth <= data.HardcoreHealth)
                _currentHealth = data.MaxHealth;
            else 
                _currentHealth = data.HardcoreHealth;
        }
        else
        {
            _currentHealth = data.Health;
        }
    }

    public void AddHealth(int health)
    {
        if (health <= 0)
            return;

        if (_currentHealth >= _maxHealth)
            return;

        _currentHealth += health;
        HealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            return;

        _soundContainer.Play(SoundsName.TakeDamage);
        ApplyDamage(damage);
        _healthView.Play();
        HealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
            Kill();
    }

    private void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
    }

    public void Kill()
    {
        Died?.Invoke();
        _playerView.SetParent(null);
        _gameOverService.Lose();
        gameObject.SetActive(false);
    }
}