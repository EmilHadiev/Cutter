using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
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
            var skeleton = await _factory.Create(AssetProvider.SkeletonPrefab);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }        
    }
}