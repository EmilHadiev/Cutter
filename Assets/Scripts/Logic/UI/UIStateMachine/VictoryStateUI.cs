using TMPro;
using UnityEngine;
using Zenject;

public class VictoryStateUI : UiState
{
    [SerializeField] private TMP_Text _rewardText;

    [Inject]
    private readonly IRewardService _rewardService;

    public override void Show()
    {
        base.Show();
        _rewardText.text = $"{_rewardService.StandartReward} + {_rewardService.AdditionalReward}";
        _rewardService.GiveReward();
    }
}