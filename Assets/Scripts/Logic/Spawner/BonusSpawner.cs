using UnityEngine;
using Zenject;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private Bonus[] _bonus;
    [SerializeField] private BonusPlace[] _places;

    [Inject]
    private readonly IFactory _factory;

    private void OnValidate()
    {
        _places ??= GetComponentsInChildren<BonusPlace>();
    }

    private void Start()
    {
        if (_bonus.Length == 0 || _places.Length == 0)
            return;

        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _places.Length; i++)
        {
            var prefab = _factory.Create(GetRandomBonus().gameObject);
            prefab.transform.position = _places[i].transform.position;
        }
    }

    private Bonus GetRandomBonus()
    {
        int index = Random.Range(0, _bonus.Length);
        return _bonus[index];
    }
}