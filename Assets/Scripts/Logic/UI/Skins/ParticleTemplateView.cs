using System;

public class ParticleTemplateView : SkinTemplateView
{
    public event Action<ParticleData> Clicked;

    protected override void PerformEvent(SkinData skinData)
    {
        Clicked?.Invoke(skinData as ParticleData);
    }
}
