using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RewardViewCreator
{
    private readonly IFactory _factory;
    private readonly ParticleData[] _particles;
    private readonly SwordData[] _swords;
    private readonly Transform _container;

    private const string UI = "UI";

    public RewardViewCreator(IFactory factory, Transform container, ParticleData[] particleData, SwordData[] swordData)
    {
        _factory = factory;
        _particles = particleData;
        _swords = swordData;
        _container = container;
    }

    public bool TryShowReward(int currentLevel)
    {
        var skin = GetRandomSkin(currentLevel);

        if (skin == null)
            return false;
        else
            CreateView(skin).Forget();

        return true;
    }

    private async UniTask CreateView(SkinData skinData)
    {
        string skinName = GetSkinName(skinData);
        GameObject prefab = await _factory.CreateAsync(skinName);
        
        prefab.transform.parent = _container;
        prefab.transform.localScale = new Vector3(skinData.ViewScale, skinData.ViewScale, skinData.ViewScale);
        prefab.transform.localPosition = skinData.ViewPosition;
        prefab.transform.localRotation = Quaternion.Euler(skinData.ViewRotation.x, skinData.ViewRotation.y, skinData.ViewRotation.z);

        LayerChanger.SetLayerRecursively(prefab, LayerMask.NameToLayer(UI));
    }

    private string GetSkinName(SkinData skin)
    {
        if (skin is SwordData sword)
            return sword.Sword.ToString();
        else if (skin is ParticleData particle)
            return particle.Particle.ToString();

        throw new ArgumentException(nameof(skin));
    }

    private SkinData GetRandomSkin(int currentLevel)
    {
        string level = currentLevel.ToString();
        int firstSymb = (int)level[0];

        if (firstSymb % 2 == 0)
            return GetSkin(_particles);
        else
            return GetSkin(_swords);
    }

    private SkinData GetSkin(SkinData[] skins)
    {
        return skins.FirstOrDefault(s => s.IsSpecialReward == false && s.IsPurchased == false);
    }
}