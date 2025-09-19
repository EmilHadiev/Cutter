using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SkinContainer : MonoBehaviour
{
    [SerializeField] private SwordTemplateView _swordView;
    [SerializeField] private ParticleTemplateView _particleView;
    [SerializeField] private Transform _swordContainer;
    [SerializeField] private Transform _particleContainer;
    [SerializeField] private SwordSkinViewer _skinView;

    private PlayerData _playerData;
    private IFactory _factory;
    private ICoinsStorage _coinsStorage;
    private List<SwordTemplateView> _swordsView;
    private List<ParticleTemplateView> _particlesView;
    private SkinUnlocker _skinUnlocker;
    private CoinsCalculator _coinsCalculator;

    private IEnumerable<SwordData> _swordsData;
    private IEnumerable<ParticleData> _particlesData;
    private List<SkinTemplateView> _skins;

    private async void Awake()
    {
        _swordsView = new List<SwordTemplateView>();
        _particlesView = new List<ParticleTemplateView>();
        _skins = new List<SkinTemplateView>();

        await CreateSwordTemplates();
        await CreateParticleTemplates();

        _skinUnlocker = new SkinUnlocker(_skins, _coinsStorage, _coinsCalculator);

        SubScribeToEvents();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _swordsView.Count; i++)
            _swordsView[i].Clicked -= SetSwordToPlayer;

        for (int i = 0; i < _particlesView.Count; i++)
            _particlesView[i].Clicked -= SetParticleToPlayer;
    }

    private void SubScribeToEvents()
    {
        for (int i = 0; i < _swordsView.Count; i++)
            _swordsView[i].Clicked += SetSwordToPlayer;

        for (int i = 0; i < _particlesView.Count; i++)
            _particlesView[i].Clicked += SetParticleToPlayer;
    }

    [Inject]
    private void Constructor(IEnumerable<SwordData> swords, IEnumerable<ParticleData> particles, PlayerData playerData, 
        Factory factory, ICoinsStorage coinsStorage, CoinsCalculator coinsCalculator)
    {
        _swordsData = swords;
        _particlesData = particles;
        _playerData = playerData;
        _factory = factory;
        _coinsStorage = coinsStorage;
        _coinsCalculator = coinsCalculator;
    }

    public bool TryUnlockRandomSkin(out int newPrice)
    {
        if (_skinUnlocker.TryUnlock())
        {
            newPrice = _skinUnlocker.GetCurrentPrice();
            return true;
        }

        newPrice = 0;
        return false;
    }

    public int GetCurrentPrice() => _skinUnlocker.GetCurrentPrice();

    private async UniTask CreateSwordTemplates()
    {
        foreach (var data in _swordsData)
        {
            var view = Instantiate(_swordView, _swordContainer);
            string assetName = data.Sword.ToString();
            var prefab = await _factory.CreateAsync(assetName);

            view.Init(data, prefab, _skinView);
            _swordsView.Add(view);
            _skins.Add(view);
        }
    }

    private async UniTask CreateParticleTemplates()
    {
        foreach (var data in _particlesData)
        {
            var view = Instantiate(_particleView, _particleContainer);
            string assetName = data.Particle.ToString();
            var prefab = await _factory.CreateAsync(assetName);

            view.Init(data, prefab, _skinView);
            _particlesView.Add(view);
            _skins.Add(view);
        }
    }

    private void SetSwordToPlayer(SwordData sword)
    {
        _playerData.Sword = sword.Sword;
    }

    private void SetParticleToPlayer(ParticleData particleData)
    {
        _playerData.Particle = particleData.Particle;
    }
}