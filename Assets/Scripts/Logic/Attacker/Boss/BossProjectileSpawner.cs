using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BossProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Projectile _darkProjectileTemplate;
    [SerializeField] private Transform _spawnPosition;

    private List<Projectile> _projectiles;

    private IFactory _factory;


    private void Awake()
    {
        _projectiles = new List<Projectile>();

        int projectilesMax = 3;
        for (int i = 0; i < projectilesMax; i++)
        {
            var prefab = _factory.Create(_darkProjectileTemplate.gameObject);
            prefab.gameObject.SetActive(false);
            prefab.transform.position = _spawnPosition.position;

            var projectile = prefab.GetComponent<Projectile>();
            _projectiles.Add(projectile);
        }
    }

    [Inject]
    private void Constructor(IFactory factory, IPlayer player)
    {
        _factory = factory;
    }

    /// <summary>
    /// From animation
    /// </summary>
    private void AttackEnded()
    {
        if (TryGetProjectile(out Projectile projectile))
        {
            projectile.gameObject.SetActive(true);
            projectile.StartMove(transform);
        }
    }

    private bool TryGetProjectile(out Projectile projectile)
    {
        projectile = _projectiles.FirstOrDefault(p => p.gameObject.activeInHierarchy == false);

        return projectile != null;
    }
}