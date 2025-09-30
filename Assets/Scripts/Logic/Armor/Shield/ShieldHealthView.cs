using TMPro;
using UnityEngine;

public class ShieldHealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private IHealth _health;

    private void Awake()
    {
        _health = GetComponentInParent<IHealth>();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
        _health.Died -= OnDied;
    }

    private void OnDied()
    {
        _text.text = "";
    }

    private void OnHealthChanged(int health)
    {
        _text.text = health.ToString();
    }
}