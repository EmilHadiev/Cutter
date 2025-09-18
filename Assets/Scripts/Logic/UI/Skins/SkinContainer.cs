using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private List<SwordTemplateView> _swordsView;
    private List<ParticleTemplateView> _particlesView;

    private List<string> _assets;

    private IEnumerable<SwordData> _swordsData;
    private IEnumerable<ParticleData> _particlesData;

    private void Awake()
    {
        _swordsView = new List<SwordTemplateView>();
        _particlesView = new List<ParticleTemplateView>();

        _assets = new List<string>();

        CreateSwordTemplates().Forget();
        CreateParticleTemplates().Forget();

        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnSubscribeFromEvents();
    }

    private void UnSubscribeFromEvents()
    {
        for (int i = 0; i < _swordsView.Count; i++)
            _swordsView[i].Clicked -= SetSwordToPlayer;

        for (int i = 0; i < _particlesView.Count; i++)
            _particlesView[i].Clicked -= SetParticleToPlayer;
    }

    private void SubscribeToEvents()
    {
        for (int i = 0; i < _swordsView.Count; i++)
            _swordsView[i].Clicked += SetSwordToPlayer;

        for (int i = 0; i < _particlesView.Count; i++)
            _particlesView[i].Clicked += SetParticleToPlayer;
    }

    private void Done()
    {
        foreach (var asset in _assets)
        {
            if (asset == _playerData.Sword.ToString())
                continue;

            if (asset == _playerData.Particle.ToString())
                continue;

            _factory.ReleaseAsset(asset);
        }
    }

    [Inject]
    private void Constructor(IEnumerable<SwordData> swords, IEnumerable<ParticleData> particles, PlayerData playerData, Factory factory)
    {
        _swordsData = swords;
        _particlesData = particles;
        _playerData = playerData;
        _factory = factory;
    }

    private async UniTaskVoid CreateSwordTemplates()
    {
        foreach (var data in _swordsData)
        {
            var view = Instantiate(_swordView, _swordContainer);
            string assetName = data.Sword.ToString();
            var prefab = await _factory.CreateAsync(assetName);

            _assets.Add(assetName);
            view.Init(data, prefab, _skinView);
            _swordsView.Add(view);
        }
    }

    private async UniTaskVoid CreateParticleTemplates()
    {
        foreach (var data in _particlesData)
        {
            var view = Instantiate(_particleView, _particleContainer);
            string assetName = data.Particle.ToString();
            var prefab = await _factory.CreateAsync(assetName);

            _assets.Add(assetName);
            view.Init(data, prefab, _skinView);
            _particlesView.Add(view);
        }
    }

    private void SetSwordToPlayer(SwordData sword)
    {
        _playerData.Sword = sword.Sword;
        Debug.Log("!!!");
    }

    private void SetParticleToPlayer(ParticleData particleData)
    {
        _playerData.Particle = particleData.Particle;
        Debug.Log("@@@");
    }
}