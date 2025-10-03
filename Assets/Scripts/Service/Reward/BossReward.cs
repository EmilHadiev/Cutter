using UnityEngine;
using Zenject;

public abstract class BossReward : MonoBehaviour
{
    private IHealth _health;

    private void Awake()
    {
        _health = GetComponent<IHealth>();
    }

    private void OnEnable() => _health.Died += SetReward;

    private void OnDisable() => _health.Died -= SetReward;

    protected abstract void SetReward();
}