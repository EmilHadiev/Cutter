using UnityEngine;
using Zenject;

public class TrapSpawner : MonoBehaviour, ITrapSpawner
{
    [SerializeField] private Trap _trap;

    [Inject]
    private readonly IFactory _factory;

    public void Spawn(Vector3 position)
    {
        var prefab = _factory.Create(_trap.gameObject);
        prefab.transform.position = position;
    }
}