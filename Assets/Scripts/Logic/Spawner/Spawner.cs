using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class Spawner : MonoBehaviour
{
    private List<GameObject> loadedAssets = new List<GameObject>();
    private IFactory _factory;
    private IAddressablesLoader _loader;
    private List<EnemyData> _data;

    [Inject]
    private void Constructor(IFactory factory, IAddressablesLoader loader, IEnumerable<EnemyData> data)
    {
        _factory = factory;
        _loader = loader;
        _data = new List<EnemyData>(data);
    }

    private async UniTask Start()
    {
        try
        {
            foreach (var data in _data)
            {
                var prefab = await _factory.Create(data.AssetReference);

                if (prefab.TryGetComponent(out IEnemy enemy))
                    enemy.SetData(data);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }        
    }
}