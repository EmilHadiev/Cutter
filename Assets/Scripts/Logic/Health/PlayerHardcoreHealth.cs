using UnityEngine;
using Zenject;

public class PlayerHardcoreHealth : MonoBehaviour
{
    [SerializeField] private PlayerHealth _health;

    private PlayerData _data;
    private PlayerProgress _progress;

    private void OnValidate()
    {
        _health ??= GetComponent<PlayerHealth>();        
    }

    private void OnEnable()
    {
        _health.HealthChanged += TryChangeHardcoreHealth;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= TryChangeHardcoreHealth;
    }

    [Inject]
    public void Constructor(PlayerData playerData, PlayerProgress progress)
    {
        _data = playerData;
        _progress = progress;
    }

    private void TryChangeHardcoreHealth(int health)
    {
        if (_progress.IsHardcoreMode)
            _data.HardcoreHealth = health;
    }
}