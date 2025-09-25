using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerProgress _playerProgress;
    [SerializeField] private SwordData[] _swords;
    [SerializeField] private ParticleData[] _particles;

    public override void InstallBindings()
    {
        BindPlayerData();
        BindSwordsData();
        BindParticlesData();
        BindPlayerProgress();
    }

    private void BindPlayerProgress()
    {
        Container.Bind<PlayerProgress>().FromNewScriptableObject(_playerProgress).AsSingle();
    }

    private void BindPlayerData()
    {
        Container.Bind<PlayerData>().FromNewScriptableObject(_playerData).AsSingle();
    }

    private void BindParticlesData()
    {
        List<ParticleData> swords = new List<ParticleData>(_particles.Length);

        for (int i = 0; i < _particles.Length; i++)
        {
            var data = Instantiate(_particles[i]);
            swords.Add(data);
        }

        Container.Bind<IEnumerable<ParticleData>>().FromInstance(swords);
    }

    private void BindSwordsData()
    {
        List<SwordData> swords = new List<SwordData>(_swords.Length);

        for (int i = 0; i < _swords.Length; i++)
        {
            var data = Instantiate(_swords[i]);
            swords.Add(data);
        }

        Container.Bind<IEnumerable<SwordData>>().FromInstance(swords);
    }
}