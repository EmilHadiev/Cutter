using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataInstaller : MonoInstaller
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EnemyData[] _data;

    public override void InstallBindings()
    {
        BindPlayerData();
        BindEnemyData();
    }

    private void BindPlayerData()
    {
        Container.Bind<PlayerData>().FromNewScriptableObject(_playerData).AsSingle();
    }
    
    private void BindEnemyData()
    {
        List<EnemyData> enemyData = new List<EnemyData>(_data.Length);

        foreach (var data in _data)
        {
            var newData = Instantiate(data);
            enemyData.Add(newData);
        }

        Container.Bind<IEnumerable<EnemyData>>().FromInstance(enemyData).AsSingle();
    }
}