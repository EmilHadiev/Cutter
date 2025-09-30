using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IEnemySpawner
{
    UniTask<GameObject> Spawn(AssetProvider.Enemy enemy, Vector3 position = default, Quaternion rotation = default);
}