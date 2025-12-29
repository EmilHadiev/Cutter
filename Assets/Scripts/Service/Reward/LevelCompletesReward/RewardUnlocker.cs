public class RewardUnlocker
{
    private readonly IAdvService _advService;
    private readonly IUISoundContainer _uiSound;

    public RewardUnlocker(IAdvService advService, IUISoundContainer uiSound)
    {
        _advService = advService;
        _uiSound = uiSound;
    }

    public void TryUnlock(IPurchasable purchasable)
    {
        _advService.ShowReward(() =>
        {
            UnlockSkin(purchasable);
        });
    }

    private void UnlockSkin(IPurchasable purchasable)
    {
        purchasable.IsPurchased = true;
        _uiSound.Play(SoundsName.UnlockSkin);
    }
}