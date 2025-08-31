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
    }

    private void OnDisable()
    {
        _gameOverService.Lost -= PlayerLost;
    }

    private void PlayerLost()
    {
        _stateMachine.SwitchState<EnemyVictoryState>();
    }
}