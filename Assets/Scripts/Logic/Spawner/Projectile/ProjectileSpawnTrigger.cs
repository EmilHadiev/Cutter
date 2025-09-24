using UnityEngine;
using Zenject;

public class ProjectileSpawnTrigger : MonoBehaviour
{
    [SerializeField] private Transform _spawnPlace;
    [SerializeField] private Transform _playerDetector;

    [Inject]
    private IProjectileSpawnContainer _spawner;

    private void Start() => _spawner.Spawn(_spawnPlace.position, _playerDetector.position);
}