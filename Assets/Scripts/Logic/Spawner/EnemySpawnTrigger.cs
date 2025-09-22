using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SpawnOptions))]
public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private SpawnOptions _spawnOptions;

    [Inject]
    private readonly IEnemySpawner _spawner;

    private void OnValidate()
    {
        _spawnOptions ??= GetComponent<SpawnOptions>();
    }

    private async UniTask Start()
    {
        await Spawn();
    }

    private async UniTask Spawn()
    {
        var prefab = await _spawner.Spawn(transform.position, transform.rotation);

        if (prefab.TryGetComponent(out IEnemy enemy))
        {
            _spawnOptions.SetupEnemy(enemy);
        }
    }
}