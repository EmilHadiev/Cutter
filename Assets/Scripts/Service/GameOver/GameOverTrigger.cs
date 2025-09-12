using UnityEngine;
using Zenject;

[RequireComponent(typeof(TriggerObserver))]
public class GameOverTrigger : MonoBehaviour
{
    [SerializeField] private TriggerObserver _observer;

    [Inject]
    private readonly IGameOverService _gameOverService;

    private void OnValidate() => _observer ??= GetComponent<TriggerObserver>();

    private void OnEnable() => _observer.Entered += OnPlayerEntered;

    private void OnDisable() => _observer.Entered -= OnPlayerEntered;

    private void OnPlayerEntered(Collider collider) => _gameOverService.Win();
}