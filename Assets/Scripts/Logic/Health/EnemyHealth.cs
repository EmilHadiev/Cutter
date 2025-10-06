using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth, ICutCondition
{
    [SerializeField] private float _health = 1;

    public bool IsCanCut => _health > 1 == false;

    public event Action Died;
    public event Action<int> HealthChanged;

    public void AddHealth(int health)
    {
        _health += health;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
            Kill();
    }

    public void Kill()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }

    public void HandleFailCut()
    {
        HealthChanged?.Invoke(1);
        TakeDamage(1);
    }
}