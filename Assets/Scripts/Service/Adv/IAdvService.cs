using System;

public interface IAdvService
{
    void ShowInterstitial();
    void ShowReward(Action action = null);
    void SkipNextInterstitial();
    void StickyBannerToggle(bool isOn);
}