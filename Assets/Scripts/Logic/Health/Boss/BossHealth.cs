using System;
using UnityEngine;

[RequireComponent(typeof(BossHealthView))]
public class BossHealth : MonoBehaviour, IHealth
{
    [SerializeField] private int _health;

    public event Action Died;
    public event Action<int> HealthChanged;


    public void AddHealth(int health)
    {
        
    }

    public void Kill()
    {
        Died?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Kill();
            return;
        }

        HealthChanged?.Invoke(_health);
    }
}