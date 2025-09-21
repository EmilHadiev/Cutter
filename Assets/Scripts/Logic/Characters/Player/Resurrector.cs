using UnityEngine;
using Zenject;

public class Resurrector : MonoBehaviour
{
    private IGameOverService _gameOverService;
    private ICombatSession _combat;
    private IHealth _health;

    private int _healthPoints;

    private void Start()
    {
        _gameOverService.Continue += Resurrect;
    }

    private void OnDestroy()
    {
        _gameOverService.Continue -= Resurrect;
    }

    [Inject]
    private void Constructor(IPlayer player, IGameOverService gameOverService, ICombatSession combatSession)
    {
        _health = player.Health;
        _healthPoints = player.Data.Health;
        _gameOverService = gameOverService;
        _combat = combatSession;
    }

    private void Resurrect()
    {
        _combat.TryContinueCombat();
        gameObject.SetActive(true);
        _health.AddHealth(_healthPoints);
    }
}