using System;
using UnityEngine;

public class SwordTemplateView : SkinTemplateView
{
    public event Action<SwordData> Clicked;

    protected override void PerformEvent(SkinData skinData)
    {
        Clicked?.Invoke(skinData as SwordData);
    }

    protected override void ShowPrefab(SwordSkinViewer skinViewer, GameObject prefab, SkinData data)
    {
        skinViewer.SetSword(prefab, data);
    }
}
