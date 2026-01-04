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
    private readonly PlayerData _playerData;

    private const string UI = "UI";

    private AssetProvider.Particles _selectedParticle;
    private AssetProvider.Swords _selectedSword;

    private bool _isSwordReward;

    public IPurchasable Skin { get; private set; }

    public RewardViewCreator(IFactory factory, Transform container, 
        ParticleData[] particleData, SwordData[] swordData, ILightOffable lightOffable, PlayerData playerData)
    {
        _factory = factory;
        _particles = particleData;
        _swords = swordData;
        _container = container;
        _particleViewChanger = new RewardParticleViewChanger();
        _lightOffable = lightOffable;
        _playerData = playerData;
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

    public void SetSelectedReward()
    {
        if (_isSwordReward)
        {
            _playerData.Sword = _selectedSword;
        }
        else
        {
            _playerData.Particle = _selectedParticle;
        }    
    }

    private async UniTask CreateView(SkinData skinData)
    {
        string skinName = GetSkinNameAndSetSkinName(skinData);
        GameObject prefab = await _factory.CreateAsync(skinName);

        prefab.transform.parent = _container;
        prefab.transform.localScale = new Vector3(skinData.ViewScale, skinData.ViewScale, skinData.ViewScale);
        prefab.transform.localRotation = Quaternion.Euler(skinData.ViewRotation.x, skinData.ViewRotation.y, skinData.ViewRotation.z);
        prefab.transform.localPosition = skinData.ViewPosition;

        _isSwordReward = true;

        if (skinData is ParticleData)
        {
            _isSwordReward = false;
            _particleViewChanger.Change(skinName, prefab);
            _lightOffable.OffLight();
        }

        LayerChanger.SetLayerRecursively(prefab, LayerMask.NameToLayer(UI));
    }

    private string GetSkinNameAndSetSkinName(SkinData skin)
    {
        if (skin is SwordData sword)
        {
            _selectedSword = sword.Sword;
            return sword.Sword.ToString();
        }            
        else if (skin is ParticleData particle)
        {
            _selectedParticle = particle.Particle;
            return particle.Particle.ToString();
        }

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