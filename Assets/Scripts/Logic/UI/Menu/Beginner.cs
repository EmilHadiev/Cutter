using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class Beginner : MonoBehaviour
{
    [SerializeField] private Button _button;

    private IUIStateMachine _uiStateMachine;
    private IAdvService _adv;
    private IGameReadyService _gameReadyService;

    private void OnEnable()
    {
        _button.onClick.AddListener(Clicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Clicked);
    }

    private void Start()
    {
        _gameReadyService.StartGame();
    }

    [Inject]
    private void Constructor(IUIStateMachine uiStateMachine, IAdvService advService, IGameReadyService gameReadyService)
    {
        _uiStateMachine = uiStateMachine;
        _adv = advService;
        _gameReadyService = gameReadyService;
    }

    private void Clicked()
    {
        _uiStateMachine.Switch<GameplayStateUI>();
        _adv.StickyBannerToggle(false);
        _gameReadyService.StartGameplay();
    }
}