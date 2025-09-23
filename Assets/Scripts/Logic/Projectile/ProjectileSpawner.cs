using UnityEngine;
using Zenject;

[RequireComponent(typeof(ProjectileReward))]
public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPlace;
    [SerializeField] private Projectile _template;
    [SerializeField] private TriggerObserver _playerDetector;

    private IFactory _factory;
    private Projectile _projectile;

    private void Awake() => CreateProjectile();

    private void OnValidate() => _playerDetector ??= GetComponentInChildren<TriggerObserver>();

    private void OnEnable() => _playerDetector.Entered += AttackPlayer;

    private void OnDisable() => _playerDetector.Entered -= AttackPlayer;

    [Inject]
    private void Constructor(IFactory factory)
    {
        _factory = factory;
    }

    private void CreateProjectile()
    {
        var prefab = _factory.Create(_template.gameObject);
        prefab.transform.localPosition = _spawnPlace.position;
        prefab.transform.localRotation = _spawnPlace.rotation;

        _projectile = prefab.GetComponent<Projectile>();
    }

    private void AttackPlayer(Collider collider)
    {
        _projectile.gameObject.SetActive(true);
        _projectile.StartMove(transform);
    }
}