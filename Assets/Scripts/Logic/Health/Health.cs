using System;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    public event Action<float, float> HealthChanged;
    public event Action Died;

    public void TakeDamage()
    {

    }

    public void AddHealth()
    {

    }

    public void Kill()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }
}