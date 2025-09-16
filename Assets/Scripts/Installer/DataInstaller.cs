using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private SwordData[] _swords;

    public override void InstallBindings()
    {
        BindPlayerData();
        BindSwordsData();
    }

    private void BindPlayerData()
    {
        Container.Bind<PlayerData>().FromNewScriptableObject(_playerData).AsSingle();
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