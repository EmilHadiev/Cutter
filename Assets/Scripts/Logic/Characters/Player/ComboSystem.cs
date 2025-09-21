using UnityEngine;
using Zenject;

public class ComboSystem : MonoBehaviour, IComboSystem
{
    [SerializeField] private ParticleViewText _particleText;

    private const int ComboMultiplier = 10;

    [Inject]
    private readonly ICombatSession _comboSession;

    public int GetComboReward => _combo * ComboMultiplier;

    private int _combo;

    private void OnEnable()
    {
        _comboSession.EnemyDied += OnEnemyDied;
        _particleText.Stop();
    }

    private void OnDisable()
    {
        _comboSession.EnemyDied -= OnEnemyDied;
        ResetCombo();
    }

    public void ResetCombo() => _combo = 0;

    private void OnEnemyDied()
    {
        ++_combo;

        _particleText.SetText(_combo.ToString() + "x");
        _particleText.Play();
    }
}