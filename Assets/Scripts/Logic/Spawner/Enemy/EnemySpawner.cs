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

    public async UniTask<GameObject> Spawn(AssetProvider.Enemy enemy,Vector3 position = default, Quaternion rotation = default)
    {
        return await Create(enemy, position, rotation);
    }

    private async UniTask<GameObject> Create(AssetProvider.Enemy enemy, Vector3 position = default, Quaternion rotation = default)
    {
        try
        {
            var skeleton = await _factory.CreateAsync(GetEnemy(enemy), position, rotation, null);
            return skeleton;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return null;
        }
    }

    private string GetEnemy(AssetProvider.Enemy enemy)
    {
        switch (enemy)
        {
            case AssetProvider.Enemy.Skeleton:
                return AssetProvider.SkeletonPrefab;
            case AssetProvider.Enemy.BK:
                return AssetProvider.BlackKnightPrefab;
            case AssetProvider.Enemy.Demon:
                return AssetProvider.DemonKing;
        }

        throw new ArgumentException(nameof(enemy));
    }
}