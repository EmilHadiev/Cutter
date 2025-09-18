using System;
using UnityEngine;

public class ParticleTemplateView : SkinTemplateView
{
    public event Action<ParticleData> Clicked;

    protected override void PerformEvent(SkinData skinData)
    {
        Clicked?.Invoke(skinData as ParticleData);
    }

    protected override void ShowPrefab(SwordSkinViewer skinViewer, GameObject prefab, SkinData data)
    {
        skinViewer.SetParticle(prefab, data);
    }
}