using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RewardAdder : MonoBehaviour
{
    [SerializeField] private Button _openReward;
    [SerializeField] private Image _circle;
    [SerializeField] private Transform _container;
    [SerializeField] private TMP_Text _percentText;

    private const int MaxRewardPercent = 1;

    private IFactory _factory;
    private PlayerProgress _progress;
    private RewardViewCreator _viewCreator;
    private RewardUnlocker _rewardUnlocker;
    private RewardCalculator _rewardCalculator;

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

        _rewardCalculator = new RewardCalculator(playerProgress);
        _viewCreator = new RewardViewCreator(_factory, _container, _particles.ToArray(), _swords.ToArray());
        _rewardUnlocker = new RewardUnlocker(advService, uiSound);
    }

    public void TryShow()
    {
        if (_viewCreator.TryShowReward(_progress.CurrentLevel))
        {
            EnableToggle(true);
            ShowRewardView();
        }
        else
        {
            EnableToggle(false);
        }
    }

    private void ShowRewardStep(float rewardPercnet)
    {
        _circle.fillAmount = rewardPercnet / MaxRewardPercent;
    }

    private void ShowRewardView()
    {
        float rewardPercent = _rewardCalculator.GetPercent();

        ShowRewardStep(rewardPercent);
        RewardInfoToggle((int)rewardPercent == MaxRewardPercent);
        ShowRewardPercentText(rewardPercent);
    }

    private void EnableToggle(bool isOn) => gameObject.SetActive(isOn);
    private void RewardInfoToggle(bool isRewardLevel)
    {
        _openReward.gameObject.SetActive(isRewardLevel);
        _percentText.gameObject.SetActive(!isRewardLevel);
    }

    private void ShowRewardPercentText(float rewardPecent)
    {
        int maxPercent = 100;
        _percentText.text = $"{rewardPecent * maxPercent}%";
    }

    private void OpenReward()
    {
        _rewardUnlocker.TryUnlock(_viewCreator.Skin);
        EnableToggle(false);
    }
}