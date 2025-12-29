using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RewardAdder : MonoBehaviour
{
    [SerializeField] private Button _openReward;
    [SerializeField] private Image _circle;
    [SerializeField] private Transform _container;

    private IFactory _factory;
    private PlayerProgress _progress;
    private RewardViewCreator _viewCreator;
    private RewardUnlocker _rewardUnlocker;

    private IEnumerable<ParticleData> _particles;
    private IEnumerable<SwordData> _swords;

    private void OnEnable() => _openReward.onClick.AddListener(OpenReward);
    private void OnDisable() => _openReward.onClick.RemoveListener(OpenReward);

    [Inject]
    private void Constructor(IFactory factory, PlayerProgress playerProgress, IAdvService advService, 
        IUISoundContainer uiSound, IEnumerable<ParticleData> particles, IEnumerable<SwordData> sowrds)
    {
        _factory = factory;
        _progress = playerProgress;
        _particles = particles;
        _swords = sowrds;

        _viewCreator = new RewardViewCreator(_factory, _container, _particles.ToArray(), _swords.ToArray());
        _rewardUnlocker = new RewardUnlocker(advService, uiSound);
    }

    public void TryShow()
    {
        if (_viewCreator.TryShowReward(_progress.CurrentLevel))
        {
            EnableToggle(true);
            ShowRewardStep();
            TryShowOpenRewardButton();
        }
        else
        {
            EnableToggle(false);
        }
    }

    private void ShowRewardStep()
    {

    }

    private void TryShowOpenRewardButton()
    {

    }

    public float CalculateProgressPercentage(int levelsPassed, int levelsPerReward)
    {
        // ”ровни с момента последней полученной награды
        int levelsSinceLastReward = levelsPassed % levelsPerReward;

        // ≈сли levelsPassed кратно levelsPerReward, то награда уже получена
        // и мы показываем прогресс к следующей награде
        if (levelsSinceLastReward == 0)
        {
            // »грок только что получил награду или еще не начал
            return 0f;
        }

        // –асчет процента
        float percentage = (float)levelsSinceLastReward / levelsPerReward * 100f;
        return percentage;
    }

    private void EnableToggle(bool isOn) => gameObject.SetActive(isOn); 

    private void OpenReward()
    {
        _rewardUnlocker.TryUnlock(_viewCreator.Skin);
        EnableToggle(false);
    }
}