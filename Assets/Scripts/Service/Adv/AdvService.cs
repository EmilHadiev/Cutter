using System;
using YG;

public class AdvService : IAdvService
{
    public void ShowInterstitial()
    {
        YG2.InterstitialAdvShow();
    }

    public void SkipNextInterstitial()
    {
        YG2.SkipNextInterAdCall();
    }

    public void ShowReward(Action action = null)
    {
        YG2.RewardedAdvShow("", () =>
        {
            action?.Invoke();
        });
    }

    public void StickyBannerToggle(bool isOn)
    {
        YG2.StickyAdActivity(isOn);
    }
}