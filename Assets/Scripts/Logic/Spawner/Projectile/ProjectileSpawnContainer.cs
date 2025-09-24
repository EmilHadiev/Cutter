using UnityEngine;
using Zenject;

public class ProjectileSpawnContainer : MonoBehaviour, IProjectileSpawnContainer
{
    [SerializeField] private ProjectileSpawner _template;

    [Inject]
    private readonly IFactory _factory;

    public GameObject Spawn(Vector3 projectileSpawnPlace, Vector3 playerDetectorPlace)
    {
        var prefab = _factory.Create(_template.gameObject);

        if (prefab.TryGetComponent(out ProjectileOptions projectileOptions))
        {
            projectileOptions.SetPlaceToSpawn(projectileSpawnPlace);
            projectileOptions.SetPlaceToPlayerDetector(playerDetectorPlace);
        }

        return prefab;
    }
}