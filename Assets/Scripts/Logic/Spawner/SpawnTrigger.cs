using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        _spawner.Spawn(transform.position, transform.rotation);
    }
}