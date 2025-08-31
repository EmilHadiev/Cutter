using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{
    public event Action Died;
    public event Action<int> HealthChanged;

    public void AddHealth(int health)
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }

    public void Kill()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}