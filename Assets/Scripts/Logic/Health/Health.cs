using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    public event Action<float, float> HealthChanged;
    public event Action Died;

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