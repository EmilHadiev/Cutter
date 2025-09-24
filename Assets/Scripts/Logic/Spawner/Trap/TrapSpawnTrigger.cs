using UnityEngine;
using Zenject;

public class TrapSpawnTrigger : MonoBehaviour
{
    [Inject]
    private readonly ITrapSpawner _spawner;

    private void Start()
    {
        _spawner.Spawn(transform.position);
    }
}