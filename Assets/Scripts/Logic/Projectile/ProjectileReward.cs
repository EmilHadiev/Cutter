using UnityEngine;
using Zenject;

[RequireComponent(typeof(EnemyHealth))]
public class ProjectileReward : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;

    private const int AdditionalReward = 5;

    private IComboSystem _combo;
    private IRewardService _reward;
    private IEnemySoundContainer _soundContainer;

    private void OnValidate()
    {
        _health ??= GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        _health.Died += OnKilled;
    }

    private void OnDisable()
    {
        _health.Died -= OnKilled;
    }

    [Inject]
    private void Constructor(IPlayer player, IRewardService rewardService, IEnemySoundContainer enemySound)
    {
        _combo = player.ComboSystem;
        _reward = rewardService;
        _soundContainer = enemySound;
    }

    private void OnKilled()
    {
        _soundContainer.Play(SoundsName.SpawnerDeath);
        _combo.AddPoint();
        _reward.AddAdditionalReward(AdditionalReward);
    }
}