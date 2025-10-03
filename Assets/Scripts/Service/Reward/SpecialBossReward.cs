using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpecialBossReward : BossReward
{
    private IEnumerable<SwordData> _swords;
    private IEnumerable<ParticleData> _particles;

    private PlayerProgress _progress;

    [Inject]
    private void Constructor(IEnumerable<SwordData> swords, IEnumerable<ParticleData> particles, PlayerProgress progress)
    {
        _swords = swords;
        _particles = particles;
        _progress = progress;
    }

    protected override void SetReward()
    {
        if (_progress.IsHardcoreOpen)
        {
            UnLockReward(_swords);
            UnLockReward(_particles);
        }
        else
        {
            _progress.IsHardcoreOpen = true;
        }
    }

    private void UnLockReward(IEnumerable<SkinData> skins)
    {
        foreach (var skin in skins)
            if (skin.IsSpecialReward)
                skin.IsSpecialReward = false;
    }
}