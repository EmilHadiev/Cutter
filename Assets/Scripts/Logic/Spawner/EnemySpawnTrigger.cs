using Cysharp.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(SpawnOptions))]
public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawner;
    [SerializeField] private SpawnOptions _spawnOptions;

    private void OnValidate()
    {
        _spawner ??= FindObjectOfType<EnemySpawner>();
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