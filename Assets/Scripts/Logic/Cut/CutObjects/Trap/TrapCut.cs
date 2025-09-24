using DynamicMeshCutter;
using UnityEngine;
using Zenject;

public class TrapCut : MonoBehaviour, ICuttable
{
    [SerializeField] private MeshTarget[] _targets;

    private const int Coins = 5;

    private IGameplaySoundContainer _soundContainer;
    private IRewardService _rewardService;

    private void OnValidate()
    {
        _targets ??= GetComponentsInChildren<MeshTarget>();
    }

    [Inject]
    private void Constructor(IGameplaySoundContainer soundContainer, IRewardService rewardService)
    {
        _soundContainer = soundContainer;
        _rewardService = rewardService;
    }

    public void TryActivateCut()
    {
        TargetToggle(true);
    }

    public void DeactivateCut()
    {
        _soundContainer.PlayWhenFree(SoundsName.AttackObstacleImpact);
        _rewardService.AddAdditionalReward(Coins);
    }

    private void TargetToggle(bool isOn)
    {
        foreach (var target in _targets)
            target.enabled = isOn;
    }
}