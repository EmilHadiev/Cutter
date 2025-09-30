using UnityEngine;

public abstract class BossReward : MonoBehaviour
{
    private void OnDestroy() => SetReward();

    protected abstract void SetReward();
}

public class SwordBossReward : BossReward
{
    protected override void SetReward()
    {
        
    }
}