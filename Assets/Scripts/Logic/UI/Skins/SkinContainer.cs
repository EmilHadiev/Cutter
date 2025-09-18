using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SkinContainer : MonoBehaviour
{
    [SerializeField] private SwordTemplateView _swordView;
    [SerializeField] private ParticleTemplateView _particleView;
    [SerializeField] private Transform _swordContainer;
    [SerializeField] private Transform _particleContainer;

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
        Show();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _swordsView.Count; i++)
            _swordsView[i].Clicked += SetSwordToPlayer;

        for (int i = 0; i < _particlesView.Count; i++)
            _particlesView[i].Clicked += SetParticleToPlayer;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _swordsView.Count; i++)
            _swordsView[i].Clicked -= SetSwordToPlayer;

        for (int i = 0; i < _particlesView.Count; i++)
            _particlesView[i].Clicked -= SetParticleToPlayer;
    }

    private void OnDestroy()
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
            view.Init(data, prefab);
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
            view.Init(data, prefab);
            _particlesView.Add(view);
        }
    }

    private void Show()
    {
        foreach (var view in _swordsView)
            view.Render();
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