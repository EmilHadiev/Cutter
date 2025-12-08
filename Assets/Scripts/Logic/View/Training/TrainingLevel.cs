using UnityEngine;

public class TrainingLevel : MonoBehaviour
{
    [SerializeField] private TrainingCanvas _canvas;
    [SerializeField] private TriggerObserver _playerTrigger;

    private void OnEnable()
    {
        _playerTrigger.Entered += OnPlayerEntered;
    }

    private void OnDisable()
    {
        _playerTrigger.Entered -= OnPlayerEntered;
    }

    private void Start()
    {
        _canvas.TryShowHint();
    }

    private void OnPlayerEntered(Collider collider)
    {
        _canvas.TryShowHint();
    }
}