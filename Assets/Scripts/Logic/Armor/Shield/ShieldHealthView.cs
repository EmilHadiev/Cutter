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
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        _text.text = health.ToString();
    }
}