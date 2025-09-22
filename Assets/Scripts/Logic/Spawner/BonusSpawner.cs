using UnityEngine;
using Zenject;

public class BonusSpawner : MonoBehaviour, IBonusSpawner
{
    [SerializeField] private Bonus[] _bonus;

    [Inject]
    private readonly IFactory _factory;

    public void Spawn(Vector3 position)
    {
        var prefab = _factory.Create(GetRandomBonus().gameObject);
        prefab.transform.position = position;
    }

    private Bonus GetRandomBonus()
    {
        int index = Random.Range(0, _bonus.Length);
        return _bonus[index];
    }
}