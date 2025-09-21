using UnityEngine;
using Zenject;

public class EnemyGameOverChecker : MonoBehaviour
{
    private IEnemyStateMachine _stateMachine;

    [Inject]
    private readonly IGameOverService _gameOverService;

    private void Start()
    {
        _stateMachine = GetComponent<IEnemy>().StateMachine;
    }

    private void OnEnable()
    {
        _gameOverService.Lost += PlayerLost;
        _gameOverService.Continue += PlayerContinue;
    }

    private void OnDisable()
    {
        _gameOverService.Lost -= PlayerLost;
        _gameOverService.Continue -= PlayerContinue;
    }

    private void PlayerLost()
    {
        _stateMachine.SaveCurrentState();
        _stateMachine.SwitchState<EnemyVictoryState>();
    }

    private void PlayerContinue() => _stateMachine.LoadSavedState();
}