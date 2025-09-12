using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
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

    public async UniTask<GameObject> Spawn(Vector3 position = default, Quaternion rotation = default)
    {
        return await Create(position, rotation);
    }

    private async UniTask<GameObject> Create(Vector3 position = default, Quaternion rotation = default)
    {
        try
        {
            var skeleton = await _factory.Create(AssetProvider.SkeletonPrefab, position, rotation, null);
            return skeleton;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return null;
        }
    }
}