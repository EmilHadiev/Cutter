using System;

public class SwordTemplateView : SkinTemplateView
{
    public event Action<SwordData> Clicked;

    protected override void PerformEvent(SkinData skinData)
    {
        Clicked?.Invoke(skinData as SwordData);
    }
}
