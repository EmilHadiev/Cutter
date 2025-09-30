using System.Collections.Generic;
using Zenject;

public class SpecialBossReward : BossReward
{
    private IEnumerable<SwordData> _swords;
    private IEnumerable<ParticleData> _particles;

    [Inject]
    private void Constructor(IEnumerable<SwordData> swords, IEnumerable<ParticleData> particles)
    {
        _swords = swords;
        _particles = particles;
    }

    protected override void SetReward()
    {
        UnLockReward(_swords);
        UnLockReward(_particles);
    }

    private void UnLockReward(IEnumerable<SkinData> skins)
    {
        foreach (var skin in skins)
        {
            if (skin.IsSpecialReward)
            {
                skin.IsPurchased = true;
                skin.IsSpecialReward = false;
            }
        }
    }
}