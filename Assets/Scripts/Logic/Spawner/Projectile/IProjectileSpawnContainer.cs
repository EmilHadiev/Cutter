using UnityEngine;

public interface IProjectileSpawnContainer
{
    GameObject Spawn(Vector3 projectileSpawnPlace, Vector3 playerDetectorPlace);
}