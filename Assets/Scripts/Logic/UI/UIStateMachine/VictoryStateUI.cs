using TMPro;
using UnityEngine;
using Zenject;

public class VictoryStateUI : UiState
{
    [SerializeField] private TMP_Text _rewardText;
    [SerializeField] private RewardAdder _rewardAdder;

    [Inject]
    private readonly IRewardService _rewardService;

    public override void Show()
    {
        base.Show();
        _rewardService.GiveReward();
        _rewardText.text = $"{_rewardService.StandartReward} + {_rewardService.AdditionalReward}";
        _rewardAdder.TryShow();
    }
}