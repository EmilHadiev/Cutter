using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;

public class RewardViewCreator
{
    private readonly IFactory _factory;
    private readonly ParticleData[] _particles;
    private readonly SwordData[] _swords;
    private readonly Transform _container;
    private readonly RewardParticleViewChanger _particleViewChanger;
    private readonly ILightOffable _lightOffable;

    private const string UI = "UI";

    public IPurchasable Skin { get; private set; }

    public RewardViewCreator(IFactory factory, Transform container, 
        ParticleData[] particleData, SwordData[] swordData, ILightOffable lightOffable)
    {
        _factory = factory;
        _particles = particleData;
        _swords = swordData;
        _container = container;
        _particleViewChanger = new RewardParticleViewChanger();
        _lightOffable = lightOffable;
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
        prefab.transform.localRotation = Quaternion.Euler(skinData.ViewRotation.x, skinData.ViewRotation.y, skinData.ViewRotation.z);
        prefab.transform.localPosition = skinData.ViewPosition;

        if (skinData is ParticleData)
        {
            _particleViewChanger.Change(skinName, prefab);
            _lightOffable.OffLight();
        }

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
        SkinData skin = null;
        int decadeIndex;

        if (currentLevel <= 10)
        {
            decadeIndex = 0;
        }
        else
        {
            decadeIndex = (currentLevel - 1) / 10;
        }

        if (decadeIndex % 2 == 0)
        {
            skin = GetSkin(_swords);
        }
        else
        {
            skin = GetSkin(_particles);
        }

        Skin = skin;
        return skin;
    }


    private SkinData GetSkin(SkinData[] skins)
    {
        return skins.FirstOrDefault(s => s.IsSpecialReward == false && s.IsPurchased == false);
    }
}