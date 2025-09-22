using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour, IEnemySpawner
{
    private IFactory _factory;

    [Inject]
    private void Constructor(IFactory factory)
    {
        _factory = factory;
    }

    public async UniTask<GameObject> Spawn(Vector3 position = default, Quaternion rotation = default)
    {
        return await Create(position, rotation);
    }

    private async UniTask<GameObject> Create(Vector3 position = default, Quaternion rotation = default)
    {
        try
        {
            var skeleton = await _factory.CreateAsync(AssetProvider.SkeletonPrefab, position, rotation, null);
            return skeleton;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return null;
        }
    }
}