using DG.Tweening;
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
    private IMetricService _metricService;

    private void OnEnable()
    {
        _button.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(StartGame);
    }

    private void Start()
    {
        ActivateGRA();
        PlayAnimation();
    }

    [Inject]
    private void Constructor(IUIStateMachine uiStateMachine, IAdvService advService, IGameReadyService gameReadyService, IMetricService metricService)
    {
        _uiStateMachine = uiStateMachine;
        _adv = advService;
        _gameReadyService = gameReadyService;
        _metricService = metricService;
    }

    private void StartGame()
    {
        _uiStateMachine.Switch<GameplayStateUI>();
        _adv.StickyBannerToggle(false);
        _gameReadyService.StartGameplay();
        _metricService.SendMetric(MetricsName.StartGame);
    }

    private void ActivateGRA() => _gameReadyService.StartGame();
    
    private void PlayAnimation()
    {
        float scaleMultiplier = 1.25f;
        int duration = 1;

        transform.DOScale(transform.localScale * scaleMultiplier, duration).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    } 
}