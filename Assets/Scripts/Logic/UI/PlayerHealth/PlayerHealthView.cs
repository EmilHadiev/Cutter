using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private PlayerHealthPointView _template;

    private IHealth _playerHealth;
    private List<PlayerHealthPointView> _points;

    private int _maxHealth;

    private void Awake()
    {
        CreateTemplates();
    }

    private void OnEnable()
    {
        _playerHealth.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _playerHealth.HealthChanged -= OnHealthChanged;
    }

    [Inject]
    private void Constructor(IPlayer player)
    {
        _playerHealth = player.Health;
        _maxHealth = player.Data.MaxHealth;
    }

    private void CreateTemplates()
    {
        _points = new List<PlayerHealthPointView>(_maxHealth);

        for (int i = 0; i < _maxHealth; i++)
        {
            PlayerHealthPointView prefab = Instantiate(_template, transform);
            prefab.gameObject.SetActive(false);
            _points.Add(prefab);
        }
    }

    private void OnHealthChanged(int currentHealth)
    {
        HidePoints();

        Debug.Log($"CurrentHealth ~ {currentHealth}");
        if (currentHealth == 1)
            return;

        ShowPoints(currentHealth);
    }

    private void HidePoints()
    {
        foreach (var point in _points)
            point.gameObject.SetActive(false);
    }

    private void ShowPoints(int health)
    {
        for (int i = 0; i < health; i++)
            _points[i].gameObject.SetActive(true);
    }
}