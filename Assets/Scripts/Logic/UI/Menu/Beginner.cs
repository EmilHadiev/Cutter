using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class Beginner : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Canvas _pauseUI;

    private IGameStarter _starter;

    private void OnValidate()
    {
        _button ??= GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(StartGame);
    }

    [Inject]
    private void Constructor(IGameStarter starter)
    {
        _starter = starter;
    }

    private void StartGame()
    {
        _starter.Start();
        _pauseUI.enabled = false;
        gameObject.SetActive(false);
    }
}