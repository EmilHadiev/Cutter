using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IEnemySpawner
{
    UniTask<GameObject> Spawn(Vector3 position = default, Quaternion rotation = default);
}